using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libMPSSEWrapper.Types
{
    public enum FtResult
    {
        Ok = 0,
        InvalidHandle,
        DeviceNotFound,
        DeviceNotOpened,
        IoError,
        InsufficientResources,
        InvalidParameter,
        InvalidBaudRate,
    }
}
