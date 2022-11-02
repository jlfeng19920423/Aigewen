using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;

namespace BloogBot.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        protected override void OnStartup(StartupEventArgs e)
        {
            //Debugger.Launch();
            //AllocConsole(); // this will launch the Console so we can see our debug text


            var mainWindow = new MainWindow(); //generate WPF window
            Current.MainWindow = mainWindow;
            mainWindow.Closed += (sender, args) => { Environment.Exit(0); };
            mainWindow.Show();

            base.OnStartup(e);

            
        }
    }
}
