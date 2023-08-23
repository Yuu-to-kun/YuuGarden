using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.Functions
{
    
    public static class SG_Console
    {
        public static event Action<string> OnOutputReceived;

        public static void WriteLine(string output)
        {
            OnOutputReceived?.Invoke(output);
        }
    }
}
