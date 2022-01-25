using System;
using System.Linq;
using System.Net.Sockets;
using Xamarin.Essentials;
using Xamarin.Forms;
using ReMouse.Phone.Core.Network;
using ReMouse.Shared;
using System.Net;
using System.Threading.Tasks;

[assembly: Dependency(typeof(ReMouse.Phone.Droid.DroidCore.Network.WifiHandler))]

namespace ReMouse.Phone.Droid.DroidCore.Network
{
    public class WifiHandler : IWifiHandler
    {
        public bool IsSupported { get; }

        private bool isAvailable;
        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                if (isAvailable == value)
                    return;
                isAvailable = value;
                AvailabilityChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool> AvailabilityChanged;

        public WifiHandler()
        {
            IsSupported = true;

            if (!IsSupported)
                return;

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            CheckWifis();
        }

        private void CheckWifis()
        {
            IsAvailable = Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) || WifiApStateReceiver.WifiAP_Enabled;
        }

        ~WifiHandler()
        {
            if (!IsSupported)
                return;

            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckWifis();
        }

        public void Refresh()
        {
            CheckWifis();
        }

        public Task<ISocket> ConnectAsync(string ip)
        {
            if (ip == null)
                return null;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), Constants.WifiPort);
            Socket s = new Socket(endPoint.AddressFamily, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            try
            {
                s.Connect(endPoint);
            }
            catch (Exception e)
            {

            }

            return s.Connected ? Task.FromResult((ISocket)new WifiSocket(s)) : Task.FromResult((ISocket)null);
        }
    }
}