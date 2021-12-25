using ReMouse.WPF.Core.DataBinding;
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

        public WifiViewModel(RelayCommand homeCmd)
        {
            HomeCmd = homeCmd;
        }
    }
}
