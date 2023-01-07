using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Undead;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters.Enemies
{
    internal class UndeadSummon : WorldObject, Core.IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.EnemiesTop;

        private readonly AnimationComponent animationComponent;
        private readonly RootComponent rootComponent;
        private readonly TriggerComponent triggerComponent;

        private readonly AnimationBuilder animationBuilder;
        private readonly World world;
        private const double scale = 2f;

        private Vector2 direction;
        private float velocity = 5f;

        private const double lifespan = 2;
        private double timeAlive = 0;

        private UndeadSummonState state = UndeadSummonState.Spawning;

        public UndeadSummon(DependencyManager dependencyManager, Vector2 location) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.InjectChecked(ref world);

            rootComponent = CreateDefaultComponent<RootComponent>();
            rootComponent.Move(location);

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadSummonAppearAnimation>());
            animationComponent.SetFlip(true);

            animationComponent.OnAnimationFinishedEvent += AnimationComponent_OnAnimationFinishedEvent;

            triggerComponent = CreateDefaultComponent<TriggerComponent>();
            triggerComponent.AddHitbox(30, 32, 35, 45);

            triggerComponent.OnCollisionEvent += TriggerComponent_OnCollisionEvent;
            triggerComponent.CooldownTime = 10;

            ActivateComponents();
        }

        private void TriggerComponent_OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            if (state != UndeadSummonState.Moving) return;

            var objectHit = e.ObjectHit;

            if (objectHit.TryGetComponentFast(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(this, 15);
                state = UndeadSummonState.Dying;

                AnimateAndDestroy();
            }
        }

        private void AnimateAndDestroy()
        {
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadSummonDeathAnimation>());
        }

        private void AnimationComponent_OnAnimationFinishedEvent(object sender, EventArgs e)
        {
            switch (state)
            {
                case UndeadSummonState.Spawning:
                    {
                        state = UndeadSummonState.Moving;
                        animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadSummonIdleAnimation>());

                        StartMovingToPlayer();
                        break;
                    }
                case UndeadSummonState.Dying:
                    {
                        Destroy();
                        break;
                    }
            }
        }

        private void StartMovingToPlayer()
        {
            var player = world.LoadedLevel.GetObject<Hero2>();
            direction = player.RootComponent.Location - rootComponent.Location;
            direction.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (state == UndeadSummonState.Moving)
            {
                rootComponent.Location += direction * velocity;


                timeAlive += gameTime.ElapsedGameTime.TotalSeconds;

                if (timeAlive > lifespan)
                {
                    state = UndeadSummonState.Dying;
                    AnimateAndDestroy();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
            //triggerComponent.DebugDraw(spriteBatch);
        }
    }

    internal enum UndeadSummonState
    {
        Spawning,
        Moving,
        Dying
    }
}
