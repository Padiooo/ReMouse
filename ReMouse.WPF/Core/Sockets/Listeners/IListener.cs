using ReMouse.WPF.Core.Sockets.Clients;
using System;

namespace ReMouse.WPF.Core.Sockets.Listeners
{
    public interface IListener
    {
        event EventHandler<IClient> OnClientConnect;

        string Name { get; }

        void Start();
        void Stop();
    }
}
