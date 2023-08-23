﻿using StarGarden.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StarGarden.Pages
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            SG_Console.OnOutputReceived += WriteLine;
            InitializeComponent();
        }

        public void WriteLine(string output)
        {
            textBox.AppendText(output + Environment.NewLine);
            textBox.ScrollToEnd();
        }

    }
}
