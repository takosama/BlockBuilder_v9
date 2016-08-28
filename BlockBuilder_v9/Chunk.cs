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
        public int[,,] BlockList = new int[18, 128, 18];

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
         
            p.SetUpPolygon();
            CreatePolygonList();


            
        }

        unsafe public void SendGPU()
        {
            Vertex = p.Vertex.ToArray();
            Index = p.Index.ToArray();

            fixed (DX.VERTEX3D* v = Vertex)
                VertexPointer = (IntPtr)v;
            fixed (ushort* i = Index)
                IndexPointer = (IntPtr)i;

            VertexHandle = DX.CreateVertexBuffer(Vertex.Length, DX.DX_VERTEX_TYPE_NORMAL_3D);
            IndexHandle = DX.CreateIndexBuffer(Index.Length, DX.DX_INDEX_TYPE_16BIT);

            DX.SetVertexBufferData(0, VertexPointer, Vertex.Length, VertexHandle);
            DX.SetIndexBufferData(0, IndexPointer, Index.Length, IndexHandle);

            p.Clear();
        }

        unsafe public Chunk(int x, int z)
        {
            p.SetUpPolygon();
            SetUpBuffer();

            this.x = x;
            this.z = z;
        }

        public void GenerateChunk(int c_x, int c_z)
        {
            for (int y = 3; y < 16; y += 1)
            for (int x = 2; x < 17; x++)
            {
                for (int z = 1; z < 17; z++)
                {
                    BlockList[x, y, z] = 1;
                }
            }
        }

        void CreatePolygonList()
        {
            Cube c = new Cube();
            var n = new PolygonList();
            for (int x = 1; x < 17; x++)
                for (int y = 0; y < 128; y++)
                    for (int z = 1; z < 17; z++)
                        if (BlockList[x, y, z] == 1)
                        {
                            Cube.surfaceFlagList s = new Cube.surfaceFlagList();
                            s.SetTrueAll();

                           // if (x != 0)
                                if (BlockList[x - 1, y, z] != 0) s.Left = false;
                           // if (x != 16)
                                if (BlockList[x + 1, y, z] != 0) s.Right = false;
                            if (y != 0)
                                if (BlockList[x, y - 1, z] != 0) s.Bottom = false;
                            if (y != 127)
                                if (BlockList[x, y + 1, z] != 0) s.Top = false;
                         //   if (z != 0)
                                if (BlockList[x, y, z - 1] != 0) s.Front = false;
                           // if (z != 16)
                                if (BlockList[x, y, z + 1] != 0) s.Back = false;

                            p.AddPolygon(c.GeneratePolygonList(s, DX.VGet(x + this.x - 1, y, z + this.z - 1), 1, 12));
                        }
      

        }

        public int GetBlock(int x, int y, int z)
        {
            return BlockList[x , y, z ];
        }

        public void SetBlock(int x, int y, int z)
        {
            BlockList[x , y, z ] = 1;
        }
        public void DeleteBlock(int x, int y, int z)
        {
            BlockList[x , y, z ] = 0;
        }

        public int ClacNextFloor(int x,int y,int z)
        {
            int start = 127;
            if (y < 128) start = y;

            for(int i=start;i>=0;i--)
            {
                if (BlockList[x, i, z] == 1) return i;
            }
            return 0;
        }
    }
}