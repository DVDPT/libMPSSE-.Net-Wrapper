using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace libMPSSEWrapper
{
    public class LibMpsse
    {
        public const string DllName = "libMPSSE.dll";

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static void Init_libMPSSE();

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static void Cleanup_libMPSSE();
    }
}
