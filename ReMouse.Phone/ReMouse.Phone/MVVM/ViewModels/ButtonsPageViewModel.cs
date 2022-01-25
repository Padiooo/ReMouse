using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using ReMouse.Phone.MVVM.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class ButtonsPageViewModel : ObservableObject, IPageStatusTracker
    {
        public IAsyncCommand PositionCmd { get; }
        public IAsyncCommand PropertiesCmd { get; }

        public HashSet<ButtonModel> selectedButtons = new HashSet<ButtonModel>();
        public ButtonModel SelectedItem
        {
            get => null;
            set
            {
                value.IsSelected ^= true;
                if (value.IsSelected)
                    selectedButtons.Add(value);
                else
                    selectedButtons.Remove(value);
                OnPropertyChanged();
            }
        }

        public ObservableRangeCollection<ButtonModel> Buttons { get; }

        public ButtonsPageViewModel()
        {
            Buttons = new ObservableRangeCollection<ButtonModel>(DependencyService.Get<ButtonRepository>().GetAll().Select(data => new ButtonModel() { Data = data, IsSelected = false }));

            PositionCmd = new AsyncCommand(PositionAsync);
            PropertiesCmd = new AsyncCommand(PropertiesAsync);
        }

        private async Task PositionAsync()
        {
            await ((PageHolder)App.Current.Resources[PageHolder.ResourceKey]).GetPage<PositionPage>().PushAsync(App.Current.MainPage.Navigation);
        }

        private async Task PropertiesAsync()
        {
            if (selectedButtons.Count != 0)
            {
                PropertyPage propertyPage = ((PageHolder)App.Current.Resources[PageHolder.ResourceKey]).GetPage<PropertyPage>();
                propertyPage.BindingContext = new PropertyPageViewModel(selectedButtons.Select(bm => bm.Data).ToList());

                await propertyPage.PushAsync(App.Current.MainPage.Navigation);
            }
        }

        public void Reset()
        {
            foreach (var button in Buttons)
                button.IsSelected = false;
        }

        public void OnPagePushed()
        {
            foreach (var button in Buttons)
                button.Update();
        }

        public void OnPagePopped()
        {

        }
    }
}
