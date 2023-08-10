using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FpPS4_Garden.Models;

namespace FpPS4_Garden.Functions.FileWork
{
    public class Installation_Process
    {
        


        public async Task Install()
        {
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            string startMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Start Menu", "Programs");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string startMenuFolder = Path.Combine(startMenuPath, "Fp_Garden");
            ConfigFunctions configFunctions = new ConfigFunctions();

            await Task.Run(() =>
            {
                File.Copy(appPath, Path.Combine(Misc.downloadPath, "FP Garden.exe"));


                if (!Directory.Exists(Path.Combine(startMenuPath, "Fp_Garden")))
                {
                    Directory.CreateDirectory(startMenuFolder);


                }
                if (!File.Exists(Path.Combine(startMenuFolder, "FP Garden.lnk")))
                {
                    CreateShortcut shortcut = new CreateShortcut();

                    shortcut.startMenuShortcut(Path.Combine(startMenuFolder, "FP Garden.lnk"), Path.Combine(Misc.downloadPath, "FP Garden.exe"));
                }
                if (!File.Exists(Path.Combine(desktopPath, "FP Garden.lnk")))
                {
                    CreateShortcut shortcut = new CreateShortcut();

                    shortcut.startMenuShortcut(Path.Combine(desktopPath, "FP Garden.lnk"), Path.Combine(Misc.downloadPath, "FP Garden.exe"));
                }

                var config = configFunctions.OpenConfig();
                config.installPath = Misc.downloadPath;

                configFunctions.SaveConfig(config);
            });
        }


    }
}
