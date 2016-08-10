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
            DX.ChangeWindowMode(1)
                ;
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
            Chunk[] ca = new Chunk[256];

            int count = 0;
            int k = 0;

            for (int x_i = 0; x_i < 16; x_i++)
            {
                for (int z_i = 0; z_i < 16; z_i++)
                {
                    Chunk c = new Chunk(x_i * 16, z_i * 16);
                    c.GenerateChunk();
                    c.Refresh();
                    ca[count] = c;
                    count++;

                }
            }
            /*
            while (true)
            {
             //   GC.WaitForFullGCComplete();
                ca[0] = new Chunk(0, 0);
                ca[0].GenerateChunk();
                ca[0].Refresh();
                ca[0].Dispose();
              
            }
            */
                
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
                      DX.ClearDrawScreen();

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
                          //    c.Draw(hdl);
                          foreach (var n in ca)
                              n.Draw(hdl);

                      k++;
                    if (k ==1)
                      {
                          ca[0].SetBlock(1, 10, 1);
                          ca[0].Refresh();
                      }
                      if(k==2)
                      {
                          ca[0].DeleteBlock(1, 10, 1);
                          ca[0].Refresh();
                          k = 0;
                      }

                      if (IsPointMove)
                          {
                              DX.DrawString(0, 0, MX + "," + MY, DX.GetColor(0, 255, 0));
                          }
                          else
                          {
                              DX.DrawString(0, 0, "PointerFree", DX.GetColor(0, 255, 0));
                          }

                          DX.ScreenFlip();

                      }

          


            DX.WaitKey();

        }
    }


}