using System;
using System.Diagnostics;
using libMPSSEWrapper.Spi;
using libMPSSEWrapper.Types;

namespace Test
{



    public class Maxim186Configuration
    {
        public Maxim186.Polarity Polarity { get; set; }
        public Maxim186.ConversionType ConversionType { get; set; }
        public Maxim186.PowerMode PowerMode { get; set; }
        public Maxim186.Channel Channel { get; set; }
    }


    public class Maxim186 : SpiDevice
    {
        private readonly Maxim186Configuration _adcConfig;
        [Flags]
        public enum Polarity
        {
            Bipolar = 0,
            Unipolar = 1
        }

        [Flags]
        public enum ConversionType
        {
            Differential = 0,
            SingleEnded = 1,
        }

        [Flags]
        public enum PowerMode
        {
            FullPowerDown = 0,
            FastPowerDown = 1,
            InternalClockMode = 2,
            ExternalClockMode = 3,
        }


        [Flags]
        public enum Channel
        {
            Channel0 = 0,
            Channel1 = 4,
            Channel2 = 1,
            Channel3 = 5,
            Channel4 = 2,
            Channel5 = 6,
            Channel6 = 3,
            Channel7 = 7,
        }


        public Maxim186(Maxim186Configuration adcConfig, FtChannelConfig config)
            : this(adcConfig, config, null)
        {
        }

        public Maxim186(Maxim186Configuration adcConfig, FtChannelConfig config, SpiConfiguration spiConfig)
            : base(config, spiConfig)
        {
            _adcConfig = adcConfig;
        }

        public int GetConvertedSample()
        {
            return GetConvertedSampleFrom(_adcConfig.Channel);
        }

        public int GetConvertedSampleFrom(Channel channel)
        {
            var sizeTransfered = 0;
            var data = new byte[2];

            Write(new[] { GetAdcControlWord(_adcConfig, channel) }, out sizeTransfered, FtSpiTransferOptions.ChipselectEnable);

            Debug.Assert(sizeTransfered == 1);

            Read(data, out sizeTransfered, FtSpiTransferOptions.ChipselectDisable);

            Debug.Assert(sizeTransfered == data.Length);

            return (data[0] << 8 | data[1]) >> 3;
        }


        private byte GetAdcControlWord(Maxim186Configuration config, Channel? channel = null)
        {
            int word = 0;
            channel = channel ?? config.Channel;
            word = 1 << 7; // Reserved Bit
            word |= (int)channel.Value << 4; // channel
            word |= (int)config.Polarity << 3;
            word |= (int)config.ConversionType << 2;
            word |= (int)config.PowerMode;

            return (byte)word;
        }
    }
}
