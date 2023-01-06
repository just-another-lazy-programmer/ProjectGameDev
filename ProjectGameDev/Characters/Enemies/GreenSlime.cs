using Microsoft.Xna.Framework;
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
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.Characters.Enemies
{
    internal class GreenSlime : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;
        protected AnimationBuilder animationBuilder;
        protected AnimationComponent animationComponent;
        protected PhysicsComponent physicsComponent;
        protected CollisionComponent2 collisionComponent;
        protected TriggerComponent triggerComponent;
        protected const double scale = 0.13;
        protected CooldownManager cooldownManager;

        public GreenSlime(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.Inject(ref cooldownManager);

            var hitbox = new Rectangle(5, 0, 35, 28);

            CreateDefaultComponent<RootComponent>();

            collisionComponent = CreateDefaultComponent<CollisionComponent2>();
            collisionComponent.AddHitbox(hitbox);
            collisionComponent.IgnoreHitbox = true; // <- we only want trigger

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<GreenSlimeIdleAnimation>());

            physicsComponent = CreateDefaultComponent<PhysicsComponent>();

            triggerComponent = CreateDefaultComponent<TriggerComponent>();
            triggerComponent.AddHitbox(hitbox);
            triggerComponent.CooldownTime = 2;

            triggerComponent.OnCollisionEvent += TriggerComponent_OnCollisionEvent;

            ActivateComponents();
        }

        private void TriggerComponent_OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            var objectHit = e.ObjectHit;
            
            if (objectHit.TryGetComponentFast(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(this, 30);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
            //collisionComponent.DebugDraw(spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
