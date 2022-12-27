using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using ProjectGameDev.Core.Level.Model;

namespace ProjectGameDev.Core.Level
{
    internal class LevelLoader
    {
        private readonly ContentManager contentManager;
        private const string tilemapsDirectory = "./Levels/Tilemaps";

        public LevelLoader(DependencyManager dependencyManager)
        {
            dependencyManager.InjectChecked(ref contentManager);
        }

        public void LoadTileMap(string tileset, string tilemap)
        {
            // @TODO: is this cross-platform?
            var map = ReadTileMap(ReadFile(Path.Combine(tilemapsDirectory, tilemap)));
        }

        private string ReadFile(string path)
        {
            // @TODO: error handling?
            return File.ReadAllText(path);
        }

        private TileMap ReadTileMap(string tilemap)
        {
            var json = JObject.Parse(tilemap);

            // This looks horrible but it's just mapping the json to the C# classes
            // So it's fine

            return new TileMap
            {
                Height = (int)json["height"],
                Width = (int)json["width"],

                TileHeight = (int)json["tileheight"],
                TileWidth = (int)json["tilewidth"],

                Layers = ((JArray)json["layers"]).Select(l => new Layer
                {
                    Data = ((JArray)l["data"]).Select(d => (int)d).ToList(),
                    Height = (int)l["height"],
                    Width = (int)l["width"]
                }).ToList(),
                TileSets = ((JArray)json["tilesets"]).Select(t => new TileSet
                {
                    FirstGID = (int)t["firstgid"],
                    Source = (string)t["source"]
                }).ToList()
            };
        }
    }
}
