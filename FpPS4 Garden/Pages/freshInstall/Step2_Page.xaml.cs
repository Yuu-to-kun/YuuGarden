using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using FpPS4_Garden.Models;

namespace FpPS4_Garden.Pages
{
    /// <summary>
    /// Interaction logic for Step1_Page.xaml
    /// </summary>
    public partial class Step2_Page : Page
    {
        
        public Step2_Page()
        {
            InitializeComponent();

            FolderPathTextBox.Text = Misc.downloadPath;
        }

        private void BrowseInstallFolder_Click(object sender, RoutedEventArgs e)
        {
            // Declaring Variables
            FolderBrowserDialog installFolderDialog = new FolderBrowserDialog();
            DialogResult result = installFolderDialog.ShowDialog();
            var installFolder = installFolderDialog.SelectedPath;

            // Browse Folder Dialog
            if(result == DialogResult.OK)
            {
                FolderPathTextBox.Text = System.IO.Path.Combine(installFolder, "Fp_Garden");
            }
            Misc.downloadPath = System.IO.Path.Combine(installFolder, "Fp_Garden");
        }
    }
}
