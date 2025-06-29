using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32
{
    internal class BootRecord
    {
        private int bytesPerSectorSize = 2;
        private int sectorPerClusterSize = 1;
        private int reservedSectorCountSize = 2;
        private int numOfFATSize = 1;
        private int FATSize32Size = 4;
        private int rootDirectoryClusterSize = 4;

        public uint bytesPerSector { get; private set; } // 섹터의 바이트 크기
        public uint sectorPerCluster { get; private set; } // 클러스터의 섹터 크기
        public uint reservedSectorCount { get; private set; } // 예약 공간의 섹터 크기
        public uint numOfFAT { get; private set; } // FAT의 개수(보통 자료 손상을 대비해 2개의 동일한 FAT(백업)을 가진다.)
        public uint FATSize32 { get; private set; } // FAT의 섹터 수
        public uint rootDirectoryCluster { get; private set; } // 루트 디렉터리의 클러스터 번호

        private byte[] data;

        public BootRecord(byte[] data)
        {
            this.data = data;

            Analyze();
        }
        private void Analyze()
        {
            bytesPerSector = Util.ByteToUInt(Util.CropBytes(data, 11, bytesPerSectorSize));
            sectorPerCluster = Util.ByteToUInt(Util.CropBytes(data, 13, sectorPerClusterSize));
            reservedSectorCount = Util.ByteToUInt(Util.CropBytes(data, 14, reservedSectorCountSize));
            numOfFAT = Util.ByteToUInt(Util.CropBytes(data, 16, numOfFATSize));
            FATSize32 = Util.ByteToUInt(Util.CropBytes(data, 36, FATSize32Size));
            rootDirectoryCluster = Util.ByteToUInt(Util.CropBytes(data, 44, rootDirectoryClusterSize));
        }
    }
}
