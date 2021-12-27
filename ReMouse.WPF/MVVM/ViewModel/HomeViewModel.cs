using ReMouse.WPF.Core.DataBinding;

namespace ReMouse.WPF.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public RelayCommand BluetoothCmd { get; }
        public RelayCommand WifiCmd { get; }

        public HomeViewModel(RelayCommand bluetoothCmd, RelayCommand wifiCmd)
        {
            BluetoothCmd = bluetoothCmd;
            WifiCmd = wifiCmd;
        }
    }
}
