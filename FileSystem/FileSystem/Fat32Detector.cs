using FileSystem.FileSystem.Interfaces;
using FileSystem.Structure.MBR;
using FileSystem.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem
{
    internal class Fat32Detector : ISystemDetector
    {
        public IFileSystem ISystemDetector(DataStream ds)
        {
            throw new NotImplementedException();
        }
        public bool IsMatch(byte[] data)
        {
            byte[] signatureBytes = Util.CropBytes(data, 510, 2);

            if (Util.ByteToUInt(signatureBytes) != Mbr.SIGNATURE)
                return false;

            string fsType = Util.ByteToASCII(data, 82, 8).Trim();
            if (fsType == "FAT32")
            {
                Log.Information("FAT32 파일 시스템 감지됨");
                return true;
            }
            return false;
        }

        public IFileSystem Create(DataStream ds, uint offset)
        {
            return new Fat32(ds, offset);
        }
    }
}
