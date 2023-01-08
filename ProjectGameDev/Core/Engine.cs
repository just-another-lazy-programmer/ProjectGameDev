using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Levels.Level1;
using ProjectGameDev.Utility;

namespace ProjectGameDev.Core
{
    internal class Engine
    {
        private readonly DependencyManager dependencyManager;
        private readonly World world;
        private readonly SoundManager soundManager;

        public Engine(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
            dependencyManager.Inject(ref world);
            dependencyManager.InjectChecked(ref soundManager);
        }

        public void LoadLevel()
        {
            var level = new Level1(dependencyManager);
            level.Load();

            world.LoadedLevel = level;
        }

        public void Tick(GameTime gameTime)
        {
            world.Update(gameTime);
            soundManager.Update();
        }

        internal void Draw(SpriteBatch spriteBatch)
        {

            SortedDictionary<DrawLayer, List<IDrawable>> objects = new();

            if (world.LoadedLevel != null)
            {
                // add world objects
                foreach (var worldObject in world.LoadedLevel.GetObjects())
                {
                    if (worldObject is IDrawable drawable)
                    {
                        if (!objects.ContainsKey(drawable.DrawLayer))
                            objects.Add(drawable.DrawLayer, new());

                        objects[drawable.DrawLayer].Add(drawable);
                    }
                }
            }

            if (world.ActiveScreen != null)
            {
                // add ui elements
                foreach (var element in world.ActiveScreen.GetElements())
                {
                    if (element is IDrawable drawable)
                    {
                        if (!objects.ContainsKey(drawable.DrawLayer))
                            objects.Add(drawable.DrawLayer, new());

                        objects[drawable.DrawLayer].Add(drawable);
                    }
                }
            }

            // draw all layers
            foreach (var layer in objects)
                foreach (var obj in layer.Value)
                    obj.Draw(spriteBatch);
        }
    }
}
