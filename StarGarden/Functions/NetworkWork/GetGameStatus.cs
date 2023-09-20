using Newtonsoft.Json;
using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions.NetworkWork
{
    public class Cusacode
    {
        public string id { get; set; }
        public string tag { get; set; }
    }

    public class Homebrew
    {
        public string title { get; set; }
        public string tag { get; set; }
    }

    public class Snowy_Root
    {
        public List<Cusacode> cusacode { get; set; }
        public List<Homebrew> homebrew { get; set; }
    }
    public class GetGameStatus
    {
        public Snowy_Root parseNormalGame()
        {
            using (var client = new HttpClient())
            {
                string cusaUrl = "&cusa=";
                string homebrewUrl = "&homebrew=";
                for (int i = 0; i < GlobalObjects.GamesTemplate.Count; i++)
                {
                    if (GlobalObjects.GamesTemplate[i].Cusa.StartsWith("CUSA"))
                    {
                        if (cusaUrl == "&cusa=")
                        {
                            cusaUrl = $"{cusaUrl}" + $"{GlobalObjects.GamesTemplate[i].Cusa}";
                        }
                        else
                        {
                            cusaUrl = $"{cusaUrl}" + $",{GlobalObjects.GamesTemplate[i].Cusa}";

                        }
                    }
                    else
                    {
                        if (homebrewUrl == "&homebrew=")
                        {
                            homebrewUrl = $"{homebrewUrl}" + $"{GlobalObjects.GamesTemplate[i].Name}";
                        }
                        else
                        {
                            homebrewUrl = $"{homebrewUrl}" + $",{GlobalObjects.GamesTemplate[i].Name}";

                        }
                    }
                    
                }
                string url = "https://fpps4.net/scripts/api.php?token=3g4YNf7XvchD" + cusaUrl + homebrewUrl;

                Uri endpoint = new Uri(url);
                var responseMsg = client.GetAsync(endpoint).Result;
                string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                Snowy_Root result = JsonConvert.DeserializeObject<Snowy_Root>(responseBody);
                return result;
            }
        }
    }
}
