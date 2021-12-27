using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.Core.Sockets;
using ReMouse.WPF.Core.Sockets.Clients;
using ReMouse.WPF.Core.Sockets.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMouse.WPF.MVVM.Model
{
    public enum ConnectionType { WIFI, BLUETOOTH }

    public class ListenerModel : IListener
    {
        public string Name => currentListener != null ? currentListener.Name : "";
        public event EventHandler<IClient> OnClientConnect;

        private IListener currentListener;

        private ConnectionType _connectionType;
        public ConnectionType ConnectionType
        {
            get => _connectionType;
            set
            {
                if (currentListener != null && _connectionType == value)
                    return;
                _connectionType = value;

                if (currentListener != null)
                {
                    currentListener.Stop();
                    currentListener.OnClientConnect -= CurrentListener_OnClientConnect;
                }

                currentListener = value == ConnectionType.WIFI ? wifiListener.Value : bluetoothListener.Value;
                currentListener.Start();
                currentListener.OnClientConnect += CurrentListener_OnClientConnect;
            }
        }

        private Lazy<WifiListener> wifiListener = new Lazy<WifiListener>();
        private Lazy<BluetoothListener> bluetoothListener = new Lazy<BluetoothListener>();

        private void CurrentListener_OnClientConnect(object sender, IClient e)
        {
            OnClientConnect.Invoke(this, e);
        }

        public void Start()
        {
            currentListener?.Start();
        }

        public void Stop()
        {
            currentListener?.Stop();
        }
    }
}
