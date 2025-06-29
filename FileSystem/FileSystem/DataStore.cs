using FileSystem.Structure.FAT32.Analyzer;
using FileSystem.Structure.MBR;
using FileSystem.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem
{
    internal class DataStore
    {
        string path;
        DataStream ds;

        public DataStore(string path)
        {
            this.path = path;
            ds = new DataStream(path);
        }

        public DataAnalyzer BuildFileSystem()
        {
            IFileSystem fs;
            uint offset = InitBootOffset();

            byte[] firstSector = ds.GetBytes(offset, 512); // 첫 섹터 읽어오기

            //if (isFat32(firstSector))
            //{
            //    fs = new Fat32(ds, offset);
            //}
            fs = new Fat32(ds, offset);
            return fs.BuildFileSystem();
        }

        private bool isMbr(byte[] data)
        {
            byte[] signatureBytes = Util.CropBytes(data, 510, 2);
            byte[] bootFlag = Util.CropBytes(data, 450, 1);

            if (Util.ByteToUInt(signatureBytes) != Mbr.SIGNATURE)
                return false;

            for (int i = 0; i < 4; i++)
            {
                byte type = data[446 + (i * 16) + 4];
                if(type != 0)
                {
                    Log.Information($"MBR 파티션 엔트리 발견(타입: {type:X2})");
                    return true;
                }
            }
            return false;
        }
        private uint InitBootOffset()
        {
            uint bootRecordOffset = 0;
            byte[] tmpSector = ds.GetBytes(0, 512); // 첫 섹터 읽어오기

            if (isMbr(tmpSector))
            {
                Mbr mbr = new Mbr(tmpSector);
                Partition partition = mbr.SelectPartition();

                bootRecordOffset = partition.bootRecordOffset;
            }

            return bootRecordOffset;
        }

        private bool isFat32(byte[] data)
        {
            byte[] signatureBytes = Util.CropBytes(data, 510, 2);

            if (Util.ByteToUInt(signatureBytes) != Mbr.SIGNATURE)
                return false;

            string fsType = Util.ByteToASCII(data, 82, 8).Trim();
            if(fsType == "FAT32")
            {
                Log.Information("FAT32 파일 시스템 감지됨");
                return true;
            }
            return false;
        }
    }
}
