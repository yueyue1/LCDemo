using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LCDemo
{
    class LibWrap
    {
       [DllImport(("winmm.dll"), EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString
        (string lpszCommand, string lpszReturnString, uint cchReturn, int hwndCallback);
    }
}
