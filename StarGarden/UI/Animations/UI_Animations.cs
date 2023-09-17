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

namespace StarGarden.UI.Animations
{
    public class UI_Animations
    {
        //MainMenu Animations
        public async void settingsUnload(Border settingMenuChild,Border settingMenu)
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

            settingMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            settingMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(200);
            settingMenu.Visibility = Visibility.Hidden;
        }
        public async void settingsLoad(Border settingMenuChild, Border settingMenu)
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

            settingMenu.Visibility = Visibility.Visible;
            settingMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            settingMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
        }
        public async void updatesMenuUnload(Border updateMenuChild, Border updateMenu)
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

            updateMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            updateMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(200);
            updateMenu.Visibility = Visibility.Hidden;
        }
        public async void updatesMenuLoad(Border updateMenuChild, Border updateMenu)
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

            updateMenu.Visibility = Visibility.Visible;
            updateMenuChild.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            updateMenuChild.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
        }
        public async void gameClick(Border gamePopup,Grid mainGrid,ScrollViewer scrollViewer,Button playButton)
        {
            // animation
            double targetHeight = gamePopup.ActualHeight;
            gamePopup.RenderTransform = new TranslateTransform(0, targetHeight);
            if (!mainGrid.Children.Contains(gamePopup))
            {
                mainGrid.Children.Add(gamePopup);
            }
            gamePopup.Visibility = Visibility.Visible;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

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

            gamePopup.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            gamePopup.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);

            await Task.Delay(slideAnimation.Duration.TimeSpan + TimeSpan.FromSeconds(0.2));
            playButton.Visibility = Visibility.Visible;
        }
        public async void gamePopUpExit(Border gamePopup, Grid mainGrid)
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
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            gamePopup.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            gamePopup.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            await Task.Delay(200);
            gamePopup.Visibility = Visibility.Hidden;
            mainGrid.Children.Remove(gamePopup);
        }
    }
}
