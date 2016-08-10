using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class Cube
    {
        public struct surfaceFlagList
        {
            public bool Top;
            public bool Bottom;
            public bool Right;
            public bool Left;
            public bool Front;
            public bool Back;

            public void SetTrueAll()
            {
                Top = true;
                Bottom = true;
                Right = true;
                Left = true;
                Front = true;
                Back = true;
            }
        }


     static   public PolygonList GeneratePolygonList(surfaceFlagList FlagList, DX.VECTOR Vec, int Size, int Light)
        {
            PolygonList p = new PolygonList();
            p.SetUpPolygon();
            Rotate rot = new Rotate();
            Rectangle r = new Rectangle();
            List<ushort> Index = new List<ushort>();
            float halfSize = (float)(Size * 0.5);

            if (FlagList.Top == true)
            {
                rot = Rotate.GetRotate(1, 0, 0);
              var n=Rectangle.GetRectanglePolygon(DX.VGet(Vec.x, Vec.y + halfSize, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }
            if (FlagList.Bottom == true)
            {
                rot = Rotate.GetRotate(1, 0, 0);
                var n =Rectangle. GetRectanglePolygon(DX.VGet(Vec.x, Vec.y - halfSize, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }

            if (FlagList.Right == true)
            {
                rot = Rotate.GetRotate(0, 1, 0);
              var n=Rectangle.GetRectanglePolygon(DX.VGet(Vec.x + halfSize, Vec.y, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }
            if (FlagList.Left == true)
            {
                rot = Rotate.GetRotate(0, 1, 0);
        var n=   Rectangle.GetRectanglePolygon(DX.VGet(Vec.x - halfSize, Vec.y, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }

            if (FlagList.Front == true)
            {
                rot = Rotate.GetRotate(0, 0, 0);
              var n=Rectangle.GetRectanglePolygon(DX.VGet(Vec.x, Vec.y, Vec.z - halfSize), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }
            if (FlagList.Back == true)
            {
                rot = Rotate.GetRotate(0, 0, 0);
                var n=Rectangle.GetRectanglePolygon(DX.VGet(Vec.x, Vec.y, Vec.z + halfSize), rot, Size, Light, DX.DX_NONE_GRAPH);
                p.AddPolygon(n);
            }

            return p;
        }
    }
}