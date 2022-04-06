using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace libMPSSEWrapper.Types
{
    public enum FtI2cClockRate
    {
        I2C_CLOCK_STANDARD_MODE = 100000,							// 100kb/sec
        I2C_CLOCK_FAST_MODE = 400000,								// 400kb/sec
        I2C_CLOCK_FAST_MODE_PLUS = 1000000, 						// 1000kb/sec
        I2C_CLOCK_HIGH_SPEED_MODE = 3400000 					    // 3.4Mb/sec
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct FtI2cChannelConfig
    {
        public FtI2cClockRate ClockRate;
        public byte LatencyTimer;
        public UInt32 configOptions;
        public UInt32 Pin;
        public UInt16 reserved;
    }
}
