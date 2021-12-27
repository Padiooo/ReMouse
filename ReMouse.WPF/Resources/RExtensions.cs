using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReMouse.WPF.Resources
{
    public static class RExtensions
    {
        public static ImageSource ToSource(this string ressourcePath)
        {
            //BitmapImage image = new BitmapImage(new Uri("pack://application:,,," + R.Images.Icon, UriKind.Relative));
            Uri uri = new Uri("pack://application:,,," + ressourcePath);
            BitmapImage image = new BitmapImage(uri);

            return image;
        }
    }
}
