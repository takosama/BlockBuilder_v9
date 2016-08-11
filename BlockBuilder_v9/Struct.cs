using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    struct Rotate
    {
        public float ax { get; private set; }
        public float ay { get; private set; }
        public float az { get; private set; }

        public static Rotate GetRotate(float ax, float ay, float az)
        {
            Rotate rot = new Rotate();
            rot.ax = ax;
            rot.ay = ay;
            rot.az = az;
            return rot;
        }
    }

    struct cubeSideFlag
    {
        public bool u;
        public bool d;
        public bool f;
        public bool b;
        public bool l;
        public bool r;

        public void SetDefalt()
        {
            u = true;
            d = true;
            f = true;
            b = true;
            l = true;
            r = true;
        }
    }

class PolygonList
    {
        public List<DX.VERTEX3D> Vertex { get; private set; }
        public List<ushort> Index { get; private set; }
        int MaxIndex;

        public void SetUpPolygon()
        {
            Vertex = new List<DX.VERTEX3D>();
            Index = new List<ushort>();
            MaxIndex = -1;
        }

        public void Clear()
        {
            Vertex = null;
            Index =null;
            MaxIndex = -1;
        }

        public void AddPolygon(Polygon polygon)
        {
            if (polygon.Index == null || polygon.Vertex == null) return;

            ++this.MaxIndex;
            int Max = this.MaxIndex;
            Vertex.AddRange(polygon.Vertex);
            Index.AddRange(polygon.Index.Select(x => (ushort)(x + Max)));
            Max += polygon.Index.Max();
            this.MaxIndex = Max;
        }

        public void AddPolygon(PolygonList polygonList)
        {
            if (polygonList.Index.Count == 0 || polygonList.Vertex.Count == 0) return;
            Vertex.AddRange(polygonList.Vertex);
            ++this.MaxIndex;
            int Max = this.MaxIndex;
            Index.AddRange(polygonList.Index.Select(x => (ushort)(x + Max)));
            Max += polygonList.Index.Max();
            this.MaxIndex = Max;
        }


    }

    class Polygon
    {
        public DX.VERTEX3D[] Vertex { get; private set; }
        public ushort[] Index;

        public Polygon(DX.VERTEX3D[] Vertex, ushort[] Index)
        {
          
            this.Vertex = Vertex;
            this.Index = Index;
        }

       
    }
}