using ReMouse.Phone.Core.Network;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ReMouse.Phone.MVVM.Models
{
    public class SocketModel : ObservableObject, ISocket
    {
        public const string ResourceKey = "";

        private bool connected;
        public bool Connected
        {
            get => connected;
            private set => SetProperty(ref connected, value);
        }

        private ConnectionStatus connectionStatus = ConnectionStatus.DISCONNECTED;
        public ConnectionStatus ConnectionStatus
        {
            get => connectionStatus;
            set => SetProperty(ref connectionStatus, value);
        }

        public event EventHandler<bool> ConnectionChanged;
        public event EventHandler<DataReceivedArgs> DataReceived;

        private ISocket socket;

        public SocketModel() { }

        public Task SetSocketAsync(ISocket s)
        {
            return Task.Run(async () =>
            {
                if (socket != null)
                {
                    socket.DataReceived -= Socket_DataReceived;
                    socket.ConnectionChanged -= Socket_ConnectionChanged;
                    await socket.DisconnectAsync();
                }

                if (s == null)
                {
                    ConnectionStatus = ConnectionStatus.DISCONNECTED;
                    Connected = false;
                    return;
                }

                socket = s;

                socket.DataReceived += Socket_DataReceived;
                socket.ConnectionChanged += Socket_ConnectionChanged;
                ConnectionStatus = socket.Connected ? ConnectionStatus.CONNECTED : ConnectionStatus.DISCONNECTED;
                Connected = socket.Connected;
            });
        }

        private void Socket_ConnectionChanged(object sender, bool connected)
        {
            Connected = connected;
            ConnectionStatus = connected ? ConnectionStatus.CONNECTED : ConnectionStatus.DISCONNECTED;
            ConnectionChanged?.Invoke(this, connected);
        }

        private void Socket_DataReceived(object sender, DataReceivedArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        public async Task DisconnectAsync()
        {
            await socket.DisconnectAsync();
        }

        public async Task SendAsync(byte[] buffer, int offset, int length)
        {
            await socket.SendAsync(buffer, offset, length);
        }
    }
}
