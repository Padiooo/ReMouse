using ReMouse.Phone.Core.RepositoryData;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.Models
{
    public class ApplicationColorModel : ObservableObject
    {
        public const string ResourceKey = "ColorApp";

        public ApplicationColorModel()
        {
            ApplicationDataRepository dataRepository = DependencyService.Get<ApplicationDataRepository>();
            var data = dataRepository.GetAll()[0];

            switch (data.Theme)
            {
                case ApplicationTheme.Light:
                    BackgroundColor = ApplicationData.Light_Background;
                    ForegroundColor = ApplicationData.Light_Foreground;
                    break;
                case ApplicationTheme.Dark:
                    BackgroundColor = ApplicationData.Dark_Background;
                    ForegroundColor = ApplicationData.Dark_Foreground;
                    break;
                case ApplicationTheme.Custom:
                    BackgroundColor = Color.FromHex(data.Custom_Background);
                    ForegroundColor = Color.FromHex(data.Custom_Foreground);
                    break;
            }
        }

        private Color backgroundColor;
        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetProperty(ref backgroundColor, value);
        }

        private Color foregroundColor;
        public Color ForegroundColor
        {
            get => foregroundColor;
            set => SetProperty(ref foregroundColor, value);
        }
    }
}
