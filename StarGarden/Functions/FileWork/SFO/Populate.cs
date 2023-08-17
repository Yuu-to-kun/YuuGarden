using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using StarGarden.Models.SFO;
using System.IO;

namespace StarGarden.Functions.FileWork.SFO
{
    public class Populate
    {
        public void sfo_header(ref SFO_Header header, FileStream fs)
        {

            byte[] sfo_header_buffer = new byte[Marshal.SizeOf<SFO_Header>()];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(sfo_header_buffer, 0, Marshal.SizeOf<SFO_Header>());

            GCHandle handle = GCHandle.Alloc(sfo_header_buffer, GCHandleType.Pinned);

            try
            {
                IntPtr ptr = IntPtr.Add(handle.AddrOfPinnedObject(), 0);
                header = (SFO_Header)Marshal.PtrToStructure(ptr, typeof(SFO_Header));
            }
            finally
            {
                handle.Free();
            }
        }

        public void sfo_indexEntries(ref List<SFO_IndexEntry> indexEntries, ref SFO_Header header, FileStream fs)
        {
            byte[] entrybuffer = new byte[Marshal.SizeOf<SFO_IndexEntry>() * header.indexTableEntries];

            fs.Seek(0x14, SeekOrigin.Begin);
            fs.Read(entrybuffer, 0, Marshal.SizeOf<SFO_IndexEntry>() * header.indexTableEntries);

            GCHandle handle1 = GCHandle.Alloc(entrybuffer, GCHandleType.Pinned);
            try
            {
                for (int i = 0; i < header.indexTableEntries; i++)
                {
                    IntPtr ptr = IntPtr.Add(handle1.AddrOfPinnedObject(), (i * 0x10));
                    SFO_IndexEntry inentry = (SFO_IndexEntry)Marshal.PtrToStructure(ptr, typeof(SFO_IndexEntry));
                    indexEntries.Add(inentry);

                }
            }
            finally
            {
                handle1.Free();
            }
        }
    }
}
