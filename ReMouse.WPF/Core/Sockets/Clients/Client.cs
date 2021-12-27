using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ReMouse.WPF.Core.Sockets.Clients
{
    public class Client : IClient
    {
        public event Action<byte[], int> OnDataReceived;
        public event Action OnDisconnect;

        public bool Connected { get; private set; }
        public string Name { get; private set; }

        private readonly Socket _socket;
        private readonly byte[] _buffer;

        public Client(Socket socket, string name)
        {
            Name = name;
            _buffer = new byte[64];
            _socket = socket;
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveAsync, null);
        }

        private void ReceiveAsync(IAsyncResult ar)
        {
            try
            {
                int received = _socket.EndReceive(ar);
                if (received != 0)
                {
                    OnDataReceived?.Invoke(_buffer, received);

                    _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveAsync, null);
                }
                else
                {
                    Disconnect();
                }
            }
            catch
            {
                Disconnect();
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                _socket.Send(data);
            }
            catch (Exception e)
            {

            }
        }

        public void Disconnect()
        {
            try
            {
                OnDisconnect?.Invoke();
                _socket.Close();
                _socket.Dispose();
            }
            catch
            {

            }
        }
    }
}
