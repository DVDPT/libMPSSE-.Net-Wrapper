using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace libMPSSEWrapper.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FtChannelConfig
    {
        public int ClockRate;
        public byte LatencyTimer;
        public FtConfigOptions configOptions;
        public int Pin;
        public short reserved;
    }
}
