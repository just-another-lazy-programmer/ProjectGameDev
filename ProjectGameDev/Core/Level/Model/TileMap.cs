using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Level.Model
{
    internal class TileMap
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public int TileHeight { get; set; }
        public int TileWidth { get; set; }

        public List<Layer> Layers { get; set; }
        public List<TileSet> TileSets { get; set; }
    }
}
