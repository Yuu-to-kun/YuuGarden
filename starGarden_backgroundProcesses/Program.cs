using starGarden_backgroundProcesses.Models;
using System.IO;
string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "StarGarden","bgProccesses");
ConfigFuncs config = new ConfigFuncs();
List<int> list = new List<int>();

if (!Directory.Exists(configFolderPath))
{
    DirectoryInfo directoryInfo = Directory.CreateDirectory(configFolderPath);
    directoryInfo.Attributes |= FileAttributes.Hidden;
    config.Create(list, configFolderPath);

}

while (true)
{
    if (config.OpenBGP_Config(Path.Combine(configFolderPath)).proccessIds.Count == 0)
    {
        break;
    }
    Console.WriteLine("Monitoring Processes");
    Task.Delay(3000).Wait();
}