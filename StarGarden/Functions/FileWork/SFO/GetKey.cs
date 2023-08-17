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
        public object GetData(FileStream fs, SFO_IndexEntry indexEntry, SFO_Header header)
        {
            byte[] buffer = new byte[indexEntry.paramLen];
            fs.Seek(header.dataTableOffset + indexEntry.dataOffset, SeekOrigin.Begin);
            fs.Read(buffer, 0, (int)indexEntry.paramLen);

            switch (isKeyUint(indexEntry))
            {
                case true:
                    var uintResult = BitConverter.ToUInt32(buffer, 0);
                    return uintResult;
                case false:
                    var stringResult = $"{Encoding.UTF8.GetString(buffer)}";
                    return stringResult;
            }

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
        public object SpecificData(string keyName, FileStream fs,List<SFO_IndexEntry> indexEntries,SFO_Header header)
        {
            for (int i = 0; i < indexEntries.Count; i++)
            {
                List<byte> bufferList = new List<byte>();
                fs.Seek(header.keyTableOffset + indexEntries[i].keyOffset, SeekOrigin.Begin);
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

                if (result == keyName)
                {

                   return GetData(fs, indexEntries[i], header);

                }
            }
            return null;
        }
        public object GetSpecificKeyData(string sfoPath, string KeyName)
        {
            using (FileStream fs = File.OpenRead(sfoPath))
            {
                SFO_Header header = new SFO_Header();
                List<SFO_IndexEntry> sfo_IndexEntries = new List<SFO_IndexEntry>();

                Populate populate = new Populate();
                BigEndian bigEndian = new BigEndian();
                GetKey getKey = new GetKey();

                populate.sfo_header(ref header, fs);
                populate.sfo_indexEntries(ref sfo_IndexEntries, ref header, fs);

                bigEndian.header(ref header);

                var data = getKey.SpecificData(KeyName, fs, sfo_IndexEntries, header);

                return data;
            }
        }
    }
}
