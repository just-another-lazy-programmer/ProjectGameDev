﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core.Level;
using ProjectGameDev.Core.Multiplayer;
using ProjectGameDev.Levels.TestLevel;
using ProjectGameDev.Utility;

namespace ProjectGameDev.Core
{
    internal class Engine
    {
        private readonly DependencyManager dependencyManager;
        private readonly World world;

        public Engine(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
            dependencyManager.Inject(ref world);
        }

        public void LoadLevel()
        {
            var level = new TestLevel(dependencyManager);
            level.Load();

            world.LoadedLevel = level;
        }

        public void Tick(GameTime gameTime)
        {
            foreach (var worldObject in world.LoadedLevel.GetObjects())
            {
                worldObject.Update(gameTime);
            }

            foreach (var obj in world.LoadedLevel.GetObjects())
            {
                if (obj is IReplicate replicate)
                {
                   //replicate.NetworkComponent. 
                }
            }
        }

        public void ConnectMultiplayer(string host, ushort port)
        {
            dependencyManager.GetDependencyChecked<MultiplayerManager>().EstablishConnection(host, port);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            SortedDictionary<DrawLayer, List<IDrawable>> objects = new();

            foreach (var worldObject in world.LoadedLevel.GetObjects())
            {
                if (worldObject is IDrawable drawable)
                {
                    if (!objects.ContainsKey(drawable.DrawLayer))
                        objects.Add(drawable.DrawLayer, new());

                    objects[drawable.DrawLayer].Add(drawable);
                }
            }

            foreach (var layer in objects)
                foreach (var obj in layer.Value)
                    obj.Draw(spriteBatch);
        }
    }
}