using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReMouse.Phone.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageTextButton : ContentView
    {
        public ImageTextButton()
        {
            InitializeComponent();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            GestureRecognizers.Add(tap);
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            if (Command != null)
                Command.Execute(null);
        }

        public static BindableProperty CommandProperty
            = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageTextButton), default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static BindableProperty ImageSourceProperty
            = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImageTextButton), default(ImageSource), BindingMode.TwoWay);

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static BindableProperty TextProperty
            = BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageTextButton), default(string), BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
                Label.Text = Text;
            else if (propertyName == ImageSourceProperty.PropertyName)
                Image.Source = ImageSource;
        }
    }
}