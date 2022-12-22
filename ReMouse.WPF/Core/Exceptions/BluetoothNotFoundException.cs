using System;

namespace ReMouse.WPF.Core.Exceptions
{
    public class BluetoothNotFoundException : Exception
    {
        public BluetoothNotFoundException() : base("Bluetooth radio not found or disabled.")
        {

        }
    }
}
