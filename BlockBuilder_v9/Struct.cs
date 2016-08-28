using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class Rotate
    {
        public float ax { get; set; }
        public float ay { get; set; }
        public float az { get; set; }

        public Rotate(float ax, float ay, float az)
        {
            this.ax = ax;
            this.ay = ay;
            this.az = az;
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
        public List<DX.VERTEX3D> Vertex;// { get; private set; }
        public List<ushort> Index;// { get; private set; }
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
            Index = null;
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
            ++this.MaxIndex;
            int Max = this.MaxIndex;

            Vertex.AddRange(polygonList.Vertex);
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

    class Pos
    {
        public Pos(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public float x;
        public float y;
        public float z;
    }

}