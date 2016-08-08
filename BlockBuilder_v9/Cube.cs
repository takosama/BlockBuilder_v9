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
   public     struct surfaceFlagList
        {
        public    bool Top;
        public    bool Bottom;
        public    bool Right;
        public    bool Left;
        public    bool Front;
        public    bool Back;

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

        public PolygonList GeneratePolygonList(surfaceFlagList FlagList, DX.VECTOR Vec,int Size,int Light)
        {
            PolygonList p = new PolygonList();
            Rotate rot = new Rotate();
            List<ushort> Index = new List<ushort>();
            float halfSize = (float)(Size * 0.5); 

            if (FlagList.Top == true)
                  rot = Rotate.GetRotate(1, 0, 0);
                 p.AddPolygon(Rectangle.GetRectanglePolygon(DX.VGet(Vec.x, Vec.y+halfSize, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH));
            if (FlagList.Top == true)
                rot = Rotate.GetRotate(1, 0, 0);
            p.AddPolygon(Rectangle.GetRectanglePolygon(DX.VGet(Vec.x, Vec.y - halfSize, Vec.z), rot, Size, Light, DX.DX_NONE_GRAPH));

            return p;
        }
    }
}
