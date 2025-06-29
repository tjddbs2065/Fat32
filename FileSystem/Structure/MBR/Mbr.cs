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

        public static int SIGNATURE = 0xAA55;
        private const int partitionSize = 16;

        private byte[] data;
        private List<Partition> partitionTableList;

        public Mbr(byte[] data)
        {
            this.data = data;
            partitionTableList = new List<Partition>();

            // 파티션 테이블을 읽어온다.
            byte[] partitionTable = Util.CropBytes(data, bootCodeSize, partitionTableSize);
            SetPartitions(partitionTableList, partitionTable);

            Log.Information("MBR 분석 완료");
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

        public Partition SelectPartition()
        {
            Console.WriteLine("\n==========파티션 리스트========== ");
            for (int i = 0; i < partitionTableList.Count; i++)
            {
                Console.WriteLine(" - 파티션 #" + i + " : " + partitionTableList[i].partitionType);
            }
            Console.WriteLine("\n분석할 파티션을 선택해 주세요: ");
            string readValue = Console.ReadLine() ?? "";
            int partNum = int.Parse(readValue);

            return partitionTableList[partNum];
        }
    }
}
