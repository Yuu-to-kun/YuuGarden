using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DiscordRPC;
using StarGarden.Functions;
using StarGarden.Models.Launcher;
using StarGarden.Pages;
using Windows.Media.Protection.PlayReady;

namespace StarGarden.Models
{
    public static class GlobalObjects
    {
        //Discord RPC
        public static DiscordRpcClient DiscordRpcClient { get; set; } = new DiscordRpcClient("1147311963379073084");
        public static RichPresence RichPresence { get; set; } = new RichPresence
        {
            State = "Idling",
            Assets = new Assets()
            {
                LargeImageKey = "image_large",
                LargeImageText = ":3",
            },
            Timestamps = new Timestamps()
            {
                Start = DateTime.UtcNow
            }

        };

        //Main Console
        public static ConsoleWindow SG_Console { get; set; } = new ConsoleWindow();
        //Loading Window
        public static Window loadingWindow { get; set; } = new LoadingWindow();

        //GardenPage
        public static Garden_MainScreen GardenPage { get; set; }

        //Mutex
        public static bool aIsNewInstance = false;
        public static Mutex myMutex { get; set; } = new Mutex(true, "StarGarden", out aIsNewInstance);

        //Misc
        public static string downloadPath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarGarden");
        public static string localDataPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden");
        public static bool isFreshInstall { get; set; }
        public static bool canLoad { get; set; } = false;
        public static bool shutDown { get; set; } = false;
        // Game Entries
        public static ObservableCollection<GameEntry> GamesTemplate { get; set; } = new ObservableCollection<GameEntry>();
        public static List<string> gamesList { get; set; } = new List<string>();

        //Running Games
        public static List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler,string)> runningGames { get; set; }
            = new List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler ,string)>();

        //Checks for Animation
        public static bool isAnimating { get; set; } = false;

        // Games Output
        public static void OutputReceived(object sender, DataReceivedEventArgs args,ConsoleWindow consoleWindow)
        {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    consoleWindow.WriteLine(args.Data);
                });
        }
        public static void ErrorOutputReceived(object sender, DataReceivedEventArgs args, ConsoleWindow consoleWindow)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                consoleWindow.WriteLine(args.Data, System.Windows.Media.Brushes.Red);
            });
        }
    }
}
