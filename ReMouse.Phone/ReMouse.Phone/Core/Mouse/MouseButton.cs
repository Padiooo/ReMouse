using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using ReMouse.Shared.Packets;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class MouseButton : ButtonDisplayBase
    {
        private readonly SocketModel socket;
        private readonly int mouseSpeed;
        private readonly VelocityTracker velocityTracker = new VelocityTracker();

        private readonly Packet packet;
        private readonly byte[] buffer = new byte[sizeof(int) * 2 + sizeof(float) * 2];

        private RelativeLayout layout;
        private TouchTracking.Forms.TouchEffect touch;
        private Color layoutColor;

        public MouseButton(RelativeLayout layout, ButtonData buttonData)
            : base(layout, buttonData)
        {
            this.layout = layout;

            mouseSpeed = DependencyService.Get<ApplicationDataRepository>().GetAll()[0].MouseSpeed;
            socket = (SocketModel)Xamarin.Forms.Application.Current.Resources[SocketModel.ResourceKey];
            packet = new Packet()
            {
                ButtonType = ButtonType.MoveMouse,
                ButtonBehaviour = ButtonBehaviour.DEFAULT,
            };

            touch = new TouchTracking.Forms.TouchEffect()
            {
                Capture = true,
            };
            touch.TouchAction += Touch_TouchAction;
            layout.Effects.Add(touch);
        }

        private async void Touch_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchTracking.TouchActionType.Entered:
                    velocityTracker.Reset();
                    break;
                case TouchTracking.TouchActionType.Pressed:
                    velocityTracker.Reset();
                    break;
                case TouchTracking.TouchActionType.Moved:
                    velocityTracker.AddPoint(args.Location.X, args.Location.Y);
                    packet.X = velocityTracker.XVelocity * mouseSpeed;
                    packet.Y = velocityTracker.YVelocity * mouseSpeed;
                    packet.GetBytes(buffer, 0);
                    await socket.SendAsync(buffer, 0, buffer.Length);
                    break;
                case TouchTracking.TouchActionType.Released:
                    velocityTracker.Reset();
                    break;
                case TouchTracking.TouchActionType.Cancelled:
                    velocityTracker.Reset();
                    break;
                case TouchTracking.TouchActionType.Exited:
                    velocityTracker.Reset();
                    break;
                default:
                    break;
            }
        }

        public override void Destroy()
        {
            layout.BackgroundColor = layoutColor;
            layout.Effects.Remove(touch);

            layout = null;
            touch = null;

            base.Destroy();
        }
    }
}
