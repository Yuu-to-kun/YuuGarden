using StarGarden.Functions.FileWork;
using StarGarden.Models.Launcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    public class StartGame
    {
        public void Start(string elfLocation)
        {
            ConfigFunctions configFunctions = new ConfigFunctions();
            ProcessStartInfo game = new ProcessStartInfo();
            var config = configFunctions.OpenConfig();

            game.FileName = config.fpPS4_ExePath;
            game.WindowStyle = ProcessWindowStyle.Normal;
            game.Arguments = "/k -e " + elfLocation;
            game.CreateNoWindow = false;
            game.UseShellExecute = false;

            Process process = new Process();

            process.StartInfo = game;
            process.Start();
        }

    }
}
