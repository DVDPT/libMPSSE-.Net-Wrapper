namespace libMPSSEWrapper.I2c
{
    public class FtI2cConfiguration
    {
        public static FtI2cConfiguration ChannelZeroConfiguration = new FtI2cConfiguration(0, 0);
        
        public int Address { get; private set; }
        
        public int ChannelIndex { get; private set; }

        public FtI2cConfiguration(int addess, int channelIndex)
        {
            Address = addess;
            ChannelIndex = channelIndex;
        }

    }
}
