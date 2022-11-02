using System;
using System.Windows;
using System.Windows.Threading;
using BloogBot.Game;


namespace BloogBot.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var context = (MainViewModel)DataContext;
            context.InitializeObjectManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
