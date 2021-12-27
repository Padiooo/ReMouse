using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.Core.Sockets;
using ReMouse.WPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMouse.WPF.MVVM.ViewModel
{
    public class BluetoothViewModel
    {
        public RelayCommand HomeCmd { get; }

        public ConnectivityModel Connectivity { get; }

        public BluetoothViewModel(RelayCommand homeCmd, ConnectivityModel connectivityModel)
        {
            Connectivity = connectivityModel;
            HomeCmd = homeCmd;
        }
    }
}
