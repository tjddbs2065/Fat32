using FileSystem.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.MBR
{
    internal class Partition
    {
        private int bootFlagSize = 1;
        private int startingCHSAddrSize = 3;
        private int partTypeSize = 1;
        private int endingCHSAddrSize = 3;
        private int startingLBAAddrSize = 4;
        private int sizeInSectorSize = 4;

        public string partitionType { get; private set; }
        public uint bootRecordOffset { get; private set; }
        public uint sizeInSector { get; private set; }

        // 16바이트의 파티션 데이터를 분석한다.
        public Partition(byte[] data)
        {
            partitionType = "";
            AnalyzeProperties(data);
        }

        private void AnalyzeProperties(byte[] data)
        {
            SetPartitionType(data);
            SetBootRecordOffset(data);
            SetSizeInSector(data);
        }

        private void SetPartitionType(byte[] data)
        {
            int partTypeOffset = 4;
            uint type = Util.ByteToUInt(Util.CropBytes(data, partTypeOffset, partTypeSize));

            switch (type)
            {
                case 0x0C:
                    partitionType = "FAT32";
                    break;
                case 0x07:
                    partitionType = "NTFS";
                    break;
                default:
                    partitionType = "Unknown FS";
                    break;
            }
            Log.Information("파티션 종류: " + partitionType);
        }

        private void SetBootRecordOffset(byte[] data)
        {
            int lbaAddrOffset = 8;
            bootRecordOffset = Util.ByteToUInt(Util.CropBytes(data, lbaAddrOffset, startingLBAAddrSize));

            Log.Information("BootRecord 주소: " + bootRecordOffset + " Sectors");
        }

        private void SetSizeInSector(byte[] data)
        {
            int sizeInSectorOffset = 12;
            sizeInSector = Util.ByteToUInt(Util.CropBytes(data, sizeInSectorOffset, sizeInSectorSize));

            Log.Information("파티션 크기: " + sizeInSector + " Sectors");
        }
    }
}
