﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StarGarden.Models.Launcher;
using StarGarden.Functions.FileWork;
using StarGarden.Functions.FileWork.SFO;
using System.ComponentModel;
using StarGarden.Functions;
using System.Windows.Media.Animation;

namespace StarGarden.Pages
{
    /// <summary>
    /// Interaction logic for Garden_MainScreen.xaml
    /// </summary>
    public partial class Garden_MainScreen : Page
    {

        private List<string> games = new List<string>();
        public ObservableCollection<GameEntry> GamesTemplate { get; } = new ObservableCollection<GameEntry>();
        
        public GameEntry checkedGame = null;
        
        public Garden_MainScreen()
        {
            GameDetection detection = new GameDetection();
            games = detection.Scan();
            

            GetKey getKey = new GetKey();
            for (int i = 0; i < games.Count; i++)
            {
                GameEntry gameEntry = new GameEntry
                {
                    Name = $"{getKey.GetSpecificKeyData(detection.sfoPath(games[i]), "TITLE")}",
                    Cusa = $"{getKey.GetSpecificKeyData(detection.sfoPath(games[i]), "TITLE_ID").ToString()}",
                    ImageSource = $"{System.IO.Path.Combine(games[i],"sce_sys","icon0.png")}",
                    GamePath= games[i],
                    
                };

                GamesTemplate.Add(gameEntry);
                

            }

            InitializeComponent();
            DataContext = this;

            // fix game being centerd when less then 4 games are displayed
            if (games.Count < 4) 
            {
                ItemsControl.HorizontalAlignment = HorizontalAlignment.Left;

            } else if (games.Count > 4 || games.Count == 4)
            {
                ItemsControl.HorizontalAlignment = HorizontalAlignment.Center;
            }

        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame game = new StartGame();
            game.Start($"\"{checkedGame.ElfLoc}\"",
                checkedGame.Name,System.IO.Path.Combine(checkedGame.GamePath,"SG_Logs"),System.IO.Path.Combine(checkedGame.GamePath,"sce_sys","icon0.png"));
            
        }

        private async void settingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (settingMenu.Visibility == Visibility.Visible)
            {
                // UnLoad animation
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

            } else if (settingMenu.Visibility == Visibility.Hidden)
            {
                // Load animation
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
        }

        private async void gameClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var entry = (GameEntry)button.DataContext;
            GetKey getKey = new GetKey();


            checkedGame = entry;

            SG_Console.WriteLine($"{entry.Name} has been selected!");

            gamePopupTitle.Text = entry.Name;
            gamePopupCode.Text = getKey.GetSpecificKeyData(entry.SfoPath, "TITLE_ID").ToString();

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
                EasingFunction = new CubicEase{EasingMode = EasingMode.EaseOut}
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

        private void gamePopUpExit(object sender, RoutedEventArgs e)
        {
            gamePopup.Visibility = Visibility.Hidden;
            playButton.Visibility = Visibility.Hidden;
            mainGrid.Children.Remove(gamePopup);
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;


        }
    }


}