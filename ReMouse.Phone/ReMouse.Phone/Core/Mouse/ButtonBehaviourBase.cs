using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public abstract class ButtonBehaviourBase : ButtonDisplayBase
    {
        /// <summary>
        /// The default behaviour of buttons.
        /// </summary>
        public IAsyncCommand TapCmd { get; }
        /// <summary>
        /// 
        /// </summary>
        public IAsyncCommand LongTapCmd { get; }

        private static SocketModel socket;
        private static SocketModel Socket
        {
            get
            {
                if (socket == null)
                    socket = (SocketModel)App.Current.Resources[SocketModel.ResourceKey];
                return socket;
            }
        }

        protected ButtonBehaviourBase(RelativeLayout layout, ButtonData buttonData)
            : base(layout, buttonData)
        {
            TapCmd = new AsyncCommand(OnTap);
            LongTapCmd = new AsyncCommand(OnLongTap);

            TouchEffect.SetCommand(image, TapCmd);
            TouchEffect.SetLongPressCommand(image, LongTapCmd);
        }

        protected virtual Task OnTap()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnLongTap()
        {
            return Task.CompletedTask;
        }

        protected async Task SendAsync(byte[] buffer, int offset, int length)
        {
            await Socket.SendAsync(buffer, offset, length).ConfigureAwait(false);
        }
    }
}
