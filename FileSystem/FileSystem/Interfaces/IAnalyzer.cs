using FileSystem.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.FileSystem.Interfaces
{
    internal interface IAnalyzer
    {
        public FileNode GetNode(uint clusterNum);
        public FileNode GetRootNode();
        public void Export(uint clusterNum);
        //public void Delete();
        //public void Read();
        //public void Restore();
        //public void Update();
    }
}
