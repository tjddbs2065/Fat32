using FileSystem.FileSystem.Interfaces;
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
    internal class FSController
    {
        string path;
        private DataStream ds;
        private List<ISystemDetector> detectors;

        public FSController(string path)
        {
            this.path = path;
            ds = new DataStream(path);

            detectors = new List<ISystemDetector>
            {
                new Fat32Detector(),
            };
        }

        public IAnalyzer BuildFileSystem()
        {
            uint offset = InitBootOffset();

            byte[] firstSector = ds.GetBytes(offset, Util.SECTOR); // 첫 섹터 읽어오기

            foreach (ISystemDetector detector in detectors)
            {
                if (detector.IsMatch(firstSector))
                {
                    return detector.Create(ds, offset).BuildFileSystem();
                }
            }
            throw new Exception("알 수 없는 파일시스템 입니다.");
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

    }
}
