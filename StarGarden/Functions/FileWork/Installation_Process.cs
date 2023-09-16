using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StarGarden.Models;
using WindowsShortcutFactory;

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
                try
                {
                    File.Copy(appPath, Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe"));
                }
                catch (System.IO.IOException)
                {
                    if (File.Exists(Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe")))
                    {
                        File.Delete(Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe"));
                        File.Copy(appPath, Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe"));
                    }
                }
                


                if (!Directory.Exists(Path.Combine(startMenuPath, "StarGarden")))
                {
                    Directory.CreateDirectory(startMenuFolder);


                }
                if (!Directory.Exists(Path.Combine(GlobalObjects.downloadPath,"Games")))
                {
                    Directory.CreateDirectory(Path.Combine(GlobalObjects.downloadPath, "Games"));
                }
                if (!File.Exists(Path.Combine(startMenuFolder, "StarGarden.lnk")))
                {
                    WindowsShortcutFactory.WindowsShortcut shortcut1 = new WindowsShortcutFactory.WindowsShortcut();
                    shortcut1.Path = Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe");
                    shortcut1.Save(Path.Combine(startMenuFolder, "StarGarden.lnk"));

                }
                if (!File.Exists(Path.Combine(desktopPath, "StarGarden.lnk")))
                {
                    WindowsShortcutFactory.WindowsShortcut shortcut1 = new WindowsShortcutFactory.WindowsShortcut();
                    shortcut1.Path = Path.Combine(GlobalObjects.downloadPath, "StarGarden.exe");
                    shortcut1.Save(Path.Combine(desktopPath, "StarGarden.lnk"));
                }


                var config = configFunctions.OpenConfig();
                config.installPath = GlobalObjects.downloadPath;
                config.fpPS4_ExePath = Path.Combine(GlobalObjects.downloadPath, "fpPS4","fpPS4.exe");

                configFunctions.SaveConfig(config);
            });
        }


    }
}
