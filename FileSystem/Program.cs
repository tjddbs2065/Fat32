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

            string filePath = @"C:\\Users\\tjddb\\Downloads\\fat32.vhd";

            DataStore dataStore = new DataStore(filePath);
            dataStore.BuildFileSystem();

            Log.CloseAndFlush();
        }
    }
}
