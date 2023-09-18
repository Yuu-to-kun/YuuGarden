using StarGarden.Functions;
using StarGarden.Functions.FileWork;
using StarGarden.Functions.FileWork.SFO;
using StarGarden.Models;
using StarGarden.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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
            LoadASync();

        }
        private async void LoadASync()
        {   
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    while (true)
                    {

                        if (GlobalObjects.canLoad == true)
                        {
                            break;
                        }
                    }
                });
                
            });

            //Fresh Install or Launcher
            if (GlobalObjects.isFreshInstall == true)
            {
                MainFrame.Navigate(new Uri("/pages/Freshinstall/InstallMainPage.xaml", UriKind.Relative));
            }
            else if (GlobalObjects.isFreshInstall == false)
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

        private async void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (GlobalObjects.runningGames.Count > 0)
            {
                var result = System.Windows.Forms.MessageBox.Show("You are about to shutdown all games you are running and unsaved progress will be lost. Are you sure?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);

                // Convert DialogResult to MessageBoxResult
                MessageBoxResult wpfResult = (MessageBoxResult)Enum.Parse(typeof(MessageBoxResult), result.ToString());

                if (wpfResult == MessageBoxResult.Yes)
                {
                    bg_CheckClean start = new bg_CheckClean();
                    start.Clean();
                }
                else if (wpfResult == MessageBoxResult.No)
                {
                    return;
                }
            }
           
            CloseProcessess processess = new CloseProcessess();
            await processess.Close();
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

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalObjects.SG_Console.Close();
        }
    }
}
