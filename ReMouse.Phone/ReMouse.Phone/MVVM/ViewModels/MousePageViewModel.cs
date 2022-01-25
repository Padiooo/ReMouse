using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Mouse;
using ReMouse.Phone.MVVM.Models;
using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class MousePageViewModel : ObservableObject, IPageStatusTracker
    {
        private readonly MouseButtonFactory factory;
        private readonly StackLayout disconnectedLayout;

        public SocketModel Socket { get; }
        public IAsyncCommand DismissCmd { get; }

        public MousePageViewModel(RelativeLayout workingLayout, StackLayout disconnectedLayout)
        {
            factory = new MouseButtonFactory(workingLayout);
            this.disconnectedLayout = disconnectedLayout;
            Socket = (SocketModel)Application.Current.Resources[SocketModel.ResourceKey];

            DismissCmd = new AsyncCommand(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            workingLayout.SizeChanged += Layout_SizeChanged;
        }

        private void Layout_SizeChanged(object sender, EventArgs e)
        {
            RelativeLayout layout = sender as RelativeLayout;

            double xoffset = layout.Width / 2 - disconnectedLayout.Width / 2;
            double yoffset = layout.Height / 2 - disconnectedLayout.Height / 2;

            RelativeLayout.SetXConstraint(disconnectedLayout, Constraint.Constant(xoffset));
            RelativeLayout.SetYConstraint(disconnectedLayout, Constraint.Constant(yoffset));
        }

        public void OnPagePushed()
        {
            if (Socket.Connected)
                factory.CreateButtons();
        }

        public void OnPagePopped()
        {
            factory.DestroyButtons();
        }
    }
}
