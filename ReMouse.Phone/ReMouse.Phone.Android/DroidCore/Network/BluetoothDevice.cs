using Android.Bluetooth;
using System;
using System.Threading.Tasks;
using ReMouse.Phone.Core.Network;
using ReMouse.Shared;

namespace ReMouse.Phone.Droid.DroidCore.Network
{
    public class BluetoothDevice : ISocket
    {
        public string Name { get; }

        public bool Connected => socket != null && socket.IsConnected;

        public event EventHandler<bool> ConnectionChanged;
        public event EventHandler<DataReceivedArgs> DataReceived;

        private readonly Android.Bluetooth.BluetoothDevice _device;
        private BluetoothSocket socket;
        private byte[] buffer;

        public BluetoothDevice(Android.Bluetooth.BluetoothDevice device)
        {
            _device = device;
            Name = device.Name;
        }

        public async Task ConnectAsync()
        {
            socket = _device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(Constants.BluetoothService));
            await socket.ConnectAsync();

            if (socket != null && socket.IsConnected)
            {
                ConnectionChanged?.Invoke(this, socket.IsConnected);
                buffer = new byte[256];
                socket.InputStream.BeginRead(buffer, 0, buffer.Length, ReceiveAsync, null);
            }
        }

        private async void ReceiveAsync(IAsyncResult ar)
        {
            try
            {
                int received = socket.InputStream.EndRead(ar);

                if (received != 0)
                {
                    DataReceivedArgs args = new DataReceivedArgs(buffer, 0, received);
                    DataReceived?.Invoke(this, args);

                    socket.InputStream.BeginRead(buffer, 0, buffer.Length, ReceiveAsync, null);
                }
                else
                    await DisconnectAsync();
            }
            catch (Exception e)
            {
                await DisconnectAsync();
            }
        }

        public Task DisconnectAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    socket.Close();
                    socket.Dispose();
                }
                catch (Exception) { }
                finally
                {
                    socket = null;
                    ConnectionChanged?.Invoke(this, false);
                }
            });
        }

        public async Task SendAsync(byte[] buffer, int offset, int length)
        {
            System.IO.Stream outputStream = socket.OutputStream;
            await outputStream.WriteAsync(buffer, offset, length);
            await outputStream.FlushAsync();
        }
    }
}