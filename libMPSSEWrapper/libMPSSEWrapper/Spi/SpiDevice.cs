using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libMPSSEWrapper.Exceptions;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper.Spi
{
    public abstract class SpiDevice : IDisposable
    {
        private static IntPtr _handle = IntPtr.Zero;
        private static FtChannelConfig _currentGlobalConfig;

        private FtChannelConfig _cfg;

        private bool _isDisposed;
        private SpiConfiguration _spiConfig;

        protected SpiDevice(FtChannelConfig config)
            : this(config, null)
        {

        }

        protected SpiDevice(FtChannelConfig config, SpiConfiguration spiConfig)
        {
            _spiConfig = spiConfig ?? SpiConfiguration.ChannelZeroConfiguration;
            _cfg = config;
            InitLibAndHandle();
        }

        private void InitLibAndHandle()
        {
            FtResult result;
            if (_handle != IntPtr.Zero)
                return;


            LibMpsse.Init();
            result = LibMpsseSpi.SPI_OpenChannel(_spiConfig.ChannelIndex, out _handle);

            CheckResult(result);

            if (_handle == IntPtr.Zero)
                throw new SpiChannelNotConnectedException(FtResult.InvalidHandle);

            result = LibMpsseSpi.SPI_InitChannel(_handle, ref _cfg);

            CheckResult(result);

            _currentGlobalConfig = _cfg;

        }

        protected FtResult Write(byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtSpiTransferOptions options)
        {
            EnforceRightConfiguration();

            return LibMpsseSpi.SPI_Write(_handle, buffer, sizeToTransfer, out sizeTransfered, options);
        }


        protected FtResult Write(byte[] buffer, out int sizeTransfered, FtSpiTransferOptions options)
        {
            return Write(buffer, buffer.Length, out sizeTransfered, options);
        }

        protected FtResult Write(byte[] buffer, int sizeToTransfer, out int sizeTransfered)
        {
            return Write(buffer, sizeToTransfer, out sizeTransfered, FtSpiTransferOptions.ToogleChipSelect);
        }

        protected FtResult Write(byte[] buffer, out int sizeTransfered)
        {
            return Write(buffer, out sizeTransfered, FtSpiTransferOptions.ToogleChipSelect);
        }

        protected FtResult Read(byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtSpiTransferOptions options)
        {
            EnforceRightConfiguration();
            return LibMpsseSpi.SPI_Read(_handle, buffer, sizeToTransfer, out sizeTransfered, options);
        }

        protected FtResult Read(byte[] buffer, out int sizeTransfered, FtSpiTransferOptions options)
        {
            return Read(buffer, buffer.Length, out sizeTransfered, options);
        }

        protected FtResult Read(byte[] buffer,int sizeToTransfer, out int sizeTransfered)
        {
            return Read(buffer, buffer.Length, out sizeTransfered, FtSpiTransferOptions.ToogleChipSelect);
        }

        protected FtResult Read(byte[] buffer, out int sizeTransfered)
        {
            return Read(buffer, out sizeTransfered, FtSpiTransferOptions.ToogleChipSelect);
        }


        protected static void CheckResult(FtResult result)
        {
            if (result != FtResult.Ok)
                throw new SpiChannelNotConnectedException(result);
        }

        private void EnforceRightConfiguration()
        {
            if (_currentGlobalConfig.configOptions != _cfg.configOptions)
            {
                LibMpsseSpi.SPI_ChangeCS(_handle, _cfg.configOptions);
                _currentGlobalConfig = _cfg;
            }
        }


        public void Dispose()
        {
            if (_isDisposed)
                return;


            _isDisposed = true;
            LibMpsse.Cleanup();
        }
    }
}
