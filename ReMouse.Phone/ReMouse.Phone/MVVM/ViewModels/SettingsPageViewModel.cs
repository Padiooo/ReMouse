using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using ReMouse.Phone.MVVM.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class SettingsPageViewModel : IPageStatusTracker
    {
        public SettingsModel SettingsModel { get; } = new SettingsModel();

        public IAsyncCommand SaveCmd { get; }
        public IAsyncCommand ButtonsCmd { get; }
        public IAsyncCommand ResetCmd { get; }

        public SettingsPageViewModel()
        {
            ResetCmd = new AsyncCommand(Reset);

            SaveCmd = new AsyncCommand(async () =>
            {
                SettingsModel.Save();
                await App.Current.MainPage.Navigation.PopAsync();
            });

            PageHolder pageHolder = ((PageHolder)App.Current.Resources[PageHolder.ResourceKey]);
            ButtonsCmd = new AsyncCommand(async () =>
            {
                SettingsModel.Save();

                ButtonsPage page = pageHolder.GetPage<ButtonsPage>();
                ((ButtonsPageViewModel)page.BindingContext).Reset();
                await page.PushAsync(Xamarin.Forms.Application.Current.MainPage.Navigation);
            });
        }

        private async Task Reset()
        {
            bool sure = await App.Current.MainPage.DisplayAlert("Reset settings", "Are you sure you want to reset settings? If Yes, application will close.", "Yes", "No");

            if (sure)
            {
                DependencyService.Get<ButtonRepository>().Reset();
                DependencyService.Get<ApplicationDataRepository>().Reset();
                DependencyService.Get<IApplicationCloser>().CloseApp();
            }
        }

        public void OnPagePushed()
        {
            SettingsModel.Reset();
        }

        public void OnPagePopped()
        {

        }
    }
}
