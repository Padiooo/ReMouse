using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ReMouse.WPF.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
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

        public GradientModel BackgroundGradient { get; }
            = new GradientModel()
            {
                Color1 = (Color)ColorConverter.ConvertFromString("#2FA808"),
                Color2 = (Color)ColorConverter.ConvertFromString("#081C01")
            };

        public MainViewModel()
        {
            _homeVM = new HomeViewModel(new RelayCommand(BluetoothCmd), new RelayCommand(WifiCmd));
            RelayCommand homeCmd = new(o => MainView = _homeVM);
            _bluetoothVM = new BluetoothViewModel(homeCmd);
            _wifiVM = new WifiViewModel(homeCmd);

            MainView = _homeVM;
        }

        private void BluetoothCmd(object obj)
        {
            MainView = _bluetoothVM;
        }

        private void WifiCmd(object obj)
        {
            MainView = _wifiVM;
        }
    }
}
