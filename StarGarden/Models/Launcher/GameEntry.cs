using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Models.Launcher
{
    public class GameEntry
    {
        private string name;
        private string description;
        private string imageSource;
        private string gamePath;
        private string sfoPath;
        private string elfLoc;


        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageSource { get => imageSource; set => imageSource = value; }

        public string GamePath { get => gamePath; set { gamePath = value; elfLoc = Path.Combine(value, "eboot.bin"); sfoPath = Path.Combine(value, "sce_sys","param.sfo"); } }
        public string ElfLoc { get => elfLoc; set => elfLoc = value; }
        public string SfoPath { get => sfoPath; set => sfoPath = value; }
    }
}
