using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Levels;

namespace ProjectGameDev.Engine
{
    internal static class GlobalEngine
    {
        public static Level LoadedLevel { get; private set; }
        public static ContentManager ContentManager { get; private set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static Multiplayer MultiplayerManager { get; private set; } = new();
        public static Color BackgroundColor { get; set; }

        static public void LoadLevel(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;

            LoadedLevel = new TestLevel();
            LoadedLevel.Load(contentManager);
        }

        static public Texture2D LoadTexture(string name)
        {
            return ContentManager.Load<Texture2D>(name);
        }
    }
}
