using System.Windows.Media;

namespace ReMouse.WPF.Resources
{
    public class R
    {
        public class Images
        {
            public const string Icon = "/Resources/Images/phone.ico";
            public const string Bluetooth = "/Resources/Images/bluetooth.png";
            public const string Wifi = "/Resources/Images/wifi.png";
            public const string Home = "/Resources/Images/home.png";
        }

        public class Colors
        {
            public static readonly Color Disconnected_Background_1 = (Color)ColorConverter.ConvertFromString("#AA8844");
            public static readonly Color Disconnected_Background_2 = (Color)ColorConverter.ConvertFromString("#FF2222");

            public static readonly Color Connected_Background_1 = (Color)ColorConverter.ConvertFromString("#2FA808");
            public static readonly Color Connected_Background_2 = (Color)ColorConverter.ConvertFromString("#081C01");

            public static readonly Color Bluetooth_1 = (Color)ColorConverter.ConvertFromString("#0E99E6");
            public static readonly Color Bluetooth_2 = (Color)ColorConverter.ConvertFromString("#0800FF");

            public static readonly Color Wifi_1 = (Color)ColorConverter.ConvertFromString("#C80BAC");
            public static readonly Color Wifi_2 = (Color)ColorConverter.ConvertFromString("#592277");
        }

        public class Fonts
        {
            public const string Roboto = "/Resources/Fonts/#Roboto";
            public const string Heebo = "/Resources/Fonts/#Heebo";
        }
    }
}
