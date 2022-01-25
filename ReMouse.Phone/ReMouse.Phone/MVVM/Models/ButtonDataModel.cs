using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Shared.Packets;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using static ReMouse.Phone.Core.RepositoryData.ButtonData;

namespace ReMouse.Phone.MVVM.Models
{
    public class ButtonDataModel : ObservableObject
    {
        public ButtonData Data { get; set; }

        public ICommand ValidateHexCmd { get; }

        public ButtonDataModel()
        {
            ValidateHexCmd = new Command(ValidateColorHex);
        }

        private void ValidateColorHex(object eventArgs)
        {
            FocusEventArgs args = eventArgs as FocusEventArgs;
            Entry entry = args.VisualElement as Entry;

            if (HexColorValidator.IsValidColorHex(entry.Text))
            {
                ColorHex = entry.Text;
                entry.BackgroundColor = Color.Transparent;
            }
            else
            {
                entry.BackgroundColor = Color.Red;
            }
        }

        public ButtonType Type
        {
            get => Data.Type;
            set
            {
                Data.Type = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => Data.IsEnabled;
            set
            {
                Data.IsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool IsInBox
        {
            get => Data.IsInBox;
            set
            {
                Data.IsInBox = value;
                OnPropertyChanged();
            }
        }

        public double XConstraint
        {
            get => Data.XConstraint;
            set
            {
                Data.XConstraint = value;
                OnPropertyChanged();
            }
        }
        public double YConstraint
        {
            get => Data.YConstraint;
            set
            {
                Data.YConstraint = value;
                OnPropertyChanged();
            }
        }

        public double Progress
        {
            get => Data.Progress;
            set
            {
                Data.Progress = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Data));
            }
        }

        public bool IsAppTheme
        {
            get => Data.ColorMode == ButtonColorMode.AppTheme;
            set
            {
                Data.ColorMode = value ? ButtonColorMode.AppTheme : ButtonColorMode.Custom;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCustom));
            }
        }

        public bool IsCustom
        {
            get => Data.ColorMode == ButtonColorMode.Custom;
            set
            {
                Data.ColorMode = value ? ButtonColorMode.Custom : ButtonColorMode.AppTheme;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAppTheme));
            }
        }

        public Color Color
        {
            get => Data.Color;
            set
            {
                Data.Color = value;
                OnPropertyChanged();
            }
        }

        private string colorHex;
        public string ColorHex
        {
            get => colorHex ?? Color.ToHex();
            set
            {
                colorHex = value;
                Color = Color.FromHex(colorHex);
            }
        }
    }
}
