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
    internal class DataStore
    {
        string path;
        DataStream ds;

        public DataStore(string path)
        {
            this.path = path;
            ds = new DataStream(path);
        }

        public void BuildFileSystem()
        {
            if (isMbr())
            {
                Mbr mbr = new Mbr(ds);
                Log.Information("MBR 분석 완료");

                List<Partition> partitionTable = mbr.GetPartitions();

                Console.WriteLine("\n분석할 파티션을 선택해 주세요.");
                for(int i = 0; i < partitionTable.Count; i++)
                {
                    Console.WriteLine(" - 파티션 #" + i + " : " + partitionTable[i].partitionType);
                }
                int partNum = int.Parse(Console.ReadLine());

                Partition partition = partitionTable[partNum];
            }
        }

        private bool isMbr()
        {
            byte[] signatureBytes = ds.GetBytes(510, 2);

            if (Util.ByteToUInt(signatureBytes) == 0xAA55)
            {
                Log.Information("MBR 시그니처 확인");
                return true;
            }
            return false;
        }
    }
}
