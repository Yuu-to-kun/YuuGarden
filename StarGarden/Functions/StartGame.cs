using StarGarden.Functions.FileWork;
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
        public void Start(string elfLocation)
        {
            ConfigFunctions configFunctions = new ConfigFunctions();
            ProcessStartInfo game = new ProcessStartInfo();
            var config = configFunctions.OpenConfig();
            var logLoc = Path.Combine(Path.GetDirectoryName(config.fpPS4_ExePath), "game_log.txt");

            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/c \"{config.fpPS4_ExePath} -e {elfLocation} & pause\"";
            startInfo.CreateNoWindow = false;
            p.StartInfo = startInfo;
            p.Start();

            p.WaitForExit();
            SG_Console.WriteLine("Game has been stopped");
        }

    }
}
