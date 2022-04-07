using System;
using libMPSSEWrapper.Exceptions;
using libMPSSEWrapper.Spi;
using libMPSSEWrapper.Types;

namespace libMPSSEWrapper.I2c
{
    public abstract class I2cDevice : IDisposable
    {
        private static IntPtr _handle = IntPtr.Zero;
        private static FtI2cChannelConfig _currentGlobalConfig;

        private FtI2cChannelConfig _cfg;

        private bool _isDisposed;
        protected readonly FtI2cConfiguration _ftI2CConfig;

        protected I2cDevice(FtI2cChannelConfig config)
            : this(config, null)
        {
        }

        protected I2cDevice(FtI2cChannelConfig config, FtI2cConfiguration ftI2CConfig)
        {
            _ftI2CConfig = ftI2CConfig ?? FtI2cConfiguration.ChannelZeroConfiguration;
            _cfg = config;
            InitLibAndHandle();
        }

        private void InitLibAndHandle()
        {
            FtResult result;
            if (_handle != IntPtr.Zero)
                return;

            LibMpsse.Init();
            result = LibMpsseI2c.I2C_OpenChannel(_ftI2CConfig.ChannelIndex, out _handle);

            CheckResult(result);

            if (_handle == IntPtr.Zero)
                throw new I2cChannelNotConnectedException(FtResult.InvalidHandle);

            result = LibMpsseI2c.I2C_InitChannel(_handle, ref _cfg);

            CheckResult(result);

            _currentGlobalConfig = _cfg;
        }

        protected FtResult DeviceWrite(int deviceAddress, byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtI2cTransferOptions options)
        {
            EnforceRightConfiguration();
            return LibMpsseI2c.I2C_DeviceWrite(_handle, deviceAddress, sizeToTransfer, buffer, out sizeTransfered, options);
        }


        protected FtResult DeviceWrite(int deviceAddress, byte[] buffer, out int sizeTransfered, FtI2cTransferOptions options)
        {
            return DeviceWrite(deviceAddress, buffer, buffer.Length, out sizeTransfered, options);
        }

        protected FtResult DeviceRead(int deviceAddress, byte[] buffer, int sizeToTransfer, out int sizeTransfered, FtI2cTransferOptions options)
        {
            EnforceRightConfiguration();
            return LibMpsseI2c.I2C_DeviceRead(_handle, deviceAddress, sizeToTransfer, buffer, out sizeTransfered, options);
        }

        protected FtResult DeviceRead(int deviceAddress, byte[] buffer, out int sizeTransfered, FtI2cTransferOptions options)
        {
            return DeviceRead(deviceAddress, buffer, buffer.Length, out sizeTransfered, options);
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
