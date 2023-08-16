using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StarGarden.Models;

namespace StarGarden.Functions.FileWork
{
    public class Installation_Process
    {
        


        public async Task Install()
        {
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            string startMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Start Menu", "Programs");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string startMenuFolder = Path.Combine(startMenuPath, "StarGarden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            await Task.Run(() =>
            {
                File.Copy(appPath, Path.Combine(Misc.downloadPath, "StarGarden.exe"));


                if (!Directory.Exists(Path.Combine(startMenuPath, "StarGarden")))
                {
                    Directory.CreateDirectory(startMenuFolder);


                }
                if (!Directory.Exists(Path.Combine(Misc.downloadPath,"Games")))
                {
                    Directory.CreateDirectory(Path.Combine(Misc.downloadPath, "Games"));
                }
                if (!File.Exists(Path.Combine(startMenuFolder, "StarGarden.lnk")))
                {
                    CreateShortcut shortcut = new CreateShortcut();

                    shortcut.startMenuShortcut(Path.Combine(startMenuFolder, "StarGarden.lnk"), Path.Combine(Misc.downloadPath, "StarGarden.exe"));
                }
                if (!File.Exists(Path.Combine(desktopPath, "StarGarden.lnk")))
                {
                    CreateShortcut shortcut = new CreateShortcut();

                    shortcut.startMenuShortcut(Path.Combine(desktopPath, "StarGarden.lnk"), Path.Combine(Misc.downloadPath, "StarGarden.exe"));
                }


                var config = configFunctions.OpenConfig();
                config.installPath = Misc.downloadPath;
                config.gamesDirPath = Path.Combine(Misc.downloadPath, "Games");
                config.fpPS4_ExePath = Path.Combine(Misc.downloadPath, "fpPS4","fpPS4.exe");

                configFunctions.SaveConfig(config);
            });
        }


    }
}
