using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameServer.Source.Utility;
using GameServer.Source.Model;

namespace GameServer.Source
{
    public class NetConnection
    {
        protected TcpListener? tcpListener;
        private bool disposedValue;

        public async void StartListener(IPAddress address, int port)
        {
            try
            {
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();

                while (true)
                {
                    var client = await tcpListener.AcceptTcpClientAsync();
                    ThreadPool.QueueUserWorkItem(ListenerThread, client);
                }
               // return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to listen because of error " + ex.ToString());
            }
        }

        private static void ListenerThread(object? obj)
        {
            var client = obj as TcpClient;
            if (client == null)
            {
                Logger.Log("ERR! Failed to get client"); // <- this should not be possible
                return;
            }

            var stream = client.GetStream();
            
            while (stream.CanRead)
            {
                var headers = new PacketHeaders(stream);
            }
        }
    }
}
