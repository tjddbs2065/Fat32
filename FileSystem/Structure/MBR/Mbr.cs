using FileSystem.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.MBR
{
    internal class Mbr // MBR Structure: 512byte
    {
        private int bootCodeSize = 446;
        private int partitionTableSize = 64; // #1 ~ 4 partition table
        private int signatureSize = 2;

        private const int partitionSize = 16;

        private DataStream ds;
        private List<Partition> partitionTableList;

        public Mbr(DataStream ds)
        {
            this.ds = ds;
            partitionTableList = new List<Partition>();


            // 파티션 테이블을 읽어온다.
            byte[] partitionTable = ds.GetBytes(bootCodeSize, partitionTableSize);
            SetPartitions(partitionTableList, partitionTable);
        }

        public List<Partition> GetPartitions()
        {
            return partitionTableList;
        }

        private void SetPartitions(List<Partition> partitionList, byte[] data)
        {
            for (int i = 0; i < 4; i++)
            {
                if (data[(i * partitionSize) + 4] != 0)
                {
                    Log.Information("================== 파티션 #" + (partitionList.Count) + " ==================");
                    partitionList.Add(
                        new Partition(Util.CropBytes(data, i * partitionSize, partitionSize))
                    );
                }
            }
            Log.Information("MBR 파티션 총 개수: " + partitionList.Count + "개");
        }
    }
}
