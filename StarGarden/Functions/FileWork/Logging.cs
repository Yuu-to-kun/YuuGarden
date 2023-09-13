using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace StarGarden.Functions.FileWork
{
    public class Logging
    {
        public void Save(string logLoc,string gameName)
        {

            if (!Directory.Exists(logLoc))
            {
                Directory.CreateDirectory(logLoc);
            }
            if (Directory.GetFiles(logLoc, "log*").Length < 10)
            {
                var tr = new TextRange(GlobalObjects.SG_Console.textBox.Document.ContentStart, GlobalObjects.SG_Console.textBox.Document.ContentEnd);
                File.WriteAllText(Path.Combine(logLoc, $"log_{DateTime.Now.ToString("dd_MM_yyyy_HH'hr'mm'min'ss'sec'")}_.txt"), tr.Text.Substring(
                tr.Text.IndexOf("GameLog") + "GameLog".Length));

                //Tries to see if the app has not been shutdown
                try
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SG_Console.WriteLine($"Saving Log for {gameName}");
                    });
                }
                catch (TaskCanceledException)
                {

                    return;
                }
            }
            else
            {
                var fileList = Directory.GetFiles(logLoc, "log*");
                var oldestFile = fileList.OrderBy(f => File.GetCreationTime(f)).First();
                File.Delete(oldestFile);

                var tr = new TextRange(GlobalObjects.SG_Console.textBox.Document.ContentStart, GlobalObjects.SG_Console.textBox.Document.ContentEnd);

                File.WriteAllText(Path.Combine(logLoc, $"log_{DateTime.Now.ToString("dd_MM_yyyy_HH'hr'mm'min'ss'sec'")}_.txt"), tr.Text.Substring(
                tr.Text.IndexOf("GameLog") + "GameLog".Length));

                //Tries to see if the app has not been shutdown
                if (System.Windows.Application.Current != null)
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SG_Console.WriteLine($"Saving Log for {gameName}");
                        });
                    }
                    catch (TaskCanceledException)
                    {

                        return;
                    }
                }
               
            }
        }
    }
}
