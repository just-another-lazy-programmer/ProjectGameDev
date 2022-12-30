using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.GreenSlime;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters.Enemies
{
    internal class Slime : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;
        protected AnimationBuilder animationBuilder;
        protected AnimationComponent animationComponent;
        protected PhysicsComponent physicsComponent;
        protected CollisionComponent2 collisionComponent;
        protected const double scale = 0.2;
        protected CooldownManager cooldownManager;

        public Slime(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.Inject(ref cooldownManager);

            CreateDefaultComponent<RootComponent>();

            collisionComponent = CreateDefaultComponent<CollisionComponent2>();
            collisionComponent.AddHitbox(10, 0, 52, 45);
            collisionComponent.IgnoreHitbox = true; // <- we only want trigger

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<GreenSlimeIdleAnimation>());

            physicsComponent = CreateDefaultComponent<PhysicsComponent>();

            ActivateComponents();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
            //collisionComponent.DebugDraw(spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (!cooldownManager.IsOnCooldown(this, null, 5))
            {
                cooldownManager.SetCooldown(this, null);
                Debug.WriteLine("Yay!");
            }
        }
    }
}
