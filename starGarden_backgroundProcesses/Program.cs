using starGarden_backgroundProcesses.Models;
using System.IO;
string configFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "StarGarden","bgProccesses");
if (!Directory.Exists(configFolderPath))
{
    DirectoryInfo directoryInfo = Directory.CreateDirectory(configFolderPath);
    directoryInfo.Attributes |= FileAttributes.Hidden;
    ConfigFuncs config = new ConfigFuncs();
    List<int> list = new List<int>();
    config.Create(list, configFolderPath);
}