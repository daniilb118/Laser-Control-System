using NullFX.CRC;

namespace laserControl
{
    /// <summary>
    /// a data unit of IOPort protocol implementation
    /// </summary>
    public class Frame
    {
        public static int HeaderSize => 4;
        public byte thisACK;
        public byte thatACK;
        public byte isAuxiliary;
        public byte checksum;
        public byte[] message;

        public Frame(byte thisACK, byte thatACK, byte isAuxiliary, byte[] message)
        {
            this.thisACK = thisACK;
            this.thatACK = thatACK;
            this.isAuxiliary = isAuxiliary;
            this.message = message;
            setChecksum();
        }

        public Frame(byte[] frameBytes)
        {
            if (frameBytes.Length < HeaderSize)
            {
                throw new ArgumentException($"Unexpected frame size. Got {frameBytes.Length} bytes instead of the minimum size: HeaderSize ({HeaderSize}) + message size (1) = {HeaderSize + 1} bytes.");
            }
            int messageSize = frameBytes.Length - HeaderSize;
            message = new byte[messageSize];
            thisACK = frameBytes[messageSize + 0];
            thatACK = frameBytes[messageSize + 1];
            isAuxiliary = frameBytes[messageSize + 2];
            checksum = frameBytes[messageSize + 3];
            Array.Copy(frameBytes, message, messageSize);
        }

        public bool IsValid => Crc8.ComputeChecksum(GetBytes()) == 0;

        public byte[] GetBytes() => [.. message, thisACK, thatACK, isAuxiliary, checksum];

        private void setChecksum()
        {
            checksum = Crc8.ComputeChecksum(GetBytes(), 0, HeaderSize + message.Length - 1);
        }
    }
}
