using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Undead;
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
    internal class Undead : WorldObject, Core.IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;

        private readonly AnimationComponent animationComponent;
        private readonly RootComponent rootComponent;
        private readonly TriggerComponent triggerComponent;
        private readonly CooldownManager cooldownManager;
        private readonly TimerManager timerManager;

        private readonly object summoningKey = new();

        public HealthComponent HealthComponent { get; private set; }

        private readonly AnimationBuilder animationBuilder;
        private readonly World world;

        private const double scale = 2f;
        private bool isLookingRight = false;
        private Hero2 player;

        private UndeadState state = UndeadState.Idle;
        private const int summonCooldown = 4;

        private bool redFrameRequested = false;
        private bool inRedFrame = false;
        private readonly object redFrameKey = new();
        private readonly float redFrameLength = 0.2f;

        public Undead(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.InjectChecked(ref world);
            dependencyManager.Inject(ref cooldownManager);
            dependencyManager.Inject(ref timerManager);

            rootComponent = CreateDefaultComponent<RootComponent>();

            triggerComponent = CreateDefaultComponent<TriggerComponent>();
            triggerComponent.AddHitbox(50, 30, 110, 150);
            triggerComponent.CooldownTime = 5;

            triggerComponent.OnCollisionEvent += TriggerComponent_OnCollisionEvent;

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());
            animationComponent.SetFlip(!isLookingRight);

            animationComponent.OnAnimationFinishedEvent += AnimationComponent_OnAnimationFinishedEvent;

            HealthComponent = CreateDefaultComponent<HealthComponent>();
            HealthComponent.MaxHealth = 4;

            //DelayedTestSummon();

            ActivateComponents();
        }

        private void TriggerComponent_OnCollisionEvent(object sender, CollisionEventArgs e)
        {
            var objectHit = e.ObjectHit;

            if (objectHit is not Hero2) return;
            if (state != UndeadState.Idle) return;

            state = UndeadState.Attacking;

            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadAttackAnimation>());
            animationComponent.SetFlip(!isLookingRight);

            const int frameBound1 = 3;
            const int frameBound2 = 10;

            animationComponent.ClearBindings();

            animationComponent.BindFrame(frameBound1, () =>
            {
                AttackInternal();
                animationComponent.RemoveBinding(frameBound1);
            });

            animationComponent.BindFrame(frameBound2, () =>
            {
                AttackInternal();
                animationComponent.RemoveBinding(frameBound2);

                state = UndeadState.Idle;
            });
        }

        private void AttackInternal()
        {
            if (CollisionComponent2.TestCollisionSingle(player.CollisionComponent.GetCollisionRects(), triggerComponent))
            {
                if (player.TryGetComponentFast(out HealthComponent healthComponent))
                {
                    healthComponent.TakeDamage(this, 10);
                }
            }
        }

        private void AnimationComponent_OnAnimationFinishedEvent(object sender, EventArgs e)
        {
            if (state == UndeadState.Summoning)
            {
                state = UndeadState.Idle;
                animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());

                //DelayedTestSummon();
            }
        }

        private void Summon() // @todo: set on cooldown, states?
        {
            state = UndeadState.Summoning;
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadSummonAnimation>());
            const int frameBound = 2;
            animationComponent.BindFrame(frameBound, () =>
            {
                SpawnSummon();
                animationComponent.RemoveBinding(frameBound);
            });
        }

        private void SpawnSummon()
        {
            var summon = new UndeadSummon(DependencyManager, rootComponent.Location + new Microsoft.Xna.Framework.Vector2(0, 0));
            world.LoadedLevel?.AddObjectSafe(summon);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (redFrameRequested)
            {
                inRedFrame = true;
                redFrameRequested = false;
                cooldownManager.SetCooldown(this, redFrameKey);
            }

            if (inRedFrame && !cooldownManager.IsOnCooldown(this, redFrameKey, redFrameLength))
            {
                inRedFrame = false;
            }

            var color = inRedFrame ? Color.Red : Color.White;

            animationComponent.Draw(spriteBatch, scale, color);
            //triggerComponent.DebugDraw(spriteBatch);
        }

        public void Damage(float durationSeconds)
        {
            HealthComponent.TakeDamage(player, 1);
            state = UndeadState.Stunned;
            animationComponent.ClearBindings();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());

            redFrameRequested = true;

            timerManager.Delay(durationSeconds, () =>
            {
                state = UndeadState.Idle;
            });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            player ??= world.LoadedLevel.GetObject<Hero2>();
            var delta = (player.RootComponent.Location - rootComponent.Location);

            if (isLookingRight && (delta.X < 0))
            {
                isLookingRight = false;
                animationComponent.SetFlip(!isLookingRight);
            }
            else if (!isLookingRight && (delta.X > 0))
            {
                isLookingRight = true;
                animationComponent.SetFlip(!isLookingRight);
            }

            if (!cooldownManager.IsOnCooldown(this, summoningKey, summonCooldown, true))
            {
                if (state == UndeadState.Idle) 
                    Summon();

                cooldownManager.SetCooldown(this, summoningKey);
            }
        }
    }

    internal enum UndeadState
    {
        Idle,
        Summoning,
        Attacking,
        Stunned,
        Dying
    }
}
