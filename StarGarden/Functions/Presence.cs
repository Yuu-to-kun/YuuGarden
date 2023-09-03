using DiscordRPC;
using StarGarden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    public class Presence
    {
        public void Set(string State,string largeImageKey = "image_large",string largeKeyText = ":3")
        {
            var presenceclient = GlobalObjects.DiscordRpcClient;

            presenceclient.SetPresence(new RichPresence()
            {
                State = State,
                Assets = new Assets()
                {
                    LargeImageKey = largeImageKey,
                    LargeImageText = largeKeyText
                },
                Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow,
                }
            });

        }
    }
}
