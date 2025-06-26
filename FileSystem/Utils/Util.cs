using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Utils
{
    internal class Util
    {
        public static int ByteToInt(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
