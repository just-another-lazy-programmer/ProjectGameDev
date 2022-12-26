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

        public void Tick()
        {
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
    }
}
