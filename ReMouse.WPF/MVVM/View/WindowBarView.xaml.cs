using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReMouse.WPF.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour WindowBarView.xaml
    /// </summary>
    public partial class WindowBarView : UserControl
    {
        public WindowBarView()
        {
            InitializeComponent();
        }

        private void WindowBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                App.Current.MainWindow.DragMove();
        }

        private void CloseButton_Click(object sender, EventArgs args)
        {
            App.Current.Shutdown();
        }

        private void ChangeWindowStateButton_Click(object sender, EventArgs args)
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
                Button button = (Button)sender;
                button.Content = "◻";
            }
            else
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
                Button button = (Button)sender;
                button.Content = "❐";
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs args)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
