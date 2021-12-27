using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMouse.WPF.MVVM.ViewModel
{
    public class WifiViewModel
    {
        public RelayCommand HomeCmd { get; }

        public ConnectivityModel Connectivity { get; }

        public WifiViewModel(RelayCommand homeCmd, ConnectivityModel connectivityModel)
        {
            HomeCmd = homeCmd;
            Connectivity = connectivityModel;
        }
    }
}
