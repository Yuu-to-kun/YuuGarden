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


        private async void updatesButtonClick(object sender, RoutedEventArgs e)
        {
            UI_Animations animations = new UI_Animations();
            switch (updateMenu.Visibility)
            {
                case Visibility.Visible:
                    animations.updatesMenuUnload(updateMenuChild, updateMenu);
                    break;
                case Visibility.Hidden:
                    if (settingMenu.Visibility == Visibility.Visible)
                    {
                        settingMenu.Visibility = Visibility.Hidden;
                    }
                    animations.updatesMenuLoad(updateMenuChild, updateMenu);
                    break;
            }
        }


        private async void settingsButtonClick(object sender, RoutedEventArgs e)
        {
            UI_Animations animations = new UI_Animations();
            switch (settingMenu.Visibility)
            {
                case Visibility.Visible:
                    animations.settingsUnload(settingMenuChild, settingMenu);
                    break;
                case Visibility.Hidden:
                    if (updateMenu.Visibility == Visibility.Visible)
                    {
                        updateMenu.Visibility = Visibility.Hidden;
                    }
                    animations.settingsLoad(settingMenuChild, settingMenu);
                    break;
            }
        }

        private async void gameClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var entry = (GameEntry)button.DataContext;
            GetKey getKey = new GetKey();

            UI_Animations animations = new UI_Animations();
            checkedGame = entry;

            SG_Console.WriteLine($"{entry.Name} has been selected!");

            gamePopupTitle.Text = entry.Name;
            gamePopupCode.Text = getKey.GetSpecificKeyData(entry.SfoPath, "TITLE_ID").ToString();

            animations.gameClick(gamePopup, mainGrid, scrollViewer);
        }

        private async void gamePopUpExit(object sender, RoutedEventArgs e)
        {
            UI_Animations animations = new UI_Animations();
            
            animations.gamePopUpExit(gamePopup, mainGrid, scrollViewer);
            scrollViewer.Opacity = 1;
            scrollViewer.Visibility = Visibility.Visible;
        }
    }


}