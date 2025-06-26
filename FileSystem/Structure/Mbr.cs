using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure
{
    internal class Mbr // MBR Structure: 512byte
    {
        private int bootCode = 446;
        private int partitionTable = 64; // #1 ~ 4 partition table
        private int signature = 2;

        private DataStream ds;
        public Mbr(DataStream ds)
        {
            this.ds = ds;

            int signatureOffset = bootCode + partitionTable;
            
            if()
            {

            }
        }

        private bool isMbr()
        {
            byte[] signatureBytes = ds.GetBytes(signatureOffset, 2);

            if (Util.ByteToInt(signatureBytes) == 0x55AA)
            {
                return true;
            }
            return false;
        }
    }
}
