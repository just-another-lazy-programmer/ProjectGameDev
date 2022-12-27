using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal static class LevelUtils
    {
        public static Point GetLocationInSetFromGID(int gid, int firstgid, int tilesize, int tilesPerRow)
        {
            // Subtract the firstgid of the tileset to get the index of the tile within the set
            int index = gid - firstgid;

            // Calculate the row and column of the tile within the tileset
            int row = index / tilesPerRow;
            int col = index % tilesPerRow;

            // Multiply the row and column by the tile size to get the location of the tile in pixels
            int x = col * tilesize;
            int y = row * tilesize;

            return new Point(x, y);
        }
    }
}
