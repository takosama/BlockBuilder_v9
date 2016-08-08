using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuilder_v9
{
    class MyLight
    {
        public static readonly DX.COLOR_U8[] LightLevelArrey = CreateLightLevel();
        public static readonly DX.COLOR_U8 AmbientLightLevel = DX.GetColorU8(40, 40, 40, 40);
        static DX.COLOR_U8[] CreateLightLevel()
        {
            int[] Level = { 63, 75, 87, 99, 111, 123, 135, 147, 159, 171, 183, 195, 207, 219, 231, 243, 255 };

            DX.COLOR_U8[] rtn = Level.Select(x => DX.GetColorU8(x, x, x, x)).ToArray();
            return rtn;
        }
    }
}
