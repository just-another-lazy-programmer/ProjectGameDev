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
using ProjectGameDev.Core.Game;
using ProjectGameDev.Core.Game.States;

namespace ProjectGameDev.Levels.Level1
{
    internal class Level1 : Level
    {
        protected GameManager gameManager;

        public Level1(DependencyManager dependencyManager) : base(dependencyManager) 
        {
            dependencyManager.InjectChecked(ref gameManager);
        }

        // @NOTE: Platforms are added manually because we don't want to align them to the grid

        public override void Load()
        {
            AddObject(new Background(dependencyManager, "Backgrounds/background_0"));

            var loader = new LevelLoader(dependencyManager);
            loader.LoadTileMap("Attempt2.tmj", this, 1/16f); // 16x16
            var hero = new Hero2(dependencyManager);
            hero.CharacterMovement.Teleport(new Vector2(10, 400));
            AddObject(hero);
            var platform = new MovingPlatform(dependencyManager, new Vector2(500, 460), 45, 70);
            AddObject(platform);

            AddObject(new Platform(dependencyManager, new Vector2(350, 480)));

            var slime = new GreenSlime(dependencyManager);
            slime.GetComponentFast<RootComponent>().Move(new Vector2(525, 400));
            AddObject(slime);

            var orangeSlime = new OrangeSlime(dependencyManager);
            orangeSlime.GetComponentFast<RootComponent>().Move(new Vector2(700, 480));
            AddObject(orangeSlime);

            // layer 2

            AddObject(new Platform(dependencyManager, new Vector2(740, 410)));
            AddObject(new Platform(dependencyManager, new Vector2(550, 370)));
            AddObject(new MovingPlatform(dependencyManager, new Vector2(310, 350), 50, 65));
            AddObject(new Platform(dependencyManager, new Vector2(140, 370)));
            AddObject(new Platform(dependencyManager, new Vector2(10, 300)));

            // layer 3

            AddObject(new Platform(dependencyManager, new Vector2(140, 230)));
            AddObject(new Platform(dependencyManager, new Vector2(360, 220)));
            AddObject(new Platform(dependencyManager, new Vector2(590, 240)));
            
            var finish = new DestinationPlatform(dependencyManager, new Vector2(800, 240));
            finish.TriggerComponent.OnCollisionEvent += Finish_OnCollisionEvent;
            AddObject(finish);
        }

        private void Finish_OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            gameManager.TransitionTo<Level2State>();
        }
    }
}
