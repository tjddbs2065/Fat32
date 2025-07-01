using FileSystem.Structure.FAT32.Analyzer;
using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32.Areas
{
    internal class FatArea
    {
        private FatContext fc;
        public FatArea(FatContext fc)
        {
            this.fc = fc;
        }

        public Queue<uint> GetClusterChain(uint clusterNum)
        {
            Queue<uint> clusterChain = new Queue<uint>();
            clusterChain.Enqueue(clusterNum);

            uint clusterOffset = fc.FatOffset + clusterNum * 4;

            uint clusterVal = 0;
            while (true)
            {
                clusterVal = Util.ByteToUInt(fc.ds.GetBytes(clusterOffset, 4));
                if (clusterVal == 0x0FFFFFFF) break;

                clusterChain.Enqueue(clusterVal);
                clusterOffset += 4;
            }

            return clusterChain;
        }
    }
}
