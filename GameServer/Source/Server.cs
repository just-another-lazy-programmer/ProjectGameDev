using GameServer.Source.Utility;
using System.Net;

namespace GameServer.Source
{
    public class Server
    {
        public NetConnection NetConnection { get; }

        public Server(ILogger logger, int port=6571)
        {
            Logger.SetLogger(logger);

            NetConnection = new NetConnection();
            NetConnection.StartListener(IPAddress.Any, port);
        }
    }
}