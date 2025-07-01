using FileSystem.Structure.FAT32.Analyzer;
using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32.Areas
{
    internal class DataArea
    {
        private FatContext fc;

        public DataArea(FatContext fc)
        {
            this.fc = fc;
        }

        public byte[] GetDataBlock(IEnumerable<uint> queue)
        {
            byte[] mergedData;
            using (var ms = new MemoryStream())
            {
                foreach (var clusterNum in queue)
                {
                    uint clusterOffset = fc.DataOffset + fc.ClusterSize * (clusterNum - 2);

                    ms.Write(fc.ds.GetBytes(clusterOffset, (int)fc.ClusterSize));
                }
                mergedData = ms.ToArray();
            }

            return mergedData;
        }
    }
}
