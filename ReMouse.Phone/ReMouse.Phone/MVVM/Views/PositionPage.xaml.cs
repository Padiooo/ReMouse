using ReMouse.Phone.Core.Application;
using ReMouse.Phone.MVVM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReMouse.Phone.MVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PositionPage : ContentPage
    {
        public PositionPage()
        {
            InitializeComponent();
            BindingContext = new PositionPageViewModel(Layout);
        }

        protected override void OnAppearing()
        {
            if (BindingContext is IPageStatusTracker tracker)
                tracker.OnPagePushed();

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            if (BindingContext is IPageStatusTracker tracker)
                tracker.OnPagePopped();

            base.OnDisappearing();
        }
    }
}