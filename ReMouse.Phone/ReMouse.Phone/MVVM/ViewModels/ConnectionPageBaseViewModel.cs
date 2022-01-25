using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.Network;
using ReMouse.Phone.MVVM.Models;
using ReMouse.Phone.MVVM.Views;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public abstract class ConnectionPageBaseViewModel<THandler> : ObservableObject where THandler : class, IHandler
    {
        protected THandler Handler { get; }

        public IAsyncCommand ConnectCmd { get; }

        private bool connectEnabled = true;
        public bool ConnectEnabled
        {
            get => connectEnabled;
            private set => SetProperty(ref connectEnabled, value);
        }

        public bool IsSupported => Handler.IsSupported;
        public bool IsAvailable => Handler.IsAvailable;

        protected ConnectionPageBaseViewModel()
        {
            Handler = DependencyService.Get<THandler>();

            ConnectCmd = new AsyncCommand(ConnectAsync);

            Handler.AvailabilityChanged += Handler_AvailabilityChanged;
        }

        private void Handler_AvailabilityChanged(object sender, bool e)
        {
            OnPropertyChanged(nameof(IsAvailable));
        }

        protected async Task ConnectAsync()
        {
            SocketModel socketModel = (SocketModel)Application.Current.Resources[SocketModel.ResourceKey];
            socketModel.ConnectionStatus = ConnectionStatus.CONNECTING;
            ConnectEnabled = false;

            ISocket s = await DoConnectAsync();
            await socketModel.SetSocketAsync(s);

            if (s != null && s.Connected)
                await OpenMouseAsync();

            ConnectEnabled = true;
        }

        protected abstract Task<ISocket> DoConnectAsync();

        protected async Task OpenMouseAsync()
        {
            INavigation nav = Application.Current.MainPage.Navigation;
            await nav.PopToRootAsync();
            await ((PageHolder)Application.Current.Resources[PageHolder.ResourceKey]).GetPage<MousePage>().PushAsync(nav);
        }
    }
}
