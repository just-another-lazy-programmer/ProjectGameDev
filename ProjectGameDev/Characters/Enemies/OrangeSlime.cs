using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.GreenSlime;
using ProjectGameDev.Animations.Slime;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters.Enemies
{
    internal class OrangeSlime : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;
        protected AnimationBuilder animationBuilder;
        protected AnimationComponent animationComponent;
        protected PhysicsComponent physicsComponent;
        protected CollisionComponent2 collisionComponent;
        protected const double scale = 0.2;
        protected CooldownManager cooldownManager;

        public OrangeSlime(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.Inject(ref cooldownManager);

            CreateDefaultComponent<RootComponent>();

            collisionComponent = CreateDefaultComponent<CollisionComponent2>();
            collisionComponent.AddHitbox(10, 10, 80, 55);
            collisionComponent.IgnoreHitbox = true; // <- we only want trigger

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<OrangeSlimeIdleAnimation>());

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

            if (!cooldownManager.IsOnCooldown(this, null, 2))
            {
                cooldownManager.SetCooldown(this, null);
                physicsComponent.Impulse(new Microsoft.Xna.Framework.Vector2(0, -4));
            }
        }
    }
}
