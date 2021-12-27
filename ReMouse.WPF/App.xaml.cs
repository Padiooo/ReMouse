using Hardcodet.Wpf.TaskbarNotification;
using ReMouse.WPF.Core.CommandLine;
using ReMouse.WPF.Core.DataBinding;
using ReMouse.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace ReMouse.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Read and process args given.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (new HelpArgument(e.Args).Found)
            {
                DisplayHelp(e.Args);
                Current.Shutdown(0);
            }

            TaskbarIcon icon = (TaskbarIcon)FindResource("TaskbarIcon");

            icon.ContextMenu.Items
                .Cast<System.Windows.Controls.MenuItem>()
                .FirstOrDefault(mi => mi.Header.ToString() == "Quit")
                .Command = new RelayCommand(o =>
                {
                    Current.Shutdown();
                });

            MainWindow window = new MainWindow();
            MainViewModel viewModel = new MainViewModel(window.BackgroundTransition, icon.ContextMenu);
            window.DataContext = viewModel;

            window.HandleArguments(e.Args);
            viewModel.HandleArguments(e.Args);
        }

        private void DisplayHelp(string[] args)
        {
            Type t = typeof(CommandLineArgument);
            IEnumerable<Type> types = t.Assembly.GetTypes().Where((type) => !type.IsAbstract && t.IsAssignableFrom(type));

            _ = AttachConsole(-1);
            Console.WriteLine();
            foreach (var type in types)
            {
                string help = ((CommandLineArgument)Activator.CreateInstance(type, new object[] { args })).GetHelp();

                _ = AttachConsole(-1);
                Console.WriteLine("\t" + help);
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool AttachConsole(int processId);
    }
}
