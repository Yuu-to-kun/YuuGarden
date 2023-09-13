﻿using StarGarden.Functions;
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

            GameDetection detection = new GameDetection();
            GetKey getKey = new GetKey();
            ConfigFunctions configFunctions = new ConfigFunctions();
            var config = configFunctions.OpenConfig();

            //Fresh Install or Launcher
            if (config.installPath.Equals("") || config.installPath.Equals(null))
            {
                MainFrame.Navigate(new Uri("/pages/Freshinstall/InstallMainPage.xaml", UriKind.Relative));
            }
            else
            {
                MainFrame.Navigate(new Uri("/pages/Garden_MainScreen.xaml", UriKind.Relative));
                List<string> games = detection.Scan();
                for (int i = 0; i < games.Count; i++)
                {
                    SG_Console.WriteLine($"[Recognised Game]: {getKey.GetSpecificKeyData(detection.sfoPath(games[i]),"TITLE")}");
                }
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
                    StartCleaning start = new StartCleaning();
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
