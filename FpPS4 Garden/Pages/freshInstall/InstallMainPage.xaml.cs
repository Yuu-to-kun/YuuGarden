using FpPS4_Garden.Pages.freshInstall;
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

namespace FpPS4_Garden.Pages
{
    public partial class InstallMainPage : Page
    {
        private bool isAnimating = false; // To prevent overlapping animations

        public InstallMainPage()
        {
            InitializeComponent();

            // Go to page one on start
            ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step1_Page.xaml", UriKind.Relative));
        }

        private async void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (isAnimating)
                return;

            isAnimating = true;

            double slideDistance = 680;

            // Create a slide animation to the left
            var slideAnimation = new DoubleAnimation
            {
                From = 0,
                To = -slideDistance,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);

            await Task.Delay(TimeSpan.FromSeconds(0.2)); // Adjust as needed

            // After animation completion, navigate to the next page
            ContentFrame.RenderTransform = null; // Reset the transform

            switch (ContentFrame.Content)
            {
                case Step1_Page:
                    Indicator1.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator2.Fill = new SolidColorBrush(Colors.White);
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step2_Page.xaml", UriKind.Relative));
                    break;
                case Step2_Page:
                    Indicator2.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator3.Fill = new SolidColorBrush(Colors.White);
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step3_Page.xaml", UriKind.Relative));
                    break;
                case Step3_Page:
                    Indicator3.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator1.Fill = new SolidColorBrush(Colors.White);
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step1_Page.xaml", UriKind.Relative));
                    break;
            }

           

            // Create a slide-in animation for the new page
            var slideInAnimation = new DoubleAnimation
            {
                From = slideDistance,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideInAnimation);

            isAnimating = false;
        }
        private async void PrevButtonClick(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.Content is Step1_Page)
            {
                return;
            }

            if (isAnimating)
                return;

            isAnimating = true;

            double slideDistance = 680;

            // Create a slide animation to the left
            var slideAnimation = new DoubleAnimation
            {
                From = 0,
                To = slideDistance,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);

            await Task.Delay(TimeSpan.FromSeconds(0.2)); // Adjust as needed

            // After animation completion, navigate to the next page
            ContentFrame.RenderTransform = null; // Reset the transform

            switch (ContentFrame.Content)
            {
                case Step1_Page:
                    break;
                case Step2_Page:
                    Indicator2.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator1.Fill = new SolidColorBrush(Colors.White);
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step1_Page.xaml", UriKind.Relative));
                    break;
                case Step3_Page:
                    Indicator3.Fill = new SolidColorBrush(Colors.Gray);
                    Indicator2.Fill = new SolidColorBrush(Colors.White);
                    ContentFrame.NavigationService?.Navigate(new Uri("/Pages/Freshinstall/Step2_Page.xaml", UriKind.Relative));
                    break;
            }



            // Create a slide-in animation for the new page
            var slideInAnimation = new DoubleAnimation
            {
                From = -slideDistance,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            // Apply the animation to the Frame's content
            ContentFrame.RenderTransform = new TranslateTransform();
            ContentFrame.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideInAnimation);

            isAnimating = false;
        }
    }
}