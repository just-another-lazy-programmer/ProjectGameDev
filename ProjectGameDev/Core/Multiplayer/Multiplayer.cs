using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameServer;
using GameServer.Source;

namespace ProjectGameDev.Core.Multiplayer
{
    internal class MultiplayerManager
    {
        public ServerConnection ActiveConnection { get; protected set; }
        public const bool ShouldSelfHost = true;
        private bool isListening = false;
        private Server selfServer;

        public MultiplayerManager()
        {
            
        }

        public void EstablishConnection(string server, ushort port)
        {
            if (ActiveConnection != null)
            {
                // @todo: gracefully handle
                throw new Exception("Cannot establish connection when one is already established.");
            }

            if (ShouldSelfHost && !isListening)
            {
                selfServer = new Server(new MultiplayerLogger());
            }

            ActiveConnection = new ServerConnection();
            ActiveConnection.EstablishConnection(server, port);
        }
    }
}
