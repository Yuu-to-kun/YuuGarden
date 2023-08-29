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
        private string elfLoc;
        private bool isChecked;


        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageSource { get => imageSource; set => imageSource = value; }

        public string GamePath { get => gamePath; set { gamePath = value; elfLoc = Path.Combine(value, "eboot.bin"); } }
        public string ElfLoc { get => elfLoc; set => elfLoc = value; }
    }
}
