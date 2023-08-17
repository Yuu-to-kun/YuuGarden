using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using StarGarden.Models.SFO;


namespace StarGarden.Functions.FileWork.SFO
{
    public class GetKey
    {
        public uint UintData(FileStream fs, SFO_IndexEntry indexEntry, SFO_Header header)
        {
            byte[] buffer = new byte[indexEntry.paramLen];
            fs.Seek(header.dataTableOffset + indexEntry.dataOffset, SeekOrigin.Begin);
            fs.Read(buffer, 0, (int)indexEntry.paramLen);

            uint result = BitConverter.ToUInt32(buffer, 0);
            return result;
        }

        public string StringData(FileStream fs, SFO_IndexEntry indexEntry, SFO_Header header)
        {
            byte[] buffer = new byte[indexEntry.paramLen];
            fs.Seek(header.dataTableOffset + indexEntry.dataOffset, SeekOrigin.Begin);
            fs.Read(buffer, 0, (int)indexEntry.paramLen);

            string result = $"{Encoding.UTF8.GetString(buffer)}";
            return result;
        }

        public string KeyName(FileStream fs, SFO_IndexEntry indexEntry, SFO_Header header)
        {
            List<byte> bufferList = new List<byte>();
            fs.Seek(header.keyTableOffset + indexEntry.keyOffset, SeekOrigin.Begin);
            while (true)
            {
                int byteValue = fs.ReadByte();
                if (byteValue == -1 || byteValue == 0)
                {
                    break;
                }
                bufferList.Add((byte)byteValue);
            }

            string result = $"{Encoding.UTF8.GetString(bufferList.ToArray())}";
            return result;
        }
        public bool isKeyUint(SFO_IndexEntry indexEntry)
        {
            if ($"{indexEntry.param_fmt:X}" == "404")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
