using StarGarden.Functions.FileWork;
using StarGarden.Models;
using StarGarden.Models.Launcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    public class StartGame
    {
        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);
        public void Start(string elfLocation, string gameName, string gameIcon)
        {
            ConfigFunctions configFunctions = new ConfigFunctions();
            var config = configFunctions.OpenConfig();

            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/c \"{config.fpPS4_ExePath} -e {elfLocation} & pause\"";
            startInfo.CreateNoWindow = false;
            p.StartInfo = startInfo;
            p.Start();

            var presence = GlobalObjects.RichPresence;
            presence.State = $"{gameName}";
            presence.Timestamps.Start = DateTime.UtcNow;

            GlobalObjects.DiscordRpcClient.SetPresence(presence);

            p.WaitForExit();
            presence.State = $"Idling";
            presence.Timestamps.Start = DateTime.UtcNow;
            GlobalObjects.DiscordRpcClient.SetPresence(presence);

            SG_Console.WriteLine("Game has been stopped");
        }

    }
}
