using ReMouse.Phone.Core.RepositoryData;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class BoxButton : ButtonBehaviourBase
    {
        private bool isOpen = true;
        private readonly List<ButtonDisplayBase> buttons;

        private readonly ImageSource open;
        private readonly ImageSource closed;

        public BoxButton(RelativeLayout layout, ButtonData buttonData, List<ButtonDisplayBase> buttons)
            : base(layout, buttonData)
        {
            this.buttons = buttons;
            open = ImageSource.FromFile("open.png");
            closed = ImageSource.FromFile("closed.png");
        }

        protected override Task OnTap()
        {
            isOpen ^= true;
            image.Source = isOpen ? open : closed;
            image.IsVisible = true;
            foreach (var button in buttons)
                button.IsVisible = isOpen;

            return base.OnTap();
        }
    }
}
