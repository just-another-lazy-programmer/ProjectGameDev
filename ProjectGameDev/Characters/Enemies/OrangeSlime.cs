using Microsoft.Xna.Framework;
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
    internal class OrangeSlime : WorldObject, Core.IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;
        protected AnimationBuilder animationBuilder;
        protected RootComponent rootComponent;
        protected AnimationComponent animationComponent;
        protected PhysicsComponent physicsComponent;
        protected CollisionComponent2 collisionComponent;
        protected TriggerComponent triggerComponent;
        protected const double scale = 0.2;
        protected CooldownManager cooldownManager;
        protected World world;
        protected Hero2 player;

        protected const int range = 200;

        public OrangeSlime(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.InjectChecked(ref world);
            dependencyManager.Inject(ref cooldownManager);

            var hitbox = new Rectangle(10, 10, 80, 55);

            rootComponent = CreateDefaultComponent<RootComponent>();

            collisionComponent = CreateDefaultComponent<CollisionComponent2>();
            collisionComponent.AddHitbox(hitbox);
            collisionComponent.IgnoreHitbox = true; // <- we only want trigger

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<OrangeSlimeIdleAnimation>());

            triggerComponent = CreateDefaultComponent<TriggerComponent>();
            triggerComponent.AddHitbox(hitbox);
            triggerComponent.CooldownTime = 0.4f;
            triggerComponent.OnCollisionEvent += TriggerComponent_OnCollisionEvent;

            physicsComponent = CreateDefaultComponent<PhysicsComponent>();

            ActivateComponents();
        }

        private void TriggerComponent_OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            var objectHit = e.ObjectHit;

            if (objectHit.TryGetComponentFast(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(this, 15);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!cooldownManager.IsOnCooldown(this, null, 2) && IsPlayerInRange())
            {
                cooldownManager.SetCooldown(this, null);
                physicsComponent.Impulse(new Vector2(0, -5));
            }
        }

        private bool IsPlayerInRange()
        {
            player ??= world.LoadedLevel.GetObject<Hero2>();
            if (player == null) return false;

            // @todo: possibly cache for faster access?
            var playerLocation = player.GetComponentFast<RootComponent>().Location;

            return (playerLocation - rootComponent.Location).Length() <= range;
        }
    }
}
