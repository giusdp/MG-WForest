using System;
using Serilog;

namespace WForest
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            
            using var game = new Game1();
            Log.Information("Game instance created.");
            game.Run();
        }
    }
}