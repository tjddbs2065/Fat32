using FileSystem.Structure;
using FileSystem.Structure.FAT32.Analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem
{
    internal interface IFileSystem
    {
        public DataAnalyzer BuildFileSystem();
    }
}
