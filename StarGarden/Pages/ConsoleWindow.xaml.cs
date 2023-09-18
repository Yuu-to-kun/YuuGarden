using StarGarden.Functions;
using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
            SolidColorBrush defaultColor = Brushes.Black;
            WriteLine(output, defaultColor);
            
        }
        public void WriteLine(string output,SolidColorBrush color)
        {
            TextRange tr = new TextRange(textBox.Document.ContentEnd, textBox.Document.ContentEnd);
            tr.Text = $"{output}\r";
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, color);

            

            //textBox.AppendText(output + Environment.NewLine);
            textBox.ScrollToEnd();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (GlobalObjects.shutDown == false)
            {
                e.Cancel = true;
                this.Hide();
            }
            
        }
    }
}
