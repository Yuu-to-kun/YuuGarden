using StarGarden.Functions.FileWork;
using StarGarden.Functions.NetworkWork.Github;
using StarGarden.Models;
using StarGarden.Pages.freshInstall;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Core.AnimationMetrics;

namespace StarGarden.Pages
{
    public partial class InstallMainPage : Page
    {
        private bool isAnimating = false; // To prevent overlapping animations
        private double slideDistance = 680;

        public InstallMainPage()
        {
            InitializeComponent();

            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step1_Page.xaml", UriKind.Relative));
        }

        private async void NextButtonClick(object sender, RoutedEventArgs e)
        {
            // Animations
            DoubleAnimation slideToR = new DoubleAnimation { To = -slideDistance, Duration = TimeSpan.FromSeconds(0.2) };
            DoubleAnimation slideToL = new DoubleAnimation { From = slideDistance, To = 0, Duration = TimeSpan.FromSeconds(0.2) };
            if (!(ContentFrame.Content is Step2_Page))
            {
                if (isAnimating)
                {
                    return;
                }

                isAnimating = true;

                
                ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideToR);
                await Task.Delay(TimeSpan.FromSeconds(0.2));
            }
            
            // Page Movement
            switch (ContentFrame.Content)
            {
                case Step1_Page:
                    //Page Indicator Change
                    Indicator1.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator2.Fill = new SolidColorBrush(Colors.White);

                    //Routing
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step2_Page.xaml", UriKind.Relative));
                    break;
                case Step2_Page:
                    installPopup.Visibility = Visibility.Visible;

                    break;
                case Step3_Page:
                    break;
            }

            if (!(ContentFrame.Content is Step2_Page))
            {

                ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideToL);
                isAnimating = false;
            }


        }
        private async void PrevButtonClick(object sender, RoutedEventArgs e)
        {
            DoubleAnimation slideFromL = new DoubleAnimation { To = slideDistance, Duration = TimeSpan.FromSeconds(0.2) };
            DoubleAnimation slideToR = new DoubleAnimation { From = -slideDistance, To = 0, Duration = TimeSpan.FromSeconds(0.2) };

            if (ContentFrame.Content is Step1_Page)
            {
                return;
            }

            if (isAnimating)
            {
                return;
            }
                
            isAnimating = true;


            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideFromL);

            await Task.Delay(TimeSpan.FromSeconds(0.2)); // Adjust as needed

            //Page Movement
            switch (ContentFrame.Content)
            {
                case Step1_Page:
                    break;
                case Step2_Page:
                    //Page Indicator Change
                    Indicator2.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator1.Fill = new SolidColorBrush(Colors.White);

                    //Routing
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step1_Page.xaml", UriKind.Relative));
                    break;
            }
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideToR);

            isAnimating = false;
        }

        private async void proceedButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation slideToR = new DoubleAnimation { To = -slideDistance, Duration = TimeSpan.FromSeconds(0.2)};
            DoubleAnimation slideToL = new DoubleAnimation { From = slideDistance, To = 0, Duration = TimeSpan.FromSeconds(0.2) };
            DoubleAnimation buttonsOpacity = new DoubleAnimation { From = 1.0,To = 0.0,Duration = TimeSpan.FromSeconds(0.28),FillBehavior = FillBehavior.Stop};
            installPopup.Visibility = Visibility.Hidden;

            if (isAnimating)
            {
                return;
            }

            isAnimating = true;


            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideToR);

            await Task.Delay(TimeSpan.FromSeconds(0.2)); // Adjust as needed

            ContentFrame.RenderTransform = null;

             //Decalaring variables
             fpPS4_Download download = new fpPS4_Download();
             Installation_Process install = new Installation_Process();
             ConfigFunctions configFuncs = new ConfigFunctions();
             StarGarden_Download starGarden_Download = new StarGarden_Download();
             var config = configFuncs.OpenConfig();

             //Page Indicator Change
             Indicator2.Fill = new SolidColorBrush(Colors.Gray);
             Indicator3.Fill = new SolidColorBrush(Colors.White);

             //Routing
             ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step3_Page.xaml", UriKind.Relative));

             //Animation for disapearing buttons
             buttonsOpacity.Completed += (sender, e) =>
             {
                 NextPageButton.Visibility = Visibility.Hidden;
                 PrevButton.Visibility = Visibility.Hidden;
             };

             NextPageButton.BeginAnimation(UIElement.OpacityProperty, buttonsOpacity);
             PrevButton.BeginAnimation(UIElement.OpacityProperty, buttonsOpacity);

             await Task.Delay(buttonsOpacity.Duration.TimeSpan);

             // Start Installing
             if (config.installPath == "" || config.installPath == null)
             {

                 await download.Download(PrevButton);
                 await starGarden_Download.downloadLatestRelease();
                 await install.Install();

                 Indicator3.Fill = new SolidColorBrush(Colors.Gray);
                 Indicator4.Fill = new SolidColorBrush(Colors.White);
                 ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step4_Page.xaml", UriKind.Relative));

             }

            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideToL);

            isAnimating = false;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            installPopup.Visibility = Visibility.Hidden;
        }

    }
}