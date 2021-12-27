using ReMouse.WPF.Core.CommandLine;
using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.Core.Packets;
using ReMouse.WPF.Core.Sockets.Clients;
using ReMouse.WPF.MVVM.Model;
using ReMouse.WPF.Resources;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReMouse.WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject, IArgumentHandler
    {
        public object WindowBarView { get; } = new WindowBarViewModel();

        private readonly HomeViewModel _homeVM;
        private readonly BluetoothViewModel _bluetoothVM;
        private readonly WifiViewModel _wifiVM;

        private object _mainView;
        public object MainView
        {
            get => _mainView;
            set
            {
                _mainView = value;
                OnPropertyChanged();
            }
        }

        private readonly Action<Color, Color> _transition;

        private ListenerModel listenerModel;
        private PacketProcessor processor;
        private Color color1, color2;
        private ConnectivityModel connectivityModel;

        public MainViewModel(Action<Color, Color> transition, ContextMenu contextMenu)
        {
            _transition = transition;
            listenerModel = new ListenerModel();
            listenerModel.OnClientConnect += ListenerModel_OnClientConnect;

            RelayCommand bluetoothCmd = new RelayCommand(BluetoothCmd);
            RelayCommand wifiCmd = new RelayCommand(WifiCmd);
            _homeVM = new HomeViewModel(bluetoothCmd, wifiCmd);

            contextMenu.Items.Cast<MenuItem>().FirstOrDefault(mi => mi.Header.ToString() == "Bluetooth").Command = bluetoothCmd;
            contextMenu.Items.Cast<MenuItem>().FirstOrDefault(mi => mi.Header.ToString() == "Wifi").Command = wifiCmd;

            RelayCommand homeCmd = new(o => MainView = _homeVM);
            connectivityModel = new ConnectivityModel();
            _bluetoothVM = new BluetoothViewModel(homeCmd, connectivityModel);
            _wifiVM = new WifiViewModel(homeCmd, connectivityModel);

            MainView = _homeVM;

            processor = new PacketProcessor();
        }

        private void ListenerModel_OnClientConnect(object sender, IClient client)
        {
            _transition.Invoke(color1, color2);
            connectivityModel.RemoteName = client.Name;
            client.OnDataReceived += processor.ProcessData;
            client.OnDisconnect += Client_OnDisconnect;
        }

        private void Client_OnDisconnect()
        {
            _transition.Invoke(R.Colors.Disconnected_Background_1, R.Colors.Disconnected_Background_2);
            connectivityModel.RemoteName = "";
        }

        private void BluetoothCmd(object obj)
        {
            MainView = _bluetoothVM;
            listenerModel.ConnectionType = ConnectionType.BLUETOOTH;
            color1 = R.Colors.Bluetooth_1;
            color2 = R.Colors.Bluetooth_2;

            connectivityModel.LocalName = listenerModel.Name;
        }

        private void WifiCmd(object obj)
        {
            MainView = _wifiVM;
            listenerModel.ConnectionType = ConnectionType.WIFI;
            color1 = R.Colors.Wifi_1;
            color2 = R.Colors.Wifi_2;

            connectivityModel.LocalName = listenerModel.Name;
        }

        public void HandleArguments(string[] args)
        {
            ConnectionModeArgument connectionMode = new(args);
            if (connectionMode.Found)
            {
                Action<object> action;
                switch (connectionMode.Option)
                {
                    case ConnectionModeOption.BLUETOOTH:
                        action = BluetoothCmd;
                        break;
                    case ConnectionModeOption.WIFI:
                        action = WifiCmd;
                        break;
                    default:
                        action = null;
                        break;
                }
                action?.Invoke(null);
            }
        }
    }
}
