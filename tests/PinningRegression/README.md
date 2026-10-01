# Upload pinning regression

Run `dotnet run --project tests/PinningRegression/PinningRegression.csproj -c Release` with .NET 8.

The harness links the actual Chunk.cs and Struct.cs and supplies minimal test-only DxLib/Cube types. Upload callbacks trigger compacting GC, compare the passed pointer with the live managed array's current address, and only then inspect the live array. It never dereferences an unmatched old pointer. Three array sizes exercise both uploads (six checks).

This validates the managed pinning lifetime in Chunk.SendGPU. It does not validate mesh generation, native ABI, the external DxLibDotNet.dll, Windows/.NET Framework 4.6.1 integration, GPU copies or rendering. Those need the original Windows dependency setup and a separate smoke test.
