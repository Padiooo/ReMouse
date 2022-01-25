using System.Collections.Generic;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class MouseButtonFactory : ButtonFactory
    {
        public MouseButtonFactory(RelativeLayout layout) : base(layout) { }

        public override void CreateButtons()
        {
            List<ButtonDisplayBase> inBox = new List<ButtonDisplayBase>();
            BoxButton box = null;

            foreach (var buttonData in buttonRepository.GetAll())
            {
                if (!buttonData.IsEnabled)
                    continue;

                ButtonDisplayBase button;

                switch (buttonData.Type)
                {
                    case Shared.Packets.ButtonType.MoveMouse:
                        button = new MouseButton(layout, buttonData);
                        break;
                    case Shared.Packets.ButtonType.Box:
                        box = new BoxButton(layout, buttonData, inBox);
                        button = box;
                        break;
                    case Shared.Packets.ButtonType.Keyboard_Input:
                        button = new KeyboardButton(layout, buttonData);
                        break;
                    default:
                        button = new DefaultButton(layout, buttonData);
                        break;
                }

                if (buttonData.IsInBox && buttonData.Type != Shared.Packets.ButtonType.Box)
                    inBox.Add(button);

                buttons.Enqueue(button);
            }

            box?.TapCmd.Execute(null);
        }
    }
}
