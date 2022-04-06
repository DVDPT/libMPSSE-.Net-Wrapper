using System;
using System.Diagnostics;
using libMPSSEWrapper;
using libMPSSEWrapper.I2c;
using libMPSSEWrapper.Types;

namespace Test
{
    class Program
    {
        private const int ConnectionSpeed = 2000; // Hz
        private const int LatencyTimer = 255; // Hz

        static void Main(string[] args)
        {
            /*
             * I2C Example
             */
            var adcI2cConfig = new FtI2cChannelConfig
            {
                ClockRate = FtI2cClockRate.I2C_CLOCK_FAST_MODE,
                LatencyTimer = LatencyTimer
            };

            var i2cConfig = new FtI2cConfiguration(Ads1115.ADS_ADDR_SDA, 0);

            var adcConfig = new Ads1115Configuration()
            {
                Gain = Ads1115.Gain.One
            };
            
            var adc = new Ads1115(adcConfig, adcI2cConfig, i2cConfig);
            do
            {
                Console.WriteLine("Channel 0: " + adc.GetConvertedSampleFrom(Ads1115.Channel.Channel0));
                Console.WriteLine("Channel 1: " + adc.GetConvertedSampleFrom(Ads1115.Channel.Channel1));
                Console.WriteLine("Channel 2: " + adc.GetConvertedSampleFrom(Ads1115.Channel.Channel2));
            } while (true);

            /*
             *  SPI Example 
             *
            var adcSpiConfig = new FtSpiChannelConfig
                             {
                                 ClockRate = ConnectionSpeed,
                                 LatencyTimer = LatencyTimer,
                                 configOptions = FtConfigOptions.Mode0 | FtConfigOptions.CsDbus3 | FtConfigOptions.CsActivelow
                             };

          
            
            var adcConfig = new Maxim186Configuration
                                {
                                    Channel = Maxim186.Channel.Channel0,
                                    ConversionType = Maxim186.ConversionType.SingleEnded,
                                    Polarity = Maxim186.Polarity.Unipolar,
                                    PowerMode = Maxim186.PowerMode.InternalClockMode
                                };

           

            var adc = new Maxim186(adcConfig, adcSpiConfig);

            do
            {
                Console.WriteLine(adc.GetConvertedSample());

               

            } while (true);
            //*/
        }
    }
}
