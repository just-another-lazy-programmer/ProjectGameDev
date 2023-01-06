using Microsoft.Xna.Framework;
using ProjectGameDev.Levels;
using ProjectGameDev.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core
{
    public class World
    {
        public Level.Level LoadedLevel { get; set; }
        public UIContainer ActiveScreen { get; set; }
        public Color BackgroundColor { get; set; }

        public void Update(GameTime gameTime)
        {
            LoadedLevel?.Update(gameTime);
            ActiveScreen?.Update(gameTime);
        }
    }
}
