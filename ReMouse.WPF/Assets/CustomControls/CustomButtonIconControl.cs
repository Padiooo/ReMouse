using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ReMouse.WPF.Assets.CustomControls
{
    public class CustomButtonIconControl : RadioButton
    {
        public static readonly DependencyProperty ImageSourceProperty
            = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CustomButtonIconControl),
                new PropertyMetadata(default(ImageSource)));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty Color1Property
            = DependencyProperty.Register(nameof(Color1), typeof(Color), typeof(CustomButtonIconControl),
                new PropertyMetadata(default(Color)));

        public Color Color1
        {
            get => (Color)GetValue(Color1Property);
            set => SetValue(Color1Property, value);
        }

        public static readonly DependencyProperty Color2Property
            = DependencyProperty.Register(nameof(Color2), typeof(Color), typeof(CustomButtonIconControl),
                new PropertyMetadata(default(Color)));

        public Color Color2
        {
            get => (Color)GetValue(Color2Property);
            set => SetValue(Color2Property, value);
        }

        public static readonly DependencyProperty ImageMarginProperty
            = DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(CustomButtonIconControl),
                new PropertyMetadata(default(Thickness)));

        public Thickness ImageMargin
        {
            get => (Thickness)GetValue(ImageMarginProperty);
            set => SetValue(ImageMarginProperty, value);
        }
    }

}
