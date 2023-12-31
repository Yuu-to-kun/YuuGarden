﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StarGarden.Models.Launcher
{
    public class GameEntry
    {
        private string name;
        private string cusa;
        private string imageSource;
        private string gamePath;
        private string sfoPath;
        private string elfLoc;
        private BitmapImage pic1;
        private string gameStatus;
        private SolidColorBrush statusColor;


        public string Name { get => name; set => name = value; }
        public string Cusa { get => cusa; set => cusa = value; }
        public string ImageSource { get => imageSource; set => imageSource = value; }

        public string GamePath { get => gamePath; set { gamePath = value; elfLoc = Path.Combine(value, "eboot.bin"); sfoPath = Path.Combine(value, "sce_sys","param.sfo"); } }
        public string ElfLoc { get => elfLoc; set => elfLoc = value; }
        public string SfoPath { get => sfoPath; set => sfoPath = value; }
        public BitmapImage Pic1 { get => pic1; set => pic1 = value; }
        public string GameStatus { get => gameStatus; set => gameStatus = value; }
        public SolidColorBrush StatusColor { get => statusColor; set => statusColor = value; }
    }
}
