﻿using System;
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
        
        //Misc

        public static string downloadPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StarGarden");


        //Running Games
        public static List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler,string)> runningGames { get; set; }
            = new List<(Process, ConsoleWindow, DataReceivedEventHandler, DataReceivedEventHandler ,string)>();


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