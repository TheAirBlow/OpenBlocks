using System;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions.Core;

namespace OpenBlocks.Main
{
    public static class Entrypoint
    {
        /// <summary>
        /// Global logger instance
        /// </summary>
        public static Logger Logger;

        /// <summary>
        /// Global game instance
        /// </summary>
        public static MainGame Game;
    
        /// <summary>
        /// Entrypoint method
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var config = new LoggerConfiguration()
                .Enrich.With(new ExceptionEnricher())
                .WriteTo.File("openblocks.log")
                .WriteTo.Console();
            #if DEBUG
            config.MinimumLevel.Debug();
            #endif
            Logger = config.CreateLogger();
            Logger.Information("Welcome to OpenBlocks!");
            Logger.Information($"OpenBlocks.Main: v{Assembly.GetExecutingAssembly().GetName().Version}");
            
            try {
                Logger.Information("Running main game...");
                Game = new MainGame();
                Game.Run();
                Game.Dispose();
            } catch (Exception e) {
                Logger.Fatal("Entrypoint unrecoverable exception caught: {0}", e);
                Environment.Exit(1); // Exiting with non-zero exit code
            }
        }
    }
}