using System;
using System.Runtime.InteropServices;

namespace Zip.Pdv.Helpers
{
    public class ImportExterno
    {
        [DllImport("user32.dll")]
        public static extern Boolean GetLastInputInfo(ref tagLASTINPUTINFO plii);
        public struct tagLASTINPUTINFO
        {
            public uint cbSize;
            public Int32 dwTime;
        }

    }
}
