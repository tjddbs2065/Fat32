using FileSystem.FileSystem;
using FileSystem.Utils;
using Serilog;

namespace FileSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("프로그램이 시작되었습니다.");

            string filePath = @"C:\\Users\\tjddb\\Downloads\\Fat32 Image.vhd";

            FSController dataStore = new FSController(filePath);
            var fileSystem = dataStore.BuildFileSystem();

            // 최초 루트 노드 출력
            var node = fileSystem.GetRootNode();
            node.ShowInfo();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "q") break;
                if (input == null) continue;

                //fileSystem.Export(node.GetClusterNumber(input));

                node = fileSystem.GetNode(node.GetClusterNumber(input));
                node.ShowInfo();
            }

            Log.CloseAndFlush();
        }
    }
}
