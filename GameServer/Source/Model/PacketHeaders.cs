using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Source.Model
{
    public struct PacketHeaders
    {
        public PacketType Type { get; set; }
        public int Length { get; set; }

        public PacketHeaders(Stream stream)
        {
            Type = (PacketType)stream.ReadByte();
            var buffer = new byte[sizeof(int)];
            stream.Read(buffer);
            Length = BitConverter.ToInt32(buffer);
        }
    }

    public enum PacketType : byte
    {
        Auth,
        LoadLevel,
        SpawnObject,
        UpdateObjectProperties,
        CloseConnection
    }
}
