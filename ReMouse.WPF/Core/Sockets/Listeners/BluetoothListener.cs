using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using ReMouse.Shared;
using ReMouse.WPF.Core.Exceptions;
using ReMouse.WPF.Core.Sockets.Clients;
using System;

namespace ReMouse.WPF.Core.Sockets.Listeners
{
    public class BluetoothListener : ListenerBase
    {
        private InTheHand.Net.Sockets.BluetoothListener listener;

        public BluetoothListener()
        {
            CheckForRadio();

            Name = BluetoothRadio.PrimaryRadio.Name;
        }

        protected override void StartListener()
        {
            CheckForRadio();

            listener = new InTheHand.Net.Sockets.BluetoothListener(Guid.Parse(Constants.BluetoothService));
            listener.Start();
            listener.BeginAcceptBluetoothClient(AcceptAsync, null);
        }

        private void CheckForRadio()
        {
            if (BluetoothRadio.PrimaryRadio == null)
                throw new BluetoothNotFoundException();
        }

        private void AcceptAsync(IAsyncResult ar)
        {
            BluetoothClient client;
            try
            {
                client = listener.EndAcceptBluetoothClient(ar);
            }
            catch
            {
                return;
            }

            IClient c = new Client(client.Client, client.RemoteMachineName);

            ProcessClientConnect(c);

            listener.BeginAcceptBluetoothClient(AcceptAsync, null);
        }

        protected override void StopListener()
        {
            listener?.Stop();
            listener = null;
        }
    }
}
