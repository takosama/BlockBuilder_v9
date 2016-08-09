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
         //   DX.SetUserWindow(this.Handle);
            DX.SetWaitVSyncFlag(0);
            DX.ChangeWindowMode(1);
            DX.DxLib_Init();

            //   DX.SetUseBackCulling(1);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.SetUseZBuffer3D(1);
            DX.SetWriteZBuffer3D(1);
            DX.SetTransColor(0, 255, 0);
            DX.SetCameraNearFar(0.1f, 255);
            DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
           int hdl = DX.LoadGraph("TNT.png");
            DX.SetupCamera_Perspective((float)(Math.PI / 180 * 70));

         //   Rotate rot=new Rotate();

            Chunk c = new Chunk();
            c.GenerateChunk();
            c.Refresh();


            float x = 0;
            float y = 1.6f;
            float z = 0;
            float ay = 0;

            while(true)
            {
                if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
                    z += 0.01f;
                if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
                    z -= 0.01f;
                if (DX.CheckHitKey(DX.KEY_INPUT_UP) == 1)
                    y += 0.01f;
                if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) == 1)
                    y -= 0.01f;
                if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
                    x += 0.01f;
                if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
                    x -= 0.01f;


                DX.ClearDrawScreen();

                DX.SetCameraPositionAndAngle(DX.VGet(x,y,z), 0, 0, 0);
                c.Draw(hdl);
                DX.ScreenFlip();

            }




            DX.WaitKey();

        }
    }


}