using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public abstract class ButtonDisplayBase : IButton
    {
        private static ApplicationColorModel colorModel;
        private static ApplicationColorModel ColorModel
        {
            get
            {
                if (colorModel == null)
                    colorModel = (ApplicationColorModel)Xamarin.Forms.Application.Current.Resources[ApplicationColorModel.ResourceKey];
                return colorModel;
            }
        }

        protected RelativeLayout layout;
        protected Image image;
        private Color layoutColor;
        private bool isMouse;

        public bool IsVisible
        {
            get => image.IsVisible;
            set => image.IsVisible = value;
        }

        protected ButtonDisplayBase(RelativeLayout layout, ButtonData buttonData)
        {
            this.layout = layout;
            isMouse = buttonData.Type == Shared.Packets.ButtonType.MoveMouse;

            if (isMouse)
            {
                layoutColor = layout.BackgroundColor;
                layout.BackgroundColor = buttonData.ColorMode == ButtonData.ButtonColorMode.Custom ?
                    buttonData.Color : ColorModel.BackgroundColor;
            }
            else
            {
                image = new Image()
                {
                    Source = buttonData.ImageSource,
                    WidthRequest = buttonData.Size,
                    HeightRequest = buttonData.Size,
                };

                layout.Children.Add(image, () => new Rectangle(buttonData.XConstraint, buttonData.YConstraint, buttonData.Size, buttonData.Size));

                IconTintColorEffect.SetTintColor(image, buttonData.ColorMode == ButtonData.ButtonColorMode.Custom ?
                    buttonData.Color : ColorModel.ForegroundColor);
            }
        }

        public virtual void Destroy()
        {
            if (isMouse)
                layout.BackgroundColor = layoutColor;
            else
                layout.Children.Remove(image);

            image = null;
            layout = null;
        }
    }
}
