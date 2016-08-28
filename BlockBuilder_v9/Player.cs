using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class Player
    {
        public float x { get; set; } = 0;
        public float y { get; set; } = 0;
        public float z { get; set; } = 0;
        public Rotate rot;

        public Player()
        {
            this.x = 0;
            this.y = 10.6f;
            this.z = 0;
            rot = new Rotate(0, 0, 0);
        }

        public void PalyerMove(World w)
        {
            float mx = x;
            float my = y;
            float mz = z;

            if (DX.CheckHitKey(DX.KEY_INPUT_LEFT) == 1)
                rot.ay -= 0.04f;
            if (DX.CheckHitKey(DX.KEY_INPUT_RIGHT) == 1)
                rot.ay += 0.04f;
            if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
            {
                mx += (float)(Math.Sin(rot.ay) * 0.1);
                mz += (float)(Math.Cos(rot.ay) * 0.1);
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {
                mx -= (float)(Math.Sin(rot.ay) * 0.1);
                mz -= (float)(Math.Cos(rot.ay) * 0.1);
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                mx -= (float)(Math.Cos(rot.ay) * 0.1);
                mz += (float)(Math.Sin(rot.ay) * 0.1);
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
            {
                mx += (float)(Math.Cos(rot.ay) * 0.1);
                mz -= (float)(Math.Sin(rot.ay) * 0.1);
            }

            int c_mx = (int)(mx / 16);
            int c_mz = (int)(mz / 16);
            float p_mx = (mx % 16);
            float p_mz = (mz % 16);



           var p= Move(new Pos(mx, my, mz)); 
            x = p.x;
            y = p.y;
            z = p.z;
          
        }

        Pos Move(Pos pos)
        {
            return pos;
        }
    }
}
