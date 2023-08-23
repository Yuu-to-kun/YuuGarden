﻿using System;
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
        public event PropertyChangedEventHandler gameSelected;


        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageSource { get => imageSource; set => imageSource = value; }
        public bool IsChecked 
        { 
            get => isChecked;
            set {
                if (isChecked != value)
                {
                    isChecked = value;
                    gameSelected?.Invoke(this, new PropertyChangedEventArgs(nameof(isChecked)));
                }
            }
        }

        public string GamePath { get => gamePath; set { gamePath = value; elfLoc = Path.Combine(value, "eboot.bin"); } }
        public string ElfLoc { get => elfLoc; set => elfLoc = value; }
    }
}
