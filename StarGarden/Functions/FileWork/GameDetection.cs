using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions.FileWork
{
    public class GameDetection
    {
        public List<string> Scan()
        {
            ConfigFunctions configFunc = new ConfigFunctions();
            var config = configFunc.OpenConfig();

            List<string> result = new List<string>();
            List<string> gamesDirPath = config.gamesDirPaths;

            List<string> potentialGames = new List<string>();
            foreach (var item in gamesDirPath)
            {
               var entry = Directory.GetDirectories(item);
               potentialGames.Add(entry);
            }

            foreach (var potentialGame in potentialGames)
            {
                if (File.Exists(Path.Combine(potentialGame, "eboot.bin")) 
                    && Directory.Exists(Path.Combine(potentialGame,"sce_sys")))
                {
                    result.Add(potentialGame);
                }
            }
            return result;
        }

        public string sfoPath(string gameDir)
        {
            var result = Path.Combine(gameDir,"sce_sys","param.sfo");
            return result;
        }
    }
}
