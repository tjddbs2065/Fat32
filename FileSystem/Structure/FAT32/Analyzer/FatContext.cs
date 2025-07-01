using FileSystem.Structure.FAT32.Areas;
using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32.Analyzer
{
    internal class FatContext
    {
        public DataStream ds { get; set; }               // FileStream or MemoryStream
        public uint SectorSize { get; set; }             // 일반적 FAT는 512
        public uint ClusterSize { get; set; }            // 섹터 * sectorsPerCluster
        public uint FatOffset { get; set; }              // FAT 시작 위치
        public uint DataOffset { get; set; }             // 데이터 시작 위치
        public uint RootDirCluster { get; set; }         // 루트 디렉터리 클러스터 번호

        private uint FatAreaSize;

        public FatContext(BootRecord br, DataStream ds, uint bootRecordOffset)
        {
            this.ds = ds;
            ClusterSize = br.bytesPerSector * br.sectorPerCluster;
            FatOffset = br.reservedSectorCount * br.bytesPerSector + bootRecordOffset;
            FatAreaSize = br.numOfFAT * br.FATSize32 * br.bytesPerSector;
            DataOffset = FatOffset + FatAreaSize;
            RootDirCluster = br.rootDirectoryCluster;
        }
    }
}
