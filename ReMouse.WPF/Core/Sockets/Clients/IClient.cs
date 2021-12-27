using System;

namespace ReMouse.WPF.Core.Sockets.Clients
{
    public interface IClient
    {
        event Action<byte[], int> OnDataReceived;
        event Action OnDisconnect;

        bool Connected { get; }
        string Name { get; }

        void Disconnect();
        void Send(byte[] data);
    }
}
