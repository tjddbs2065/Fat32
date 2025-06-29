using FileSystem.Structure.FAT32.Analyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure
{
    internal class FileNode
    {
        private List<FileNode> children;

        private List<DirEntry> dirList;
        public FileNode(List<DirEntry> dirList)
        {
            this.dirList = dirList;
        }
        public void ShowInfo()
        {
            foreach (var entry in dirList)
            {
                Console.Write(entry.Name+".");
                Console.Write(entry.Extension +" : ");
                Console.Write(entry.FileSize+"bytes\n");
            }
        }
        public void Export(string path) { }
    }
}
