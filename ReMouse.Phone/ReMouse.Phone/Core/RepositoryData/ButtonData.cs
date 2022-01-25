using ReMouse.Shared.Packets;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.RepositoryData
{
    [Serializable]
    public class ButtonData
    {
        public enum ButtonColorMode
        {
            [Description("App Theme")]
            AppTheme,
            [Description("Custom")]
            Custom,
        }

        public ButtonType Type { get; set; }

        public ImageSource ImageSource => GetImageSource();
        public string FriendlyName => Type.GetName();

        public bool IsEnabled { get; set; }
        public bool IsInBox { get; set; }

        public double XConstraint { get; set; }
        public double YConstraint { get; set; }

        public double Size => 70 * (Progress + 0.5);

        public double Progress { get; set; }

        public ButtonColorMode ColorMode { get; set; }
        public Color Color { get; set; }

        public ButtonData()
        {
            IsEnabled = false;
            IsInBox = false;
            XConstraint = 0;
            YConstraint = 0;
            Progress = 0.5f;
            ColorMode = ButtonData.ButtonColorMode.AppTheme;
            Color = Color.Black;
        }

        public void Copy(ButtonData data)
        {
            IsEnabled = data.IsEnabled;
            IsInBox = data.IsInBox;
            Progress = data.Progress;
            ColorMode = data.ColorMode;
            Color = data.Color;
        }

        private ImageSource GetImageSource()
        {
            switch (Type)
            {
                case ButtonType.Box:
                    return ImageSource.FromFile("closed.png");
                case ButtonType.Keyboard_Input:
                    return ImageSource.FromFile("erase.png");
                case ButtonType.MoveMouse:
                    return ImageSource.FromFile("mouse.png");
                default:
                    if (Enum.IsDefined(typeof(ButtonType), Type))
                        return ImageSource.FromFile(Type.ToString().ToLower() + ".png");
                    else
                        return ImageSource.FromFile("erase.png");
            }
        }
    }
}
