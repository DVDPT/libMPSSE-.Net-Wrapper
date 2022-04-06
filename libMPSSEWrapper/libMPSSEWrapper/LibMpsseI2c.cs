using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper
{
    public class LibMpsseI2c
    {
        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_OpenChannel(int index, out IntPtr handle);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_CloseChannel(IntPtr handle);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_GetNumChannels(out int numChannels);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_GetChannelInfo(int index, out FtDeviceInfo chanInfo);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_InitChannel(IntPtr handle, ref FtI2cChannelConfig config);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_DeviceRead(
            IntPtr handle,
            int deviceAddress,
            int sizeToTransfer,
            byte[] buffer,
            out int sizeTransfered,
            FtI2cTransferOptions options);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult I2C_DeviceWrite(
            IntPtr handle,
            int deviceAddress,
            int sizeToTransfer,
            byte[] buffer,
            out int sizeTransferred,
            FtI2cTransferOptions transferOptions);

    }
}
