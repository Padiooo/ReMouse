using ReMouse.WPF.Core.DataBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
