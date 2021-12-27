using ReMouse.Shared;
using ReMouse.WPF.Core.Exceptions;
using ReMouse.WPF.Core.Sockets.Clients;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ReMouse.WPF.Core.Sockets.Listeners
{
    public class WifiListener : ListenerBase
    {
        private TcpListener listener;

        public WifiListener()
        {
            CheckEndPoint();
        }

        protected override void StartListener()
        {
            listener = new TcpListener(CheckEndPoint());
            listener.Start(1);
            listener.BeginAcceptSocket(AcceptAsync, null);
        }

        private IPEndPoint CheckEndPoint()
        {
            NetworkInterface[] netInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(netInterface => netInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback).ToArray();

            NetworkInterface networkInterface = null;

            foreach (NetworkInterface item in netInterfaces)
            {
                if (item.OperationalStatus != OperationalStatus.Up)
                    continue;

                var stats = item.GetIPStatistics();


                networkInterface = networkInterface == null ? item
                    : stats.BytesReceived > networkInterface.GetIPStatistics().BytesReceived ? item : networkInterface;
            }

            var ip = networkInterface.GetIPProperties().UnicastAddresses
                    .Where(address => address.Address.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            if (ip == null)
                throw new WifiNotFoundException();

            Name = ip.Address.ToString();
            IPEndPoint endPoint = new(ip.Address, Constants.WifiPort);


            return endPoint;
        }

        private void AcceptAsync(IAsyncResult ar)
        {
            Socket s;
            try
            {
                s = listener.EndAcceptSocket(ar);
            }
            catch
            {
                return;
            }

            IClient client = new Client(s, ((IPEndPoint)s.RemoteEndPoint).Address.ToString());

            ProcessClientConnect(client);

            listener.BeginAcceptSocket(AcceptAsync, null);
        }

        protected override void StopListener()
        {
            listener?.Stop();
            listener = null;
        }
    }
}
