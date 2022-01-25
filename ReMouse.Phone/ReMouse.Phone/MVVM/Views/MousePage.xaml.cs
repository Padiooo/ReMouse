using ReMouse.Phone.Core.Application;
using ReMouse.Phone.MVVM.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReMouse.Phone.MVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MousePage : ContentPage
    {
        public MousePage()
        {
            InitializeComponent();

            MousePageViewModel vm = new MousePageViewModel(WorkingLayout, DisconnectLayout);
            BindingContext = vm;
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