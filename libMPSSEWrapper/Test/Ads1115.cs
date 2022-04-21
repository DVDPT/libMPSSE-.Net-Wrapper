using System;
using System.Diagnostics;
using System.Threading;
using libMPSSEWrapper.I2c;
using libMPSSEWrapper.Spi;
using libMPSSEWrapper.Types;

namespace Test
{
    public class Ads1115Configuration
    {
        public Ads1115.Gain Gain;
    }
    
    public class Ads1115: I2cDevice
    {
        private Ads1115Configuration _adcConfig;

        private static ushort ADS1015_REG_CONFIG_CQUE_NONE = 0x0003;
        private static ushort ADS1015_REG_CONFIG_CLAT_NONLAT = 0x0000;
        private static ushort ADS1015_REG_CONFIG_CPOL_ACTVLOW = 0x0000;
        private static ushort ADS1015_REG_CONFIG_CMODE_TRAD = 0x0000;
        private static ushort ADS1015_REG_CONFIG_DR_1600SPS = 0x0080;
        private static ushort ADS1015_REG_CONFIG_MODE_SINGLE = 0x0100;
        private static ushort ADS1015_REG_CONFIG_OS_SINGLE = 0x8000;

        private static ushort ADS1015_REG_CONFIG_MUX_SINGLE_0 = 0x4000;
        private static ushort ADS1015_REG_CONFIG_MUX_SINGLE_1 = 0x5000;
        private static ushort ADS1015_REG_CONFIG_MUX_SINGLE_2 = 0x6000;
        private static ushort ADS1015_REG_CONFIG_MUX_SINGLE_3 = 0x7000;

        private static ushort ADS1115_CONVERSIONDELAY = 8;
        
        public static byte ADS_ADDR_SDA = 0x4A;
        private static byte ADS1015_REG_POINTER_CONVERT = 0x00;
        private static byte ADS1015_REG_POINTER_CONFIG = 0x01;

        private static ushort GAIN_ONE = 0x0200;
        private static double batt_voltageConv = 4.096 / 32768.0;
        
        [Flags]
        public enum Gain : int{
            TwoThirds = 0x0000,
            One = 0x0200,
            Two = 0x0400,
            Four = 0x0600,
            Eight = 0x0800,
            Sixteen = 0x0A00
        }
        
        [Flags]
        public enum Channel
        {
            Channel0 = 0,
            Channel1 = 4,
            Channel2 = 1,
            Channel3 = 5
        }
        
        public Ads1115(Ads1115Configuration adcConfig, FtI2cChannelConfig config) : this(adcConfig, config, null)
        {
        }

        public Ads1115(Ads1115Configuration adcConfig, FtI2cChannelConfig config, FtI2cConfiguration ftI2CConfig) : base(config, ftI2CConfig)
        {
            _adcConfig = adcConfig;
        }
        
        public double GetConvertedSampleFrom(Channel channel)
        {
            var sizeTransfered = 0;
            var data = new byte[2];

            FtI2cTransferOptions writeOptions = FtI2cTransferOptions.StartBit | FtI2cTransferOptions.StopBit;
            var control = GetAdcControlWord(channel);
            var status = DeviceWrite(_ftI2CConfig.Address, new[] { ADS1015_REG_POINTER_CONFIG, (byte)(control >> 8), (byte)(control & 0xFF) }, out sizeTransfered, writeOptions);
            Debug.Assert(status == FtResult.Ok);
            Debug.Assert(sizeTransfered == 3);

            status = DeviceWrite(_ftI2CConfig.Address, new[] { ADS1015_REG_POINTER_CONVERT }, out sizeTransfered, writeOptions);
            Debug.Assert(status == FtResult.Ok);
            
            Thread.Sleep(ADS1115_CONVERSIONDELAY);
            
            FtI2cTransferOptions readOptions = FtI2cTransferOptions.StartBit | FtI2cTransferOptions.StopBit | FtI2cTransferOptions.NackLastByte;
            status = DeviceRead(_ftI2CConfig.Address, data, out sizeTransfered, readOptions);
            Debug.Assert(status == FtResult.Ok);
            Debug.Assert(sizeTransfered == data.Length);

            return (data[0] << 8 | data[1]) * batt_voltageConv;
        }
        
        private int GetAdcControlWord(Channel? channel = null)
        {
            int config =
                ADS1015_REG_CONFIG_CQUE_NONE 	|   	// Disable the comparator (default val)
                ADS1015_REG_CONFIG_CLAT_NONLAT 	|  	// Non-latching (default val)
                ADS1015_REG_CONFIG_CPOL_ACTVLOW | 	// Alert/Rdy active low   (default val)
                ADS1015_REG_CONFIG_CMODE_TRAD 	| 	// Traditional comparator (default val)
                ADS1015_REG_CONFIG_DR_1600SPS 	| 	// 1600 samples per second (default)
                ADS1015_REG_CONFIG_MODE_SINGLE;   	// Single-shot mode (default)

            config |= (int) _adcConfig.Gain;
            switch (channel) {
                case (Channel.Channel0):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_0;
                    break;
                case (Channel.Channel1):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_1;
                    break;
                case (Channel.Channel2):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_2;
                    break;
                case (Channel.Channel3):
                    config |= ADS1015_REG_CONFIG_MUX_SINGLE_3;
                    break;
            }
            config |= ADS1015_REG_CONFIG_OS_SINGLE;
            return config;
        }
    }
}