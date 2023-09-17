using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using StarGarden.Pages;
using StarGarden.Pages.freshInstall;
using StarGarden.Models;

namespace StarGarden.UI.Animations
{
    public class UI_Animations
    {
        //MainMenu Animations
        public async Task settingsUnload(Border settingMenuChild,Border settingMenu)
        {
            double targetHeight = settingMenuChild.ActualHeight;
            settingMenuChild.RenderTransform = new TranslateTransform(0, targetHeight);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = 0,
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            settingMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            settingMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(fadeAnimation.Duration.TimeSpan +TimeSpan.FromSeconds(0.2));
            settingMenu.Visibility = Visibility.Hidden;
            GlobalObjects.isAnimating = false;
        }
        public async Task settingsLoad(Border settingMenuChild, Border settingMenu)
        {
            double targetHeight = settingMenuChild.ActualHeight;
            settingMenuChild.RenderTransform = new TranslateTransform(0, targetHeight);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = targetHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            settingMenu.Visibility = Visibility.Visible;
            settingMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            settingMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(fadeAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            GlobalObjects.isAnimating = false;
        }
        public async Task updatesMenuUnload(Border updateMenuChild, Border updateMenu)
        {
            double targetHeight = updateMenuChild.ActualHeight;
            updateMenuChild.RenderTransform = new TranslateTransform(0, targetHeight);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = 0,
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            updateMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            updateMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(fadeAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            updateMenu.Visibility = Visibility.Hidden;
            GlobalObjects.isAnimating = false;
        }
        public async Task updatesMenuLoad(Border updateMenuChild, Border updateMenu)
        {
            
            double targetHeight = updateMenuChild.ActualHeight;
            updateMenuChild.RenderTransform = new TranslateTransform(0, targetHeight);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = targetHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            updateMenu.Visibility = Visibility.Visible;
            updateMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            updateMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(fadeAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            GlobalObjects.isAnimating = false;

        }
        public async Task gameClick(Border gamePopup,ScrollViewer scrollViewer)
        {
            // animation
            double targetHeight = gamePopup.ActualHeight;
            gamePopup.RenderTransform = new TranslateTransform(0, targetHeight);
            gamePopup.Visibility = Visibility.Visible;
            
            

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = targetHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            gamePopup.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            gamePopup.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
            scrollViewer.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);

            await Task.Delay(fadeOutAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            GlobalObjects.isAnimating = false;
        }
        public async Task gamePopUpExit(Border gamePopup,Grid mainGrid,ScrollViewer scrollViewer)
        {
            double targetHeight = gamePopup.ActualHeight;
            gamePopup.RenderTransform = new TranslateTransform(0, targetHeight);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = 0,
                To = targetHeight,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            if (GlobalObjects.isAnimating == true)
            {
                return;
            }

            GlobalObjects.isAnimating = true;
            gamePopup.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            gamePopup.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
            await Task.Delay(fadeInAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            
            gamePopup.Visibility = Visibility.Hidden;
            GlobalObjects.isAnimating = false;
        }
    }
}
