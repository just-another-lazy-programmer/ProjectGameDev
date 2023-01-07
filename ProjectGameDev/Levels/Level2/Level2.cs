using ProjectGameDev.Characters;
using ProjectGameDev.Characters.Enemies;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Level;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectGameDev.Levels.Level2
{
    internal class Level2 : Level
    {
        private Undead undead;
        public Level2(DependencyManager dependencyManager) : base(dependencyManager)
        {
        }

        public override void Load()
        {
            base.Load();

            AddObject(new Background(dependencyManager, "Backgrounds/background_0"));

            var loader = new LevelLoader(dependencyManager);
            //var graphicsDevice = dependencyManager.GetDependencyChecked<GraphicsDevice>();
            //int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            loader.LoadTileMap("Level2.tmj", this, 1 / 16f);

            AddObject(new Hero2(dependencyManager));

            // Layer 1 platforms

            AddObject(new MovingPlatform(dependencyManager, new Vector2(150, 380), 45, 70));
            AddObject(new MovingPlatform(dependencyManager, new Vector2(700, 380), 45, 70));

            // Layer 2 platforms

            var platform1 = new DestinationPlatform(dependencyManager, new Vector2(220, 250));
            platform1.TriggerComponent.OnCollisionEvent += OnDestinationReachedEvent;
            AddObject(platform1);

            var platform2 = new DestinationPlatform(dependencyManager, new Vector2(630, 250));
            platform2.TriggerComponent.OnCollisionEvent += OnDestinationReachedEvent;
            AddObject(platform2);

            var platform3 = new DestinationPlatform(dependencyManager, new Vector2(415, 580));
            platform3.TriggerComponent.OnCollisionEvent += OnDestinationReachedEvent;
            AddObject(platform3);

            undead = new Undead(dependencyManager);
            undead.GetComponentFast<RootComponent>().Move(new Vector2(370, 150));
            AddObject(undead);
        }

        private void OnDestinationReachedEvent(object sender, CollisionEventArgs e)
        {
            var component = (sender as HitboxComponent);
            var objectHit = component.Owner;

            var arrowComponent = objectHit.GetComponentFast<ArrowComponent>();

            if (arrowComponent != null && arrowComponent.IsActive)
            {
                arrowComponent.Deactivate();
                undead.Damage(3);
            }
        }
    }
}
