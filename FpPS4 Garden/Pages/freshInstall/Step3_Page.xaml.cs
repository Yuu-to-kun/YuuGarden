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

namespace FpPS4_Garden.Pages.freshInstall
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
            var rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1.2),
                RepeatBehavior = RepeatBehavior.Forever
            };

            circleGoRound.RenderTransform = new RotateTransform();
            circleGoRound.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

    }
}
