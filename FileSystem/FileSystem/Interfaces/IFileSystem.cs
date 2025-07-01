using FileSystem.Structure;
using FileSystem.Structure.FAT32.Analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem.Interfaces
{
    internal interface IFileSystem
    {
        public FatAnalyzer BuildFileSystem();
    }
}
