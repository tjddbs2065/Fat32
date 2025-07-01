using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem.Interfaces
{
    internal interface ISystemDetector
    {
        public bool IsMatch(byte[] data);
        public IFileSystem Create(DataStream ds, uint offset);
    }
}
