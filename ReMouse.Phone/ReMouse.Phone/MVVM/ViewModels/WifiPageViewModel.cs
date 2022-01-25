using ReMouse.Phone.Core.Network;
using ReMouse.Phone.Core.RepositoryData;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class WifiPageViewModel : ConnectionPageBaseViewModel<IWifiHandler>
    {
        private readonly ApplicationDataRepository repository;

        public string IpAddress
        {
            get => repository.GetAll()[0].IPAddress;
            set => repository.GetAll()[0].IPAddress = value;
        }

        public WifiPageViewModel()
            : base()
        {
            repository = DependencyService.Get<ApplicationDataRepository>();
        }

        protected override async Task<ISocket> DoConnectAsync()
        {
            ISocket socket = await Handler.ConnectAsync(IpAddress);

            if (socket != null && socket.Connected)
                repository.Save();

            return socket;
        }
    }
}
