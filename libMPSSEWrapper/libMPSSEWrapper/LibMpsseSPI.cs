using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper
{
    public class LibMpsseSpi
    {


        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_OpenChannel(int index, out IntPtr handle);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_CloseChannel(IntPtr handle);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_GetNumChannels(out int numChannels);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_GetChannelInfo(int index, out FtDeviceInfo chanInfo);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_InitChannel(IntPtr handle, ref FtChannelConfig config);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_ChangeCS(IntPtr handle, FtConfigOptions configOptions);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_IsBusy(IntPtr handle, out bool state);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_Read(
            IntPtr handle,
            byte[] buffer,
            int sizeToTransfer,
            out int sizeTransfered,
            FtSpiTransferOptions options);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_ReadWrite(
            IntPtr handle,
            byte[] inBuffer,
            byte[] outBuffer,
            int sizeToTransfer,
            out int sizeTransferred,
            FtSpiTransferOptions transferOptions);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_Write(
            IntPtr handle,
            byte[] buffer,
            int sizeToTransfer,
            out int sizeTransfered,
            FtSpiTransferOptions options);

    }
}
