using System;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.RepositoryData
{
    public enum ApplicationTheme { Light, Dark, Custom }

    [Serializable]
    public class ApplicationData
    {
        public int MouseSpeed { get; set; }
        public ApplicationTheme Theme { get; set; }

        public string IPAddress { get; set; }

        public string Custom_Background { get; set; }
        public string Custom_Foreground { get; set; }

        [NonSerialized]
        public static readonly Color Light_Background = Color.White;
        [NonSerialized]
        public static readonly Color Light_Foreground = Color.FromHex("#2196F3");

        [NonSerialized]
        public static readonly Color Dark_Background = Color.FromHex("#121212");
        [NonSerialized]
        public static readonly Color Dark_Foreground = Color.FromHex("#F0FFFFFF");
    }
}
