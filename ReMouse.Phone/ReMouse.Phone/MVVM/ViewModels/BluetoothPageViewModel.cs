using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Network;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class BluetoothPageViewModel : ConnectionPageBaseViewModel<IBluetoothHandler>, IPageStatusTracker
    {
        public ObservableCollection<string> Devices => Handler.Devices;

        public int SelectedIndex { get; set; }

        public BluetoothPageViewModel() : base()
        {

        }

        protected override async Task<ISocket> DoConnectAsync()
        {
            try
            {
                return await Handler.ConnectAsync(Handler.Devices[SelectedIndex]);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void OnPagePushed()
        {
            Handler.Refresh();
        }

        public void OnPagePopped()
        {

        }
    }
}
