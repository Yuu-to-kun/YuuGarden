using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    public class StartCleaning
    {
        public void Clean()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {

                FileName = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StarGarden", "bgProccesses", "bg.exe")),
                CreateNoWindow = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,

            };
            Process p = new Process()
            {
                StartInfo = startInfo
            };
            try
            {
                p.Start();
            }
            catch (Exception)
            {

                throw new Exception("Missing bg.exe");
            }
            
        }
    }
}
