using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarGarden.Models.SFO;

namespace StarGarden.Functions.FileWork.SFO
{
    public class BigEndian
    {
        public void entry(ref List<SFO_IndexEntry> indexEntries)
       {
            
            for (int i = 0; i < indexEntries.Count; i++)
            {
                SFO_IndexEntry entry = indexEntries[i];

                byte[] ientry_bytes = BitConverter.GetBytes(entry.param_fmt);
                Array.Reverse(ientry_bytes);
                entry.param_fmt = BitConverter.ToUInt16(ientry_bytes, 0);

                indexEntries[i] = entry;
            }
       }

        public void header(ref SFO_Header header)
        {
            byte[] version_bytes = BitConverter.GetBytes(header.version);
            Array.Reverse(version_bytes);
            header.version = BitConverter.ToInt32(version_bytes, 0);
        }
    }
}
