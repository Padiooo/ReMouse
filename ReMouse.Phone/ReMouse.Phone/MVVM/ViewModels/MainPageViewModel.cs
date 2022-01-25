using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.MVVM.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class MainPageViewModel
    {
        public IAsyncCommand ConnectionCmd { get; }
        public IAsyncCommand MouseCmd { get; }
        public IAsyncCommand SettingsCmd { get; }

        public MainPageViewModel()
        {
            PageHolder holder = (PageHolder)Application.Current.Resources[PageHolder.ResourceKey];

            ConnectionCmd = new AsyncCommand(async () => await holder.GetPage<ConnectionPage>().PushAsync(Application.Current.MainPage.Navigation));
            MouseCmd = new AsyncCommand(async () => await holder.GetPage<MousePage>().PushAsync(Application.Current.MainPage.Navigation));
            SettingsCmd = new AsyncCommand(async () => await holder.GetPage<SettingsPage>().PushAsync(Application.Current.MainPage.Navigation));
        }
    }
}
