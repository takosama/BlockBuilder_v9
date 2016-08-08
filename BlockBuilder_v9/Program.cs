using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace BlockBuilder_v9
{
    class Program
    {
        static void Main(string[] args)
        {


        }
    }

    class Chunk
    {
        DX.VERTEX3D[] Vertex;
        ushort[] Index;


        int VertexHandle = -1;
        int IndexHandle = -1;

        IntPtr VertexPointer;
        IntPtr IndexPointer;

        public void Dispose()
        {
            CleanGPU();
        }

        
            void CleanGPU()
        {
            DX.DeleteVertexBuffer(IndexHandle);
            DX.DeleteIndexBuffer(IndexHandle);
        }

        void SetUpBuffer()
        {
            VertexHandle = DX.CreateVertexBuffer(Vertex.Length, DX.DX_VERTEX_TYPE_NORMAL_3D);
            IndexHandle = DX.CreateIndexBuffer(Index.Length, DX.DX_INDEX_TYPE_16BIT);
        }

        public void Draw(int hdl)
        {
            DX.DrawPolygonIndexed3D_UseVertexBuffer(VertexHandle, IndexHandle, hdl, 1);
        }

        public void Refresh()
        {
            CleanGPU();
            DX.SetVertexBufferData(0, VertexPointer, Vertex.Length, VertexHandle);
            DX.SetIndexBufferData(0, IndexPointer, Index.Length, IndexHandle);
        }

        unsafe public Chunk()
        {
            fixed (DX.VERTEX3D* v = Vertex)
            {
                VertexPointer = (IntPtr)v;
            }

            fixed (ushort* i = Index)
            {
                IndexPointer = (IntPtr)i;
            }
            SetUpBuffer();
        }
    }
}