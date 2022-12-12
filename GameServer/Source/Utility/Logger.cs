using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Source.Utility
{
    internal static class Logger
    {
        private static ILogger? outputDevice;

        public static void SetLogger(ILogger logger)
        {
            outputDevice = logger;
        }

        private static void LogInternal(object message)
        {
            if (outputDevice != null)
                outputDevice.WriteLine(message);
        }

        public static void Log(object message)
        {
            LogInternal(message);
        }
    }
}
