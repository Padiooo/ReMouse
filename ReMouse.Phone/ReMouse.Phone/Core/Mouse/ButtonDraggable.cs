using ReMouse.Phone.Core.RepositoryData;
using TouchTracking;
using TouchTracking.Forms;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class ButtonDraggable : ButtonDisplayBase
    {
        private TouchEffect touch;
        private ButtonData buttonData;

        private readonly double size;
        private double X;
        private double Y;

        public ButtonDraggable(RelativeLayout layout, ButtonData buttonData)
            : base(layout, buttonData)
        {
            if (buttonData.Type == Shared.Packets.ButtonType.MoveMouse)
                return;

            this.buttonData = buttonData;

            size = buttonData.Size;
            X = buttonData.XConstraint;
            Y = buttonData.YConstraint;

            touch = new TouchEffect()
            {
                Capture = true,
            };
            touch.TouchAction += Touch_TouchAction;
            image.Effects.Add(touch);
        }

        private void Touch_TouchAction(object sender, TouchActionEventArgs args)
        {
            X += args.Location.X - size / 2;
            Y += args.Location.Y - size / 2;

            RelativeLayout.SetXConstraint(image, Constraint.Constant(X));
            RelativeLayout.SetYConstraint(image, Constraint.Constant(Y));
        }

        public override void Destroy()
        {
            if (touch != null)
                image.Effects.Remove(touch);

            if (buttonData != null)
            {
                buttonData.XConstraint = X;
                buttonData.YConstraint = Y;
            }

            base.Destroy();
        }
    }
}
