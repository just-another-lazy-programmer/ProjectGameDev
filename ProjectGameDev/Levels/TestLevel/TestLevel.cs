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
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Characters.Enemies;
using ProjectGameDev.UI.Elements;

namespace ProjectGameDev.Levels.TestLevel
{
    internal class TestLevel : Level
    {
        public TestLevel(DependencyManager dependencyManager) : base(dependencyManager) { }

        public override void Load()
        {
            var loader = new LevelLoader(dependencyManager);
            //var graphicsDevice = dependencyManager.GetDependencyChecked<GraphicsDevice>();
            //int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            loader.LoadTileMap("MossyTileSet", "Attempt2.tmj", this, 1/16f); // 16x16
            //var dependencyManager = new Engine.DependencyManager();
            AddObject(new Hero2(dependencyManager));
            AddObject(new MovingPlatform(dependencyManager, new Vector2(500, 450)));

            var slime = new GreenSlime(dependencyManager);
            slime.GetComponentFast<RootComponent>().Move(new Vector2(500, 300));
            AddObject(slime);

            var orangeSlime = new OrangeSlime(dependencyManager);
            orangeSlime.GetComponentFast<RootComponent>().Move(new Vector2(700, 300));
            AddObject(orangeSlime);


            //AddObject(new Hero2(dependencyManager));
            //objects[^1].GetComponent<MovementComponent>().Teleport(new Vector2(140, 140));
            //((Hero2)objects[^1]).HeroColor = Color.Orange;
            //AddObject(new DebugRectangle(dependencyManager, new Vector2(0, 400), new Point(800, 100)));
            //AddObject(new DebugRectangle(dependencyManager, new Vector2(500, 350), new Point(50, 50)));
            //AddObject(new DebugRectangle(dependencyManager, new Vector2(300, 250), new Point(100, 50)));
        }
    }
}
