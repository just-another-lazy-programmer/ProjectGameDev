using GameServer.Source.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine.Multiplayer
{
    internal class MultiplayerLogger : ILogger
    {
        public void Write(object message)
        {
            Debug.Write(message);
        }

        public void WriteLine(object message)
        {
            Debug.WriteLine(message);
        }
    }
}
