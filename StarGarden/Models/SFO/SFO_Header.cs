using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.SFO
{
    public struct SFO_Header
    {
        public int magic;
        public int version;
        public int keyTableOffset;
        public int dataTableOffset;
        public int indexTableEntries;
    }
}
