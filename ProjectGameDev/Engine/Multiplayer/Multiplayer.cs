using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameServer;

namespace ProjectGameDev.Engine.Multiplayer
{
    internal class MultiplayerManager
    {
        public ServerConnection ActiveConnection { get; protected set; }

        public MultiplayerManager()
        {
            
        }

        public async Task EstablishConnection(string server, ushort port)
        {

        }
    }
}
