using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace StarGarden.Functions
{
    public class GetStatusColor
    {
        public SolidColorBrush getColor(string status)
        {
            Color color = new Color();
            switch (status)
            {
                case "N/A":
                    color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
                    break;
                case "Boots":
                    color = (Color)ColorConverter.ConvertFromString("#F2766E");
                    break;
                case "Menus":
                    color = (Color)ColorConverter.ConvertFromString("#4288B7");
                    break;
                case "Ingame":
                    color = (Color)ColorConverter.ConvertFromString("#fabb44");
                    break;
                case "Playable":
                    color = (Color)ColorConverter.ConvertFromString("#54A396");
                    break;
            }
            return new SolidColorBrush(color);
        }
    }
}
