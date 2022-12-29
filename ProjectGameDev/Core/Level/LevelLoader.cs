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
using ProjectGameDev.Utility;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjectGameDev.Core.Level
{
    internal class LevelLoader
    {
        private readonly ContentManager contentManager;
        private const string tilemapsDirectory = "./Levels/Tilemaps";
        private readonly static Dictionary<string, Texture2D> tilesetTextures = new();

        public LevelLoader(DependencyManager dependencyManager)
        {
            dependencyManager.InjectChecked(ref contentManager);
        }

        public void LoadTileMap(string tileset, string tilemap, Level level, float scaleFactor)
        {
            // @TODO: is this cross-platform?
            var map = ReadTileMap(ReadFile(Path.Combine(tilemapsDirectory, tilemap)));
            CreateObjectsForLevel(map, level, scaleFactor);
        }

        private Texture2D GetTexureForTileSet(TileSet tileset)
        {
            if (tilesetTextures.TryGetValue(tileset.Source, out Texture2D texture))
                return texture;

            var newTexture = contentManager.Load<Texture2D>(tileset.Source);
            tilesetTextures.Add(tileset.Source, newTexture);
            return newTexture;
        }

        private void CreateObjectsForLevel(TileMap tilemap, Level level, float scaleFactor)
        {
            foreach (var layer in tilemap.Layers)
            {
                for (int i = 0; i < layer.Data.Count; i++)
                {
                    var gid = layer.Data[i];
                    if (gid == 0) continue;

                    var tileset = FindTilesetForGid(tilemap, gid);

                    if (tileset == null)
                        throw new Exception($"Failed to find TileSet for a GID that wasn't 0 ({gid})");

                    var texture = GetTexureForTileSet(tileset);

                    var sourceLocation = TextureUtils.GetLocationInSetFromGID(
                        gid,
                        tileset.FirstGID,
                        tilemap.TileWidth,
                        texture.Width / tilemap.TileWidth
                    );

                    var destinationLocationX = (i % tilemap.Width)*(tilemap.TileWidth*scaleFactor);
                    var destinationLocationY = (i / tilemap.Width)*(tilemap.TileHeight*scaleFactor);

                    var size = new Point(
                        (int)(tilemap.TileWidth * scaleFactor), 
                        (int)(tilemap.TileHeight * scaleFactor)
                    );

                    var newTile = new Tile(
                        level.GetDependencyManager(),
                        new Vector2(destinationLocationX, destinationLocationY),
                        size,
                        texture,
                        new Rectangle(sourceLocation, new Point(tilemap.TileWidth, tilemap.TileHeight)) 
                    );

                    level.AddObject(newTile);
                }
            }
        }

        private TileSet FindTilesetForGid(TileMap tilemap, int gid)
        {
            TileSet found = null;

            foreach (var tileset in tilemap.TileSets)
            {
                if (found == null && tileset.FirstGID <= gid)
                {
                    found = tileset;
                }
                else if (found != null && tileset.FirstGID > found.FirstGID && tileset.FirstGID <= gid)
                {
                    found = tileset;
                }
            }

            return found;
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
