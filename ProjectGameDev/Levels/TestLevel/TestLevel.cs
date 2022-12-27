using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Level;

namespace ProjectGameDev.Levels.TestLevel
{
    internal class TestLevel : Level
    {
        public TestLevel(DependencyManager dependencyManager) : base(dependencyManager) { }

        public override void Load()
        {
            var loader = new LevelLoader(dependencyManager);
            loader.LoadTileMap("MossyTileSet", "Map1.tmj", this);
            //var dependencyManager = new Engine.DependencyManager();
            AddObject(new Hero(dependencyManager));
            //AddObject(new Hero());
            //objects[1].GetComponent<MovementComponent>().Teleport(new Vector2(40, 40));
            AddObject(new DebugRectangle(dependencyManager, new Vector2(0, 400), new Point(800, 100)));
            AddObject(new DebugRectangle(dependencyManager, new Vector2(500, 350), new Point(50, 50)));
            AddObject(new DebugRectangle(dependencyManager, new Vector2(300, 250), new Point(100, 50)));
        }
    }
}
