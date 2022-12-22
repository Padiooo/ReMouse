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

        }

        protected override void StartListener()
        {
            var endpoint = GetEndpoint();

            if (endpoint == null)
                throw new WifiNotFoundException();

            Name = endpoint.Address.ToString();

            listener = new TcpListener(endpoint);
            listener.Start(1);
            listener.BeginAcceptSocket(AcceptAsync, null);
        }

        private IPEndPoint GetEndpoint()
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
                return null;

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
