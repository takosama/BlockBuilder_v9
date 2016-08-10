using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class Chunk
    {
        DX.VERTEX3D[] Vertex;
        ushort[] Index;
        int[,,] BlockList = new int[16, 128, 16];

        int VertexHandle = -1;
        int IndexHandle = -1;

        IntPtr VertexPointer;
        IntPtr IndexPointer;

        PolygonList p = new PolygonList();

        int x;
        int z;

        public void Dispose()
        {
            CleanGPU();
            BlockList = null;
        }

        void CleanGPU()
        {
            DX.DeleteVertexBuffer(VertexHandle);
            DX.DeleteIndexBuffer(IndexHandle);
        }

        ~Chunk()
        {
            Vertex = null;
            Index = null;
            Dispose();
        }
        void SetUpBuffer()
        {
        }

        public void Draw(int hdl)
        {
            DX.DrawPolygonIndexed3D_UseVertexBuffer(VertexHandle, IndexHandle, hdl, 1);
        }

        unsafe public void Refresh()
        {
            CleanGPU();
            CreatePolygonList();

            Vertex = p.Vertex.ToArray();
            Index = p.Index.ToArray();

            fixed (DX.VERTEX3D* v = Vertex)
            {
                VertexPointer = (IntPtr)v;
            }

            fixed (ushort* i = Index)
            {
                IndexPointer = (IntPtr)i;

            }


            VertexHandle = DX.CreateVertexBuffer(Vertex.Length, DX.DX_VERTEX_TYPE_NORMAL_3D);
            IndexHandle = DX.CreateIndexBuffer(Index.Length, DX.DX_INDEX_TYPE_16BIT);

            DX.SetVertexBufferData(0, VertexPointer, Vertex.Length, VertexHandle);
            DX.SetIndexBufferData(0, IndexPointer, Index.Length, IndexHandle);
        }

        unsafe public Chunk(int x, int z)
        {
            p.SetUpPolygon();
            SetUpBuffer();

            this.x = x;
            this.z = z;
        }

        public void GenerateChunk()
        {
            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 8; y++)
                    for (int z = 0; z < 16; z++)
                        BlockList[x, y, z] = 1;
        }

        void CreatePolygonList()
        {
            p.Clear();

            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 128; y++)
                    for (int z = 0; z < 16; z++)
                    {
                        if (BlockList[x, y, z] == 1)
                        {
                            Cube.surfaceFlagList s = new Cube.surfaceFlagList();
                            s.SetTrueAll();

                            if (x != 0)
                                if (BlockList[x - 1, y, z] != 0) s.Left = false;
                            if (x != 15)
                                if (BlockList[x + 1, y, z] != 0) s.Right = false;
                            if (y != 0)
                                if (BlockList[x, y - 1, z] != 0) s.Bottom = false;
                            if (y != 15)
                                if (BlockList[x, y + 1, z] != 0) s.Top = false;
                            if (z != 0)
                                if (BlockList[x, y, z - 1] != 0) s.Front = false;
                            if (z != 15)
                                if (BlockList[x, y, z + 1] != 0) s.Back = false;

                            p.AddPolygon(Cube.GeneratePolygonList(s, DX.VGet(x + this.x, y, z + this.z), 1, 12));
                        }
                    }
        }

        public void SetBlock(int x, int y, int z)
        {
            BlockList[x, y, z] = 1;
        }
        public void DeleteBlock(int x, int y, int z)
        {
            BlockList[x, y, z] = 0;
        }
    }
}