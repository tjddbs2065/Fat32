using FileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Structure.FAT32.Analyzer
{
    internal class DataAnalyzer
    {
        private FatArea fatEntry;
        private DataArea dataEntry;
        private FatContext fatContext;

        public DataAnalyzer(FatContext fatContext)
        {
            this.fatContext = fatContext;

            fatEntry = new FatArea(fatContext);
            dataEntry = new DataArea(fatContext);
        }
        public FileNode GetRootNode()
        {
            return GetNode(fatContext.RootDirCluster);
        }
        public FileNode GetNode(uint clusterNum)
        {
            // FatArea에서 클러스터 체인 정보 획득
            Queue<uint> clusterChain = fatEntry.GetClusterChain(clusterNum);
            // 클러스터 체인 정보 기반으로 byte 데이터 가져오기
            byte[] dataBytes = dataEntry.GetDataBlock(clusterChain);

            // 현재 노드의 디렉토리 엔트리 정보 보관
            List<DirEntry> dirList = new List<DirEntry>();

            int i = 0;
            Stack<byte[]> dirStack = new Stack<byte[]>();
            while (true)
            {
                // 디렉토리 엔트리 크기인 32byte로 데이터를 자른다.
                byte[] tmp = Util.CropBytes(dataBytes, i++ * DirEntry.Size, DirEntry.Size);
                
                // 속성 값이 0x00이명 이후에는 값이 없는것으로 간주하고 반복 중단
                if (tmp[11] == 0x00) break;

                // 속성 값이 0x0F이면 lfn, 그 외에는 sfn
                else if (tmp[11] == 0x0F)
                {
                    dirStack.Push(tmp);
                    continue;
                }

                dirStack.Push(tmp);

                // 디렉토리 엔트리를 객체로 생성하여 리스트에 추가
                dirList.Add(new DirEntry(new Stack<byte[]>(dirStack.Reverse())));
                dirStack.Clear();
            }

            return new FileNode(dirList);
        }
    }
}
