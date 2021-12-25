using ReMouse.WPF.Core.DataBinding;
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

        public BluetoothViewModel(RelayCommand homeCmd)
        {
            HomeCmd = homeCmd;
        }
    }
}
