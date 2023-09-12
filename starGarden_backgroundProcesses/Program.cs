using starGarden_backgroundProcesses.Models;
using System.IO;
string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "StarGarden","bgProccesses");
ConfigFuncs config = new ConfigFuncs();
List<int> list = new List<int>();
config.Create(list, configFolderPath);
var configFile = config.OpenBGP_Config(configFolderPath);

if (!Directory.Exists(configFolderPath))
{
    DirectoryInfo directoryInfo = Directory.CreateDirectory(configFolderPath);
    directoryInfo.Attributes |= FileAttributes.Hidden;
    


    
}

while (!(configFile.proccessIds.Count == 0))
{
    Console.WriteLine("Monitoring Processes");
    Task.Delay(3000).Wait();
}