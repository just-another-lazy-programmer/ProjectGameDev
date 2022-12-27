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
            ReadTileMap(ReadFile(Path.Combine(tilemapsDirectory, tilemap)));
        }

        private string ReadFile(string path)
        {
            // @TODO: error handling?
            return File.ReadAllText(path);
        }

        private void ReadTileMap(string tilemap)
        {
            var json = JObject.Parse(tilemap);

            var height = (int)json["height"];
            var width = (int)json["width"];

            var tileheight = (int)json["tileheight"];
            var tilewidth = (int)json["tilewidth"];

            var layers = (JArray)json["layers"];
            var tilesets = (JArray)json["tilesets"];


        }
    }
}
