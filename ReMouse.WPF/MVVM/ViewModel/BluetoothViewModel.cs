using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.MVVM.Model;

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
