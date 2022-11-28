using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Engine;
using ProjectGameDev.Levels;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters
{
    internal class Hero : WorldObject, Engine.IDrawable, IPhysics, ICollision
    {
        public RootComponent RootComponent { get; protected set; }
        public MovementComponent CharacterMovement { get; protected set; }
        public AnimationComponent AnimationComponent { get; protected set; }
        public PhysicsComponent PhysicsComponent { get; protected set; }
        public CollisionComponent2 CollisionComponent { get; protected set; }

        public DrawLayer DrawLayer => DrawLayer.DebugTop;

        private const double scale = 0.2;
        private const string textureAssetName = "hero";

        public Hero()
        {
            var loadedTexture = LoadTexture(textureAssetName);
            RootComponent = CreateDefaultComponent<RootComponent>();
            CharacterMovement = CreateDefaultComponent<MovementComponent>();
            AnimationComponent = CreateDefaultComponent<AnimationComponent>();
            PhysicsComponent = CreateDefaultComponent<PhysicsComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent2>();

            AnimationComponent.SetAnimation(AnimationBuilder.GetAnimation<HeroIdleAnimation>());
            AnimationComponent.SetTexture(loadedTexture);

            CharacterMovement.OnState(MovementState.Idle, AnimationBuilder.GetAnimation<HeroIdleAnimation>());
            CharacterMovement.OnState(MovementState.Running, AnimationBuilder.GetAnimation<HeroWalkingAnimation>());
            CharacterMovement.OnState(MovementState.Jumping, AnimationBuilder.GetAnimation<HeroJumpingAnimation>());

            //CollisionComponent.AddHitbox(0, 0, (int)(loadedTexture.Bounds.Width * scale), (int)(loadedTexture.Bounds.Height * scale));
            var source = AnimationBuilder.GetAnimation<HeroIdleAnimation>().CurrentFrame.SourceRectangle;
            CollisionComponent.AddHitbox(0, 0, (int)(source.Width*scale), (int)(source.Height*scale));

            CharacterMovement.Speed = 10;

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
