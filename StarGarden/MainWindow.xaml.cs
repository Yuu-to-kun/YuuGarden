using StarGarden.Functions.FileWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StarGarden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // page :3

            //Declaring variables
            ConfigFunctions configFunctions = new ConfigFunctions();
            var config = configFunctions.OpenConfig();

            //Routing depending on freshinstall
            if (config.installPath.Equals("") || config.installPath.Equals(null))
            {
                MainFrame.Navigate(new Uri("/pages/Freshinstall/InstallMainPage.xaml", UriKind.Relative));
            }
            else
            {
                MainFrame.Navigate(new Uri("/pages/Garden_MainScreen.xaml", UriKind.Relative));
            }
                
        }

        // Custom Top Bar Stuff
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
           

            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
    }
}
