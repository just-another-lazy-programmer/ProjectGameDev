using Serilog.Core;
using System;

using var game = new ProjectGameDev.Game1();

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
            logger.Error("An unhandled exception occured", ex);
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
