using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32.Areas
{
    internal class DirEntry
    {
        public static int Size = 32;

        public StringBuilder Name { get; private set; }
        public string Extension { get; private set; }
        public uint Attribute { get; private set; }
        private string CreateTime;
        private string CreateDate;
        private string LastAccessDate;
        private string LastModifiedDate;
        public uint FileSize { get; private set; }
        public uint ClusterNum { get; private set; }
        public bool isDeleted { get; private set; }


        public DirEntry(Stack<byte[]> stack)
        {
            Name = new StringBuilder();
            Extension = "";
            Attribute = 0;
            ClusterNum = 0;
            FileSize = 0;
            CreateTime = "";
            CreateDate = "";
            LastAccessDate = "";
            LastModifiedDate = "";

            InitDir(stack);
        }

        private void InitDir(Stack<byte[]> stack)
        {
            if (stack.Count <= 1)
            {
                byte[] data = stack.Pop();
                Name.Append(Util.ByteToASCII(Util.CropBytes(data, 0, 8)));
                SetProperties(data);
            }
            else
            {
                int cnt = stack.Count;
                for (int i = 0; i < cnt; i++)
                {
                    byte[] data = stack.Pop();
                    if (data[11] == 0x0F)
                    {
                        Name.Append(Util.ByteToUnicode(Util.CropBytes(data, 1, 10)));
                        Name.Append(Util.ByteToUnicode(Util.CropBytes(data, 14, 12)));
                        Name.Append(Util.ByteToUnicode(Util.CropBytes(data, 28, 4)));
                    }
                    else
                    {
                        SetProperties(data);
                    }
                }
            }
        }

        private void SetProperties(byte[] data)
        {
            isDeleted = data[0] == 0xE5 ? true : false;
            Extension = Util.ByteToASCII(Util.CropBytes(data, 8, 3));
            Attribute = Util.ByteToUInt(Util.CropBytes(data, 11, 1));

            byte[] attr = new byte[4];
            Buffer.BlockCopy(Util.CropBytes(data, 26, 2), 0, attr, 0, 2);
            Buffer.BlockCopy(Util.CropBytes(data, 20, 2), 0, attr, 2, 2);
            ClusterNum = Util.ByteToUInt(attr);

            FileSize = Util.ByteToUInt(Util.CropBytes(data, 28, 4));
        }
    }
}
