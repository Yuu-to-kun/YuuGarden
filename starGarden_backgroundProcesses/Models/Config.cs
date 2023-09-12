using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace starGarden_backgroundProcesses.Models
{
    public class Config
    {
        public List<int> proccessIds;
    }

    public class ConfigFuncs
    {
        public void Create(List<int> processIds,string location)
        {
            Config config = new Config();
            config.proccessIds = processIds;

            var conf = JsonConvert.SerializeObject(config, Formatting.Indented);

            File.WriteAllText(Path.Combine(location, "config.json"), conf);
        }
        public Config OpenBGP_Config(string location)
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine(location, "config.json")));
            return config;
        }
        public void SaveBGP(Config config, string location)
        {
            var conf = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Path.Combine(location, "config.json"), conf);
        }

    }
}
