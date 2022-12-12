using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine.Multiplayer
{
    // The server is a dedicated listen server that mostly acts as a relay since we don't want to do any collision
    // checking or rendering server side, this comes with a nice advantage that the server is cross platform and can be hosted
    // independedly from the game client itself

    // This also means that when a client is hosting a server that client acts like a normal client with an additional server component
    // NOTE: This server does not take into account anti-cheating systems or latency-compensation as that goes way out of scope

    // The server makes use of a TCP connection so that only 1 port is required, typically I'd use UDP as it's more appropriate for
    // gaming (less overhead) however I really don't feel like implementing a TCP-like protocol ontop of UDP right now as it
    // can be quite time consuming

    internal class ServerConnection : IDisposable
    {
        protected TcpClient tcpClient;

        public bool EstablishConnection(string address, ushort port)
        {
            try
            {
                tcpClient = new TcpClient(address, port);
                return tcpClient.Connected;
            }
            catch (Exception ex)
            {
                // @todo: exception handling
                return false;
            }
        }

        public void Dispose()
        {
            // Close just calls Dispose (IDisposable) so a second call isn't required
            tcpClient.Close();
        }

    }
}
