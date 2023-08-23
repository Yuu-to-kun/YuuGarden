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

            game.FileName = "cmd.exe";
            game.WindowStyle = ProcessWindowStyle.Normal;
            game.Arguments = "/k " + config.fpPS4_ExePath + " -e " + elfLocation;

            Process.Start(game);
        }

    }
}
