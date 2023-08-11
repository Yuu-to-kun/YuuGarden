using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.IO;

namespace FpPS4_Garden.Functions.FileWork
{
    public class CreateShortcut
    {
        public void startMenuShortcut(string shortcutPath, string targetPath)
        {
            WshShell shell = new WshShell();

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
            shortcut.Save();
        }
    }
}
