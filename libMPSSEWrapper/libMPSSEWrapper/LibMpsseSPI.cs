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
        public extern static FtResult SPI_InitChannel(IntPtr handle, ref FtChannelInfo config);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_ChangeCS(IntPtr handle, int configOptions);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static FtResult SPI_IsBusy(IntPtr handle, out bool state);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SPI_Read(
            IntPtr handle,
            byte[] buffer,
            int sizeToTransfer,
            out int sizeTransfered,
            int options);

        /*
         FTDI_API FT_STATUS SPI_Write(FT_HANDLE handle, uint8 *buffer, 	uint32 sizeToTransfer, uint32 *sizeTransfered, uint32 options);
         * 
FTDI_API FT_STATUS SPI_ReadWrite(FT_HANDLE handle, uint8 *inBuffer, 	uint8 *outBuffer, uint32 sizeToTransfer, uint32 *sizeTransferred, 	uint32 transferOptions);
         */

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SPI_ReadWrite(
            IntPtr handle,
            byte[] inBuffer,
            byte[] outBuffer,
            int sizeToTransfer,
            out int sizeTransferred,
            int transferOptions);

        [DllImport(LibMpsse.DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SPI_Write(
            IntPtr handle,
            byte[] buffer,
            int sizeToTransfer,
            out int sizeTransfered,
            int options);

    }
}
