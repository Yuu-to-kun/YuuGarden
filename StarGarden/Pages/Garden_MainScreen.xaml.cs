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

namespace StarGarden.Pages
{
    /// <summary>
    /// Interaction logic for Garden_MainScreen.xaml
    /// </summary>
    public partial class Garden_MainScreen : Page
    {
        private List<string> games = new List<string>();
        public Garden_MainScreen()
        {
            GameDetection detection = new GameDetection();
            games = detection.Scan();
            InitializeComponent();
            DataContext = new MainViewModel(games,detection);
        }
    }

    public class MainViewModel
    {
        public ObservableCollection<GameEntry> Games { get; } = new ObservableCollection<GameEntry>();

        List<string> _games;
        GameDetection _detection;
        public MainViewModel(List<string> games, GameDetection detection)
        {
            _games = games;
            _detection = detection;

            GetKey getKey = new GetKey();
            for (int i = 0; i < _games.Count; i++)
            {
                Games.Add(new GameEntry { Name = $"{getKey.GetKeyData(_detection.sfoPath(games[i]), "TITLE")}", Description = "Description 1", ImageSource = "https://fpps4.net/images/NA.jpg" });
            }
        }
    }
}