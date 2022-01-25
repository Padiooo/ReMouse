using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.RepositoryData;
using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.Models
{
    public class SettingsModel : ObservableObject
    {
        private readonly ApplicationDataRepository dataRepository;
        private readonly ApplicationColorModel applicationColorModel;
        private readonly ApplicationData data;

        private ApplicationTheme applicationTheme;
        public ApplicationTheme ApplicationTheme
        {
            get => applicationTheme;
            set
            {
                SetProperty(ref applicationTheme, value);
                IsCustom = applicationTheme == ApplicationTheme.Custom;

                switch (applicationTheme)
                {
                    case ApplicationTheme.Light:
                        ForegroundColor = ApplicationData.Light_Foreground;
                        BackgroundColor = ApplicationData.Light_Background;
                        break;
                    case ApplicationTheme.Dark:
                        ForegroundColor = ApplicationData.Dark_Foreground;
                        BackgroundColor = ApplicationData.Dark_Background;
                        break;
                    case ApplicationTheme.Custom:
                        ForegroundColor = Color.FromHex(foregroundHex);
                        BackgroundColor = Color.FromHex(backgroundHex);
                        break;
                }
            }
        }

        public ApplicationTheme[] ApplicationThemes => (ApplicationTheme[])Enum.GetValues(typeof(ApplicationTheme));

        private bool isCustom;
        public bool IsCustom
        {
            get => isCustom;
            set => SetProperty(ref isCustom, value);
        }

        public ICommand ValidateColorHexCmd { get; }

        private string foregroundHex;
        public string ForegroundHex
        {
            get => foregroundHex;
            private set
            {
                ForegroundColor = Color.FromHex(value);
                SetProperty(ref foregroundHex, value);
            }
        }

        private Color foregroundColor;
        public Color ForegroundColor
        {
            get => foregroundColor;
            set => SetProperty(ref foregroundColor, value);
        }

        private string backgroundHex;
        public string BackgroundHex
        {
            get => backgroundHex;
            private set
            {
                BackgroundColor = Color.FromHex(value);
                SetProperty(ref backgroundHex, value);
            }
        }

        private Color backgroundColor;
        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetProperty(ref backgroundColor, value);
        }

        public SettingsModel()
        {
            dataRepository = DependencyService.Get<ApplicationDataRepository>();
            data = dataRepository.GetAll()[0];

            applicationColorModel = (ApplicationColorModel)Application.Current.Resources[ApplicationColorModel.ResourceKey];

            Reset();

            ValidateColorHexCmd = new Command(ValidateColorHex);
        }

        public void Reset()
        {
            ForegroundHex = data.Custom_Foreground;
            BackgroundHex = data.Custom_Background;

            ApplicationTheme = data.Theme;
        }

        public void Save()
        {
            applicationColorModel.ForegroundColor = ForegroundColor;
            applicationColorModel.BackgroundColor = BackgroundColor;

            data.Theme = ApplicationTheme;

            if (ApplicationTheme == ApplicationTheme.Custom)
            {
                data.Custom_Background = backgroundHex;
                data.Custom_Foreground = foregroundHex;
            }

            dataRepository.Save();
        }

        private void ValidateColorHex(object eventArgs)
        {
            FocusEventArgs args = eventArgs as FocusEventArgs;
            Entry entry = args.VisualElement as Entry;

            if (HexColorValidator.IsValidColorHex(entry.Text))
            {
                if (entry.Placeholder == "Foreground")
                    ForegroundHex = entry.Text;
                else
                    BackgroundHex = entry.Text;

                entry.BackgroundColor = Color.Transparent;
            }
            else
            {
                entry.BackgroundColor = Color.Red;
            }
        }
    }
}
