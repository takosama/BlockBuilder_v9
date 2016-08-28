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
        [STAThread]
        static void Main(string[] args)
        {
            //   DXLibInit();
            Form1 f = new Form1();
            f.ShowDialog();
        }
    }

}