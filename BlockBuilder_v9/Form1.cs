using DxLibDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockBuilder_v9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            DXLibInit();

        }

        private void DXLibInit()
        {
            DX.SetWaitVSyncFlag(0);
            DX.SetUserWindow(this.Handle);
            DX.SetWindowSize(1280, 720);
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.SetUseZBuffer3D(1);
            DX.SetWriteZBuffer3D(1);
            DX.SetTransColor(0, 255, 0);
            DX.SetCameraNearFar(0.1f, 255);
            DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
            DX.SetupCamera_Perspective((float)(Math.PI / 180 * 70));
            DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
           float s = 128;
           float e = 256;
            DX.SetFogEnable(1);
            DX.SetFogStartEnd(s, e);
            DX.SetFogColor(0,0, 0);
        }
        Player p = new Player();

        World w = new World();
        int hdl;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            w.LoadWorld();

            DX.SetCameraPositionAndAngle(DX.VGet(0, 0, 0), 0, 1.6f, 0);
            hdl = DX.LoadGraph("TNT.png");

            timer1.Interval = 1000 / 60;
            timer1.Start();
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            DX.ClearDrawScreen();

            int cx = (int)(p.x / 16);
            int cz = (int)(p.z / 16);
            int x = (int)((p.x % 16));
            int z = (int)((p.z % 16));

            p.PalyerMove(w);



         

            DX.SetCameraPositionAndAngle(DX.VGet(p.x-0.5f, p.y+1, p.z-0.5f), p.rot.ax, p.rot.ay, p.rot.az);

         





            foreach (var c in w.chunkArrey)
                c.Draw(hdl);


            DX.DrawString(0, 0, cx + "," + x,DX.GetColor(0,255,0));
            DX.DrawString(0, 16, cz + "," + z,DX.GetColor(0,255,0));
            DX.DrawString(0, 16 * 3, "speed=" + p.speed.ToString(), DX.GetColor(0, 255, 0));

            DX.DrawString(0, 16*4, "x="+p.x.ToString(), DX.GetColor(0, 255, 0));
            DX.DrawString(0, 16*5, "y="+p.y.ToString(), DX.GetColor(0, 255, 0));
            DX.DrawString(0, 16*6, "z=" + p.z.ToString(), DX.GetColor(0, 255, 0));
            DX.ScreenFlip();
        }
    }
}

