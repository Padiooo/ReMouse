using System;

namespace ReMouse.Phone.Core.Network
{
    public interface IHandler
    {
        bool IsSupported { get; }
        bool IsAvailable { get; }

        event EventHandler<bool> AvailabilityChanged;

        void Refresh();
    }
}
