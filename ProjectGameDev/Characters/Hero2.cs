using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero2;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters
{
    internal class Hero2 : WorldObject, Core.IDrawable
    {
        public RootComponent RootComponent { get; protected set; }
        public MovementComponent CharacterMovement { get; protected set; }
        public AnimationComponent AnimationComponent { get; protected set; }
        public PhysicsComponent PhysicsComponent { get; protected set; }
        public CollisionComponent2 CollisionComponent { get; protected set; }
        public ReplicationComponent NetworkComponent { get; protected set; }
        public HealthComponent HealthComponent { get; protected set; }

        private readonly AnimationBuilder animationBuilder;
        private readonly CooldownManager cooldownManager;
        private readonly GraphicsDevice graphicsDevice;

        private const double scale = 0.2;
        private const string textureAssetName = "hero_idle";

        private const float redFrameLength = 0.2f;

        private bool redFrameRequested = false;
        private bool inRedFrame = false;
        private readonly object redFrameKey = new();

        public Color HeroColor { get; set; } = Color.White;

        public DrawLayer DrawLayer => DrawLayer.Player;

        public Hero2(DependencyManager dependencyManager) : base(dependencyManager)
        {
            // Register Dependencies
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.InjectChecked(ref graphicsDevice);
            dependencyManager.Inject(ref cooldownManager);

            // Load textures
            var loadedTexture = LoadTexture(textureAssetName);

            // Create Components
            RootComponent = CreateDefaultComponent<RootComponent>();
            CharacterMovement = CreateDefaultComponent<MovementComponent>();
            AnimationComponent = CreateDefaultComponent<AnimationComponent>();
            PhysicsComponent = CreateDefaultComponent<PhysicsComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent2>();
            NetworkComponent = CreateDefaultComponent<ReplicationComponent>();
            HealthComponent = CreateDefaultComponent<HealthComponent>();

            // Setup Animation Component
            AnimationComponent.SetAnimation(animationBuilder.GetAnimation<Hero2IdleAnimation>());

            // Setup Character Movement
            CharacterMovement.OnState(MovementState.Idle, animationBuilder.GetAnimation<Hero2IdleAnimation>());
            CharacterMovement.OnState(MovementState.Running, animationBuilder.GetAnimation<Hero2WalkingAnimation>());
            CharacterMovement.OnState(MovementState.Jumping, animationBuilder.GetAnimation<Hero2JumpingAnimation>());
            CharacterMovement.Speed = 10;

            // Setup Collisions
            CollisionComponent.AddHitbox(35, 25, 30, 55);
            CollisionComponent.ShouldTrigger = true;

            // Setup Health Component
            HealthComponent.MaxHealth = 100;
            HealthComponent.OnHealthChangedEvent += HealthComponent_OnHealthChangedEvent;

            // Activate Components
            ActivateComponents();

            //CharacterMovement.Teleport(new Vector2(10, 200));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (RootComponent.Location.Y > graphicsDevice.PresentationParameters.BackBufferHeight)
            {
                HealthComponent.TakeDamage(this, HealthComponent.MaxHealth);
            }
        }

        private void HealthComponent_OnHealthChangedEvent(object sender, HealthChangeEventArgs e)
        {
            if (e.IsDamage)
            {
                redFrameRequested = true;
            }
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

            var color = inRedFrame ? Color.Red : HeroColor;
            AnimationComponent.Draw(spriteBatch, scale, color);
            //CollisionComponent.DebugDraw(spriteBatch);
        }
    }
}
