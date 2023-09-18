using System;
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
using StarGarden.UI.Animations;
using StarGarden.Models;

namespace StarGarden.Pages
{
    /// <summary>
    /// Interaction logic for Garden_MainScreen.xaml
    /// </summary>
    public partial class Garden_MainScreen : Page
    {

        public ObservableCollection<GameEntry> GamesTemplate { get { return GlobalObjects.GamesTemplate; }}
        public GameEntry checkedGame = null;
        
        public Garden_MainScreen()
        {
            DataContext = this;

            InitializeComponent();

            // fix game being centerd when less then 4 games are displayed
            if (GlobalObjects.gamesList.Count < 4) 
            {
                ItemsControl.HorizontalAlignment = HorizontalAlignment.Left;

            } else if (GlobalObjects.gamesList.Count > 4 || GlobalObjects.gamesList.Count == 4)
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


        private async void updatesButtonClick(object sender, RoutedEventArgs e)
        {
            if (!GlobalObjects.isAnimating)
            {
                UI_Animations animations = new UI_Animations();
                switch (updateMenu.Visibility)
                {
                    case Visibility.Visible:
                        inputStealer.Visibility = Visibility.Hidden;
                        await animations.updatesMenuUnload(updateMenu, updateMenu);
                        break;
                    case Visibility.Hidden:
                        if (settingMenu.Visibility == Visibility.Visible)
                        {
                            inputStealer.Visibility = Visibility.Hidden;
                            settingMenu.Visibility = Visibility.Hidden;
                        }
                        inputStealer.Visibility = Visibility.Visible;
                        await animations.updatesMenuLoad(updateMenu, updateMenu);
                        break;
                }
            }
        }


        private async void settingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (!GlobalObjects.isAnimating)
            {
                UI_Animations animations = new UI_Animations();
                switch (settingMenu.Visibility)
                {
                    case Visibility.Visible:
                        inputStealer.Visibility = Visibility.Hidden;
                        await animations.settingsUnload(settingMenu, settingMenu);
                        break;
                    case Visibility.Hidden:
                        if (updateMenu.Visibility == Visibility.Visible)
                        {
                            inputStealer.Visibility = Visibility.Hidden;
                            updateMenu.Visibility = Visibility.Hidden;
                        }
                        inputStealer.Visibility = Visibility.Visible;
                        await animations.settingsLoad(settingMenu, settingMenu);
                        break;
                }
            }
        }

        private void InputStealerClick(object sender, RoutedEventArgs e)
        {
            if (settingMenu.Visibility == Visibility.Visible)
            {
                settingsButtonClick(sender, e);

            } else if (updateMenu.Visibility == Visibility.Visible)
            {
                updatesButtonClick(sender, e);
            } else
            {
                SG_Console.WriteLine("How did you click me?");
            }
           
        }

        private void terminalButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private async void gameClick(object sender, RoutedEventArgs e)
        {
            if (!GlobalObjects.isAnimating)
            {
                var button = (Button)sender;
                var entry = (GameEntry)button.DataContext;

                UI_Animations animations = new UI_Animations();
                checkedGame = entry;

                SG_Console.WriteLine($"{entry.Name} has been selected!");

                gamePopupTitle.Text = entry.Name;
                gamePopupCode.Text = entry.Cusa;
                if (entry.Pic1 != null)
                {
                    gamePopupImageBrush.ImageSource = checkedGame.Pic1;
                }

                await animations.gameClick(gamePopup, scrollViewer);
            }

        }

        private async void gamePopUpExit(object sender, RoutedEventArgs e)
        {
            if (!GlobalObjects.isAnimating)
            {
                UI_Animations animations = new UI_Animations();

                await animations.gamePopUpExit(gamePopup, mainGrid, scrollViewer);
                scrollViewer.Opacity = 1;
                scrollViewer.Visibility = Visibility.Visible;
            }
        }

    }
}