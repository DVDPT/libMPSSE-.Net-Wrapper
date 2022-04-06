using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libMPSSEWrapper.Types
{
    [Flags]
    public enum FtI2cTransferOptions : int
    {
        /// <summary>
        /// Options to I2C_DeviceWrite & I2C_DeviceRead
        /// Generate start condition before transmitting
        /// </summary>
        StartBit = 0x00000001,
        
        /// <summary>
        /// Generate stop condition before transmitting
        /// </summary>
        StopBit = 0x00000002,
        
        /// <summary>
        /// Continue transmitting data in bulk without caring about Ack or nAck from device if this bit is
        /// not set. If this bit is set then stop transitting the data in the buffer when the device nAcks
        /// </summary>
        BreakOnNack = 0x00000004,
        
        /// <summary>
        /// libMPSSE-I2C generates an ACKs for every byte read. Some I2C slaves require the I2C
        /// master to generate a nACK for the last data byte read. Setting this bit enables working with such
        /// I2C slaves
        /// </summary>
        NackLastByte = 0x00000008,
        
        /// <summary>
        /// no address phase, no USB interframe delays
        /// </summary>
        FastTransferBytes = 0x00000010,
        FastTransferBits = 0x00000020,
        FastTransfer = 0x00000030,
        
        /// <summary>
        /// if I2C_TRANSFER_OPTION_FAST_TRANSFER is set then setting this bit would mean that the
        /// address field should be ignored. The address is either a part of the data or this is a special I2C
        /// frame that doesn't require an address
        /// </summary>
        NoAddress = 0x00000040
    }
}
