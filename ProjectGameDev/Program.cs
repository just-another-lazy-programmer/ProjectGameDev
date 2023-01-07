#undef HANDLE_EXCEPTIONS_IN_DEBUG // <- don't catch exceptions in debug since visual studio already handles them better
                                  // can change #undef to #define if for some bizarre reason this is ever needed

using Microsoft.Xna.Framework;
using ProjectGameDev;
using Serilog;
using Serilog.Core;
using System;
using System.IO;

using var game = new Game1();

string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string appRoot = Path.Combine(appData, Game1.GameName);

var newLogger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .WriteTo.File(
        Path.Combine(appRoot, "Logs/log-.log"),
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp} [{Level}] {Message}{NewLine}{Exception}"
    )
    .CreateLogger();

newLogger.Debug("Started!");
game.SetLogger(newLogger);

#if ( DEBUG && !HANDLE_EXCEPTIONS_IN_DEBUG )
game.Run();
#else
try
{
    game.Run();
} 
catch (Exception ex)
{
#if WINDOWS
    System.Windows.Forms.MessageBox.Show($"An unhandled exception occurred \n\n{ex}");
#endif

    try
    {
        var logger = game.GetLogger();
        if (logger != null)
        {
            logger.Error(ex, "An unhandled exception occured");
        }
    }
    catch (Exception loggerEx)
    {
#if WINDOWS
        System.Windows.Forms.MessageBox.Show($"While attempting to log the unhandled exception another exception occured " +
            $"preventing the logger from being accessed\n\n{loggerEx}");
#endif
    }
}
#endif