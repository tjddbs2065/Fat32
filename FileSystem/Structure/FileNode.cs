using FileSystem.Structure.FAT32.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure
{
    internal class FileNode
    {
        private List<DirEntry> dirList;
        public FileNode(List<DirEntry> dirList)
        {
            this.dirList = dirList;
        }
        public void ShowInfo()
        {
            foreach (var entry in dirList)
            {
                Console.Write(entry.isDeleted ? "[removed!] " : "");
                Console.Write(entry.Name+".");
                Console.Write(entry.Extension +" : ");
                Console.Write(entry.FileSize+"bytes\n");
            }
        }
        public uint GetClusterNumber(string fileName)
        {
            foreach(var entry in dirList)
            {
                if (entry.Name.ToString().Equals(fileName))
                {
                    if (entry.ClusterNum == 0) return 2;
                    return entry.ClusterNum;
                }
            }
            return 2;
        }
    }
}
