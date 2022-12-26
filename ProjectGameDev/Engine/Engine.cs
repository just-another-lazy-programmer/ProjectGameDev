using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine.Multiplayer;
using ProjectGameDev.Levels;
using ProjectGameDev.Utility;

namespace ProjectGameDev.Engine
{
    // This is deprecated and should be replaced with a non-static class
    internal static class GlobalEngine
    {
        public static Level LoadedLevel { get; private set; }
        public static ContentManager ContentManager { get; private set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static MultiplayerManager MultiplayerManager { get; private set; } = new();
        public static Color BackgroundColor { get; set; }

        static public void LoadLevel(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;

            var dependencyManager = new DependencyManager();
            dependencyManager.RegisterDependency(contentManager);
            dependencyManager.RegisterDependency(graphicsDevice);
            dependencyManager.RegisterDependency(MultiplayerManager);
            dependencyManager.RegisterDependency(new SimpleSprites());

            LoadedLevel = new TestLevel(dependencyManager);

            // @TODO: Clean up
            dependencyManager.RegisterDependency(new World() { LoadedLevel = LoadedLevel, BackgroundColor = BackgroundColor });

            LoadedLevel.Load();
        }

        static public void Tick()
        {
            foreach (var obj in LoadedLevel.GetObjects())
            {
                if (obj is IReplicate replicate)
                {
                   //replicate.NetworkComponent. 
                }
            }
        }

        static public async Task ConnectMultiplayer(string host, ushort port)
        {
            await MultiplayerManager.EstablishConnection(host, port);
        }

        static public Texture2D LoadTexture(string name)
        {
            return ContentManager.Load<Texture2D>(name);
        }
    }
}
