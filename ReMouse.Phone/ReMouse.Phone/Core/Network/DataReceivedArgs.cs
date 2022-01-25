using System;

namespace ReMouse.Phone.Core.Network
{
    public class DataReceivedArgs : EventArgs
    {
        public byte[] Buffer { get; }
        public int Offset { get; }
        public int Length { get; }

        public DataReceivedArgs(byte[] buffer, int offset, int length)
        {
            Buffer = buffer;
            Offset = offset;
            Length = length;
        }
    }
}
