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

namespace StarGarden.Pages.freshInstall
{
    /// <summary>
    /// Interaction logic for Step3_Page.xaml
    /// </summary>
    public partial class Step3_Page : Page
    {
        public Step3_Page()
        {
            InitializeComponent();
            StartLoadingAnimation();

            
        }

        private void StartLoadingAnimation()
        {
            //Loading circle animation

            var rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1.2),
                RepeatBehavior = RepeatBehavior.Forever
            };

            var gradientBrush = new LinearGradientBrush();
            var gradientStops = gradientBrush.GradientStops;
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 5);
            gradientStops.Add(new GradientStop(Colors.Red, 0.0));
            gradientStops.Add(new GradientStop(Colors.Orange, 0.3));
            gradientStops.Add(new GradientStop(Colors.Yellow, 0.6));
            gradientStops.Add(new GradientStop(Colors.Green, 0.9));
            gradientStops.Add(new GradientStop(Colors.Blue, 1.2));
            gradientStops.Add(new GradientStop(Colors.Violet, 1.5));

            circleGoRound.Stroke = gradientBrush;

            circleGoRound.RenderTransform = new RotateTransform();
            circleGoRound.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

    }
}
