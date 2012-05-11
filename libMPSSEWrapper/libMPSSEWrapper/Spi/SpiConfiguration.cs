using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libMPSSEWrapper.Spi
{
    public class SpiConfiguration
    {
        public static readonly SpiConfiguration ChannelZeroConfiguration = new SpiConfiguration(0);

        public int ChannelIndex { get; private set; }

        public SpiConfiguration(int channelIndex)
        {
            ChannelIndex = channelIndex;
        }

    }
}
