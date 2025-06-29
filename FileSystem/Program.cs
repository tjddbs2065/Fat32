using FileSystem.FileSystem;
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

            DataStore dataStore = new DataStore(filePath);
            var fileSystem = dataStore.BuildFileSystem();
            var node = fileSystem.GetRootNode();
            node.ShowInfo();

            while (true)
            {


                var input = Console.ReadLine();
                if (input == "q") break;
            }

            Log.CloseAndFlush();
        }
    }
}
