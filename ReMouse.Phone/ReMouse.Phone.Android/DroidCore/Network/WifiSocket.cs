using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ReMouse.Phone.Core.Network;

namespace ReMouse.Phone.Droid.DroidCore.Network
{
    public class WifiSocket : ISocket
    {
        public bool Connected => _socket != null && _socket.Connected;

        public event EventHandler<bool> ConnectionChanged;
        public event EventHandler<DataReceivedArgs> DataReceived;

        private readonly Socket _socket;
        private readonly byte[] _buffer;

        public WifiSocket(Socket socket)
        {
            _socket = socket;
            _buffer = new byte[256];
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveAsync, null);
        }

        private void ReceiveAsync(IAsyncResult ar)
        {
            try
            {
                int received = _socket.EndReceive(ar);
                if (received != 0)
                {
                    DataReceivedArgs args = new DataReceivedArgs(_buffer, 0, received);
                    DataReceived?.Invoke(this, args);

                    _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveAsync, null);
                }
            }
            catch (Exception)
            {
                DisconnectAsync().RunSynchronously();
            }
        }

        public Task DisconnectAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    _socket.Disconnect(false);
                    _socket.Close();
                    _socket.Dispose();
                }
                catch (Exception) { }
                finally
                {
                    ConnectionChanged?.Invoke(this, false);
                }
            });
        }

        public Task SendAsync(byte[] buffer, int offset, int length)
        {
            return Task.Run(() =>
            {
                _socket.Send(buffer, offset, length, SocketFlags.None);
            });
        }
    }
}