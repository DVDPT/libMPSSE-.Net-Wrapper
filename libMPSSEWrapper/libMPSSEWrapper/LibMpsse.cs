using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper
{
    public class LibMpsse
    {
        private static int _initializations = 0;

        public const string DllName = "libMPSSE.dll";

        public static void Init()
        {
            if(Interlocked.Increment(ref _initializations) == 1)
                Init_libMPSSE();

        }

        public static void Cleanup()
        {
            if(Interlocked.Decrement(ref _initializations) == 0)
                Cleanup_libMPSSE();
        }

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Init_libMPSSE();

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Cleanup_libMPSSE();
    }
}
