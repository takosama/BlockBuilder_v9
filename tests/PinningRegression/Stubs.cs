// Test-only API surface. This is not a DxLib binary, ABI or rendering test.
namespace DxLibDLL
{
    public static class DX
    {
        public struct VERTEX3D { public int FixtureValue; }
        public struct VECTOR { public float x, y, z; }
        public const int DX_VERTEX_TYPE_NORMAL_3D = 0, DX_INDEX_TYPE_16BIT = 0;
        public static Action<IntPtr, int> VertexUpload = (_, _) => { };
        public static Action<IntPtr, int> IndexUpload = (_, _) => { };
        public static int CreateVertexBuffer(int length, int type) => 1;
        public static int CreateIndexBuffer(int length, int type) => 2;
        public static int DeleteVertexBuffer(int handle) => 0;
        public static int DeleteIndexBuffer(int handle) => 0;
        public static int DrawPolygonIndexed3D_UseVertexBuffer(int v, int i, int h, int t) => 0;
        public static int SetVertexBufferData(int offset, IntPtr source, int count, int handle)
        { VertexUpload(source, count); return 0; }
        public static int SetIndexBufferData(int offset, IntPtr source, int count, int handle)
        { IndexUpload(source, count); return 0; }
        public static VECTOR VGet(float x, float y, float z) => new() { x = x, y = y, z = z };
    }
}
namespace BlockBuilder_v9
{
    class Cube
    {
        public struct surfaceFlagList
        {
            public bool Top, Bottom, Right, Left, Front, Back;
            public void SetTrueAll() { Top = Bottom = Right = Left = Front = Back = true; }
        }
        public PolygonList GeneratePolygonList(surfaceFlagList flags, DxLibDLL.DX.VECTOR pos, int size, int light)
            => throw new NotSupportedException("Mesh generation is outside this upload-only test.");
    }
}
