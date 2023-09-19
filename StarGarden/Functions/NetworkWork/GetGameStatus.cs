using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static StarGarden.Models.fpPS4_Artifact_Json;

namespace StarGarden.Functions.NetworkWork
{
    public class Cusacode
    {
        public string id { get; set; }
        public string tag { get; set; }
    }

    public class Snowy_Root
    {
        public List<Cusacode> cusacode { get; set; }
        public List<object> homebrew { get; set; }
    }
    public class GetGameStatus
    {
        public Snowy_Root parseNormalGame(string cusa)
        {
            using (var client = new HttpClient())
            {
                Uri endpoint = new Uri($"https://fpps4.net/scripts/api.php?token=3g4YNf7XvchD&cusa={cusa}");
                var responseMsg = client.GetAsync(endpoint).Result;
                string responseBody = responseMsg.Content.ReadAsStringAsync().Result;
                Snowy_Root result = JsonConvert.DeserializeObject<Snowy_Root>(responseBody);
                return result;
            }
        }
    }
}
