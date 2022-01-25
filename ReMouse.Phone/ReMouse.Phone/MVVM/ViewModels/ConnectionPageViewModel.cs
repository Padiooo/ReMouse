using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.MVVM.Views;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class ConnectionPageViewModel
    {
        public ICommand BluetoothCmd { get; }
        public ICommand WifiCmd { get; }

        public ConnectionPageViewModel()
        {
            PageHolder holder = (PageHolder)Application.Current.Resources[PageHolder.ResourceKey];
            BluetoothCmd = new AsyncCommand(async () => await Application.Current.MainPage.Navigation.PushAsync(holder.GetPage<BluetoothPage>()));
            WifiCmd = new AsyncCommand(async () => await Application.Current.MainPage.Navigation.PushAsync(holder.GetPage<WifiPage>()));
        }
    }
}
