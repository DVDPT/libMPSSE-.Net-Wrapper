using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper.Exceptions
{
    public class I2cChannelNotConnectedException : Exception
    {
        public FtResult Reason { get; private set; }

        public I2cChannelNotConnectedException(FtResult res)
        {
            Reason = res;
        }


    }
}
