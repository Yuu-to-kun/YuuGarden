using StarGarden.Functions.FileWork;
using StarGarden.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using Windows.Storage;
using StarGarden.Functions;
using StarGarden.Pages;
using DiscordRPC;
using Windows.Media.Protection.PlayReady;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Runtime.InteropServices;

namespace StarGarden
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        // Gitea Console Imports
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        // Gitea Console Options
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_SHOWMINIMIZED = 2;
        

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            // Mutex for detecting multiple sessions
            if (!GlobalObjects.aIsNewInstance)
            {
                MessageBox.Show("There is an instance is already running...");
                App.Current.Shutdown();
            }

            //For Gitea Artifact
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //Run bg.exe to clean up left over processes
            bg_CheckClean start = new bg_CheckClean();
            start.Clean();

            // Dispose of the Discord Rpc
            GlobalObjects.DiscordRpcClient.Dispose();
        }


    }



}
