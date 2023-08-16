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

namespace StarGarden.Pages
{
    /// <summary>
    /// Interaction logic for Garden_MainScreen.xaml
    /// </summary>
    public partial class Garden_MainScreen : Page
    {
        public Garden_MainScreen()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }

    public class Game
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
    }

    public class MainViewModel
    {
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();

        public MainViewModel()
        {
            Games.Add(new Game { Name = "Game 1", Description = "Description 1", ImageSource = "https://fpps4.net/images/NA.jpg" });
            Games.Add(new Game { Name = "Game 2", Description = "Description 2", ImageSource = "https://fpps4.net/images/NA.jpg" });
            Games.Add(new Game { Name = "Game 3", Description = "Description 3", ImageSource = "https://fpps4.net/images/NA.jpg" });
            Games.Add(new Game { Name = "Game 4", Description = "This is something", ImageSource = "https://fpps4.net/images/NA.jpg" });
            Games.Add(new Game { Name = "Game 5", Description = "grjyabgr yejv", ImageSource = "https://fpps4.net/images/NA.jpg" });
        }
    }
}