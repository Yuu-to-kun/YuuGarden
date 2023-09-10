using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DiscordRPC;
using StarGarden.Functions;
using StarGarden.Pages;
using Windows.Media.Protection.PlayReady;

namespace StarGarden.Models
{
    public static class GlobalObjects
    {
        public static DiscordRpcClient DiscordRpcClient { get; set; } = new DiscordRpcClient("1147311963379073084");
        public static System.Timers.Timer Timer { get; set; } = new System.Timers.Timer(1000);
        public static DateTime currentTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 0, 0);
        public static ConsoleWindow SG_Console { get; set; } = new ConsoleWindow();
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
        public static List<Process> ProcessesList { get; set; } = new List<Process>();

        public static List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler,string)> runningGames { get; set; }
            = new List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler ,string)>();

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
