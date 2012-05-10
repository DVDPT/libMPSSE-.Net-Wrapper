using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using libMPSSEWrapper;
using libMPSSEWrapper.Types;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int channel;
            bool busy;
            IntPtr channelPtr;
            FtChannelInfo info = new FtChannelInfo();
            info.ClockRate = 2000;
            info.LatencyTimer = 255;
            info.configOptions = 0x20;
            var arr = new byte[2];
           
            int sizeTransfered;

            LibMpsse.Init_libMPSSE();
            LibMpsseSpi.SPI_OpenChannel(0, out channelPtr);
            Console.WriteLine(channelPtr); arr[0] = 0x8e;

            LibMpsseSpi.SPI_InitChannel(channelPtr, ref info);

            do
            {
                arr[0] = 0x8e;
             
                LibMpsseSpi.SPI_Write(channelPtr, arr, 1, out sizeTransfered, 0x2);


                Debug.Assert(sizeTransfered == 1);


                LibMpsseSpi.SPI_Read(channelPtr, arr, 2, out sizeTransfered, 0x4);

                Debug.Assert(sizeTransfered == 2);

                int val = (arr[0] << 8 | arr[1]) >> 3;
                Console.WriteLine(val);

            } while (true);

        }
    }
}
