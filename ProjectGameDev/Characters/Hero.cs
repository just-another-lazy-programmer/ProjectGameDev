using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Levels;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters
{
    internal class Hero : WorldObject, Core.IDrawable, IPhysics, ICollision, IReplicate
    {
        public RootComponent RootComponent { get; protected set; }
        public MovementComponent CharacterMovement { get; protected set; }
        public AnimationComponent AnimationComponent { get; protected set; }
        public PhysicsComponent PhysicsComponent { get; protected set; }
        public CollisionComponent2 CollisionComponent { get; protected set; }
        public ReplicationComponent NetworkComponent { get; set; }

        private AnimationBuilder animationBuilder;

        public DrawLayer DrawLayer => DrawLayer.DebugTop;

        private const double scale = 0.2;
        private const string textureAssetName = "hero";

        public Hero(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.Inject(ref animationBuilder);
            var loadedTexture = LoadTexture(textureAssetName);

            // Create components
            RootComponent = CreateDefaultComponent<RootComponent>();
            CharacterMovement = CreateDefaultComponent<MovementComponent>();
            AnimationComponent = CreateDefaultComponent<AnimationComponent>();
            PhysicsComponent = CreateDefaultComponent<PhysicsComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent2>();
            NetworkComponent = CreateDefaultComponent<ReplicationComponent>();

            // Setup AnimationComponent
            AnimationComponent.SetAnimation(animationBuilder.GetAnimation<HeroIdleAnimation>());
            AnimationComponent.SetTexture(loadedTexture);

            // Setup Character Movement
            CharacterMovement.OnState(MovementState.Idle, animationBuilder.GetAnimation<HeroIdleAnimation>());
            CharacterMovement.OnState(MovementState.Running, animationBuilder.GetAnimation<HeroWalkingAnimation>());
            CharacterMovement.OnState(MovementState.Jumping, animationBuilder.GetAnimation<HeroJumpingAnimation>());
            CharacterMovement.Speed = 10;

            // Setup collisions
            //CollisionComponent.AddHitbox(0, 0, (int)(loadedTexture.Bounds.Width * scale), (int)(loadedTexture.Bounds.Height * scale));
            var source = animationBuilder.GetAnimation<HeroIdleAnimation>().CurrentFrame.SourceRectangle;
            CollisionComponent.AddHitbox(0, 0, (int)(source.Width*scale), (int)(source.Height*scale));

            // Active components
            ActivateComponents();

            CharacterMovement.Teleport(new Vector2(10, 200));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, scale);
            //CollisionComponent.DebugDraw(spriteBatch);
        }
    }
}
