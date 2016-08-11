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
            DXLibInit();

            int hdl = DX.LoadGraph("TNT.png");

            World w = new World();
            w.LoadWorld();

            float x = 0;
            float y = 1.6f;
            float z = 0;
            float ay = 0;
            float ax = 0;
            int B_MX = 0;
            int B_MY = 0;
            DX.GetMousePoint(out B_MX, out B_MY);
            bool IsPointMove = true;

            while (true)
            {
                // GC.Collect();
                DX.ClearDrawScreen();

                if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                    z += 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                    z -= 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_UP) == 1)
                    y += 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) == 1)
                    y -= 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
                    x += 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
                    x -= 0.04f;
                if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 1)
                {
                    IsPointMove = !IsPointMove;
                    DX.WaitTimer(1000);
                }

                int MX = 0;
                int MY = 0;
                if (IsPointMove)
                {
                    DX.GetMousePoint(out MX, out MY);
                    ax += (MY - 240) * 0.001f;
                    ay += (MX - 320) * 0.001f;
                    DX.SetMousePoint(320, 240);
                }

                DX.SetCameraPositionAndAngle(DX.VGet(x, y, z), ax, ay, 0);

                foreach (var n in w.chunkArrey)
                    n.Draw(hdl);

                if (IsPointMove)
                    DX.DrawString(0, 0, MX + "," + MY, DX.GetColor(0, 255, 0));
                else
                    DX.DrawString(0, 0, "PointerFree", DX.GetColor(0, 255, 0));


                w.chunkArrey[0, 0].DeleteBlock(0, 0, 0);
                w.chunkArrey[0, 0].Refresh();
                w.chunkArrey[0, 0].SendGPU();

                DX.ScreenFlip();
            }
            DX.WaitKey();
        }

        private static void DXLibInit()
        {
            DX.SetWaitVSyncFlag(0);
            DX.ChangeWindowMode(1);
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.SetUseZBuffer3D(1);
            DX.SetWriteZBuffer3D(1);
            DX.SetTransColor(0, 255, 0);
            DX.SetCameraNearFar(0.1f, 255);
            DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
            DX.SetupCamera_Perspective((float)(Math.PI / 180 * 70));
        }
    }

}