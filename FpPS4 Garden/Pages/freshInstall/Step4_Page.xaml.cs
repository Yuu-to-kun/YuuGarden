using FpPS4_Garden.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FpPS4_Garden.Pages.freshInstall
{
    /// <summary>
    /// Interaction logic for Step4_Page.xaml
    /// </summary>
    public partial class Step4_Page : Page
    {
        public Step4_Page()
        {
            InitializeComponent();
        }

        private void BrowseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            //Star Process Information
            ProcessStartInfo startinfo = new ProcessStartInfo { 
                FileName = "explorer.exe",
                Arguments = Misc.downloadPath,
                WindowStyle = ProcessWindowStyle.Normal,
            };
            Process.Start(startinfo);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        private void FinishInstallationButton_Click(object sender, RoutedEventArgs e)
        {
            // Finish Installation
            if (LaunchOnClose.IsChecked == true)
            {
                Application.Current.Shutdown();
                System.Windows.Forms.Application.Restart();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
