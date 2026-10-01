using System.Reflection;
using BlockBuilder_v9;
using DxLibDLL;
unsafe class PinningRegression
{
    static int Main()
    {
        int uploads = 0;
        foreach (int count in new[] { 1, 32, 512 })
        {
            var chunk = new Chunk(0, 0);
            var polygon = (PolygonList)typeof(Chunk).GetField("p", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(chunk)!;
            for (int i = 0; i < count; i++)
            {
                polygon.Vertex.Add(new DX.VERTEX3D { FixtureValue = i + 7 });
                polygon.Index.Add((ushort)i);
            }
            DX.VertexUpload = (source, length) =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
                var live = (DX.VERTEX3D[])typeof(Chunk).GetField("Vertex", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(chunk)!;
                fixed (DX.VERTEX3D* current = live)
                {
                    // Never dereference a stale pointer, even if a future regression unpins it.
                    if (source != (IntPtr)current || length != count) throw new Exception("Vertex upload pointer no longer tracks live array");
                    for (int i = 0; i < length; i++) if (current[i].FixtureValue != i + 7) throw new Exception("Vertex fixture changed");
                }
                uploads++;
            };
            DX.IndexUpload = (source, length) =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
                var live = (ushort[])typeof(Chunk).GetField("Index", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(chunk)!;
                fixed (ushort* current = live)
                {
                    if (source != (IntPtr)current || length != count) throw new Exception("Index upload pointer no longer tracks live array");
                    for (int i = 0; i < length; i++) if (current[i] != i) throw new Exception("Index fixture changed");
                }
                uploads++;
            };
            chunk.SendGPU();
            chunk.Dispose();
        }
        if (uploads != 6) throw new Exception("Expected both uploads for all fixtures");
        Console.WriteLine("PASS 6 upload pinning checks across forced compacting GC, using production Chunk.SendGPU and test-only DxLib stubs.");
        Console.WriteLine("Actual Windows/net461/DxLib rendering and native ABI are not covered.");
        return 0;
    }
}
