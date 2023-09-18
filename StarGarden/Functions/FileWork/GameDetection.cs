using StarGarden.Functions.FileWork.SFO;
using StarGarden.Models;
using StarGarden.Models.Launcher;
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
                foreach (var item2 in Directory.GetDirectories(item))
                {
                    potentialGames.Add(item2);
                }
               
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

        public void GenerateEntries()
        {
            GlobalObjects.gamesList = Scan();


            GetKey getKey = new GetKey();
            for (int i = 0; i < GlobalObjects.gamesList.Count; i++)
            {
                GameEntry gameEntry = new GameEntry
                {
                    Name = $"{getKey.GetSpecificKeyData(sfoPath(GlobalObjects.gamesList[i]), "TITLE")}",
                    Cusa = $"{getKey.GetSpecificKeyData(sfoPath(GlobalObjects.gamesList[i]), "TITLE_ID").ToString()}",
                    ImageSource = $"{System.IO.Path.Combine(GlobalObjects.gamesList[i], "sce_sys", "icon0.png")}",
                    GamePath = GlobalObjects.gamesList[i],
                    Pic1 = new System.Windows.Media.Imaging.BitmapImage(new Uri(System.IO.Path.Combine(GlobalObjects.gamesList[i], "sce_sys", "pic1.png"))),
                    
                
                };

                GlobalObjects.GamesTemplate.Add(gameEntry);


            }
        }
    }
}
