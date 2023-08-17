using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGarden.SFO
{
    public struct SFO_IndexEntry
    {
       public ushort keyOffset;
       public ushort param_fmt;
       public uint paramLen;
       public uint paramMaxLenl;
       public uint dataOffset;
    }
}
