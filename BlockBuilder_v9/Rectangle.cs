using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class Rectangle
    {
        static ushort[] Index { get; } = { 0, 2, 1, 0, 3, 2 };

        static public Polygon GetRectanglePolygon(DX.VECTOR pos, Rotate rot, int Size, int Light, int Ghdl)
        {
            DX.VERTEX3D[] Vertex = new DX.VERTEX3D[4];
            DX.VECTOR v = new DX.VECTOR();
            DX.VECTOR[] vec = new DX.VECTOR[4];
            DX.VECTOR[] vec1 = new DX.VECTOR[4];

            float halfSize = Size * 0.5f;
            float x = pos.x;
            float y = pos.y;
            float z = pos.z;

            v.x = -halfSize;
            v.y = halfSize;

            vec[0] = v;
            v.y = -halfSize;
            vec[1] = v;//DX.VGet(-halfSize, -halfSize, 0);
            v.x = halfSize;
            vec[2] = v;// DX.VGet(halfSize, -halfSize, 0);
            v.y = halfSize;
            vec[3] = v;// DX.VGet(halfSize, halfSize, 0);

            float sinx = rot.ax;//Sin[ax];
            float cosx = rot.ax == 1 ? 0 : rot.ax == -1 ? 0 : 1;//Cos[ax];
            float siny = rot.ay;//Sin[ay];
            float cosy = rot.ay == 1 ? 0 : rot.ay == -1 ? 0 : 1;//Cos[ay];
            float sinz = rot.az;//Sin[az];
            float cosz = rot.az == 1 ? 0 : rot.az == -1 ? 0 : 1; ; //Cos[az];

            //           var vec1 = new DX.VECTOR[4];
            for (int i = 0; i < 4; i++)
            {
                vec1[i].x = vec[i].x;
                vec1[i].y = vec[i].z * sinx + vec[i].y * cosx;
                vec1[i].z = vec[i].z * cosx + vec[i].y * sinx;
            }

            for (int i = 0; i < 4; i++)
            {
                vec[i].x = vec1[i].x * cosy - vec1[i].z * siny;
                vec[i].y = vec1[i].y;
                vec[i].z = vec1[i].x * siny + vec1[i].z * cosy;
            }

            for (int i = 0; i < 4; i++)
            {
                vec1[i].x = vec[i].x * cosz - vec[i].y * sinz;
                vec1[i].y = vec[i].x * sinz + vec[i].y * cosz;
                vec1[i].z = vec[i].z;
            }

            vec1[0].x += x;
            vec1[0].y += y;
            vec1[0].z += z;

            vec1[1].x += x;
            vec1[1].y += y;
            vec1[1].z += z;

            vec1[2].x += x;
            vec1[2].y += y;
            vec1[2].z += z;

            vec1[3].x += x;
            vec1[3].y += y;
            vec1[3].z += z;

            DX.COLOR_U8 light = MyLight.LightLevelArrey[Light];
            DX.COLOR_U8 Ambient = MyLight.AmbientLightLevel;
            DX.VECTOR VertexNorm = DX.VGet(0, 0, -1);

            Vertex[0].pos = vec1[0];
            Vertex[0].norm = VertexNorm;
            Vertex[0].dif = light;
            Vertex[0].spc = Ambient;
            Vertex[0].u = 0.0f;
            Vertex[0].v = 0.0f;
            Vertex[0].su = 0.0f;
            Vertex[0].sv = 0.0f;

            Vertex[1].pos = vec1[1];
            Vertex[1].norm = VertexNorm;
            Vertex[1].dif = light;
            Vertex[1].spc = Ambient;
            Vertex[1].u = 0.0f;
            Vertex[1].v = 1.0f;
            Vertex[1].su = 0.0f;
            Vertex[1].sv = 0.0f;

            Vertex[2].pos = vec1[2];
            Vertex[2].norm = VertexNorm;
            Vertex[2].dif = light;
            Vertex[2].spc = Ambient;
            Vertex[2].u = 1.0f;
            Vertex[2].v = 1.0f;
            Vertex[2].su = 0.0f;
            Vertex[2].sv = 0.0f;

            Vertex[3].pos = vec1[3];
            Vertex[3].norm = VertexNorm;
            Vertex[3].dif = light;
            Vertex[3].spc = Ambient;
            Vertex[3].u = 1.0f;
            Vertex[3].v = 0.0f;
            Vertex[3].su = 0.0f;
            Vertex[3].sv = 0.0f;

            Polygon p = new Polygon(Vertex, Index);
            return p;
        }
    }
}