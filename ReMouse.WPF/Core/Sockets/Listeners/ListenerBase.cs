using ReMouse.WPF.Core.Sockets.Clients;
using System;

namespace ReMouse.WPF.Core.Sockets.Listeners
{
    public abstract class ListenerBase : IListener
    {
        public string Name { get; protected set; }

        public event EventHandler<IClient> OnClientConnect;

        private bool started;
        private bool autoAccept;
        private bool Accept => started && autoAccept;

        public void Start()
        {
            if (started)
                return;

            started = true;
            autoAccept = true;

            StartListener();
        }

        protected void ProcessClientConnect(IClient client)
        {
            client.OnDisconnect += Client_OnDisconnect;

            if (Accept)
            {
                autoAccept = false;
                OnClientConnect?.Invoke(this, client);
            }
            else
                client.Disconnect();
        }

        private void Client_OnDisconnect()
        {
            autoAccept = true;
        }

        public void Stop()
        {
            if (!started)
                return;

            started = false;

            StopListener();
        }

        protected abstract void StopListener();
        protected abstract void StartListener();
    }
}
