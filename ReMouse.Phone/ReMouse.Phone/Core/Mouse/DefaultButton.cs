using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Shared.Packets;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class DefaultButton : ButtonBehaviourBase
    {
        private readonly byte[] dataTap;
        private readonly byte[] dataLong;
        private readonly bool isLeft;

        public DefaultButton(RelativeLayout layout, ButtonData buttonData)
            : base(layout, buttonData)
        {
            isLeft = buttonData.Type == ButtonType.Left;

            Packet packet = new Packet
            {
                ButtonType = buttonData.Type,
                ButtonBehaviour = isLeft ? ButtonBehaviour.CLICK : ButtonBehaviour.DEFAULT
            };
            dataTap = packet.GetBytes();

            if (isLeft)
            {
                packet = new Packet
                {
                    ButtonType = buttonData.Type,
                    ButtonBehaviour = ButtonBehaviour.HOLDORRELEASE
                };
                dataLong = packet.GetBytes();
            }
        }

        protected override async Task OnTap()
        {
            await SendAsync(dataTap, 0, dataTap.Length).ConfigureAwait(false);
        }

        protected override async Task OnLongTap()
        {
            if (isLeft)
                await SendAsync(dataLong, 0, dataLong.Length);
        }
    }
}
