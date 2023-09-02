using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DiscordRPC;
using StarGarden.Functions;
using Windows.Media.Protection.PlayReady;

namespace StarGarden.Models
{
    public static class GlobalObjects
    {
        public static DiscordRpcClient DiscordRpcClient { get; set; } = new DiscordRpcClient("1147311963379073084");
        public static System.Timers.Timer Timer { get; set; } = new System.Timers.Timer(1000);
        public static DateTime currentTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 0, 0);
        //public static DateTime startTime { get; set; } = DateTime.UtcNow;
        public static RichPresence RichPresence { get; set; } = new RichPresence 
        {
            State = "00:00:00 elapsed",
            Assets = new Assets()
            {
                LargeImageKey = "image_large",
                LargeImageText = ":3",
                SmallImageKey = "image_small"
            },
            Details = "Idling",
            Timestamps = new Timestamps()
            {
                //Start = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - startTime),
            }
            
        };


        public static void UpdateTimer(object sender, System.Timers.ElapsedEventArgs e)
        {

            currentTime = currentTime.AddSeconds(1);
            RichPresence.State = $"{currentTime.ToString("HH:mm:ss")} elapsed";
            DiscordRpcClient.SetPresence(RichPresence);
        }
    }
}
