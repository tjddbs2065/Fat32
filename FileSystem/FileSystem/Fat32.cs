using FileSystem.FileSystem.Interfaces;
using FileSystem.Structure;
using FileSystem.Structure.FAT32.Analyzer;
using FileSystem.Structure.FAT32.Areas;
using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem
{
    internal class Fat32 : IFileSystem
    {
        private DataStream ds;
        private uint bootRecordOffset;
        private BootRecord br;

        private FatContext fc;

        private FatAnalyzer da;

        public Fat32(DataStream ds, uint bootRecordOffset)
        {
            this.ds = ds;
            this.bootRecordOffset = bootRecordOffset;
        }

        public FatAnalyzer BuildFileSystem()
        {
            byte[] firstSector = ds.GetBytes(bootRecordOffset, Util.SECTOR);
            br = new BootRecord(firstSector);
            fc = new FatContext(br, ds, bootRecordOffset);

            da = new FatAnalyzer(fc);
            return da;
        }
    }
}
