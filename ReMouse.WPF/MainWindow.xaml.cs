using Hardcodet.Wpf.TaskbarNotification;
using ReMouse.WPF.Core.CommandLine;
using ReMouse.WPF.Core.DataBinding;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ReMouse.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IArgumentHandler
    {
        private readonly ColorAnimation colorAnimation1 = new();
        private readonly ColorAnimation colorAnimation2 = new();

        private readonly TaskbarIcon icon;

        public MainWindow()
        {
            InitializeComponent();

            Closing += OnClosing;
            icon = (TaskbarIcon)FindResource("TaskbarIcon");

            icon.NoLeftClickDelay = true;
            icon.LeftClickCommand = new RelayCommand(o =>
            {
                Show();
                WindowState = WindowState.Normal;
            });

            colorAnimation1.Duration = TimeSpan.FromSeconds(0.3);
            colorAnimation2.Duration = TimeSpan.FromSeconds(0.3);
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            icon.Visibility = Visibility.Visible;
        }

        public void BackgroundTransition(Color color1, Color color2)
        {
            this.Dispatcher.Invoke(() =>
            {
                colorAnimation1.To = color1;
                colorAnimation2.To = color2;

                GradientStop1.BeginAnimation(GradientStop.ColorProperty, colorAnimation1);
                GradientStop2.BeginAnimation(GradientStop.ColorProperty, colorAnimation2);
            });
        }

        public void HandleArguments(string[] args)
        {
            WindowModeArgument windowMode = new WindowModeArgument(args);

            if (windowMode.Found)
            {
                switch (windowMode.Option)
                {
                    case WindowModeOption.DEFAULT:
                        Application.Current.MainWindow.Show();
                        break;
                    case WindowModeOption.MIN:
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
                        Application.Current.MainWindow.Show();
                        break;
                    case WindowModeOption.HIDDEN:
                        Hide();
                        break;
                    default:
                        break;
                }
            }
            else
                Application.Current.MainWindow.Show();
        }
    }
}
