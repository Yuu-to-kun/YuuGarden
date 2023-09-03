using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StarGarden.Functions
{
    
    public static class SG_Console
    {
        public static event Action<string,SolidColorBrush> OnOutputReceived;

        public static void WriteLine(string output)
        {
            SolidColorBrush defaultColor = Brushes.Black;
            WriteLine(output, defaultColor);
        }
        public static void WriteLine(string output, SolidColorBrush color)
        {
            OnOutputReceived?.Invoke(output,color);
        }
    }
}
