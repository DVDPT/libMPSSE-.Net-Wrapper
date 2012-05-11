using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper.Exceptions
{
    public class SpiChannelNotConnectedException : Exception
    {
        public FtResult Reason { get; private set; }

        public SpiChannelNotConnectedException(FtResult res)
        {
            Reason = res;
        }


    }
}
