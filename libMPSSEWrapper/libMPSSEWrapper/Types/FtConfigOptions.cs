using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libMPSSEWrapper.Types
{
    [Flags]
    public enum FtConfigOptions
    {

        Mode0 = 0x00000000,
        Mode1 = 0x00000001,
        Mode2 = 0x00000002,
        Mode3 = 0x00000003,


        CsDbus3 = 0x00000000, /*000 00*/
        CsDbus4 = 0x00000004, /*001 00*/
        CsDbus5 = 0x00000008, /*010 00*/
        CsDbus6 = 0x0000000C, /*011 00*/
        CsDbus7 = 0x00000010, /*100 00*/

        CsActivelow = 0x00000020,
    }
}
