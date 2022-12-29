﻿using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.Animations.Hero2;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
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
        public ReplicationComponent NetworkComponent { get; set; }

        private AnimationBuilder animationBuilder;

        private const double scale = 0.2;
        private const string textureAssetName = "hero_idle";

        public DrawLayer DrawLayer => DrawLayer.Player;

        public Hero2(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            var loadedTexture = LoadTexture(textureAssetName);

            // Create components
            RootComponent = CreateDefaultComponent<RootComponent>();
            CharacterMovement = CreateDefaultComponent<MovementComponent>();
            AnimationComponent = CreateDefaultComponent<AnimationComponent>();
            PhysicsComponent = CreateDefaultComponent<PhysicsComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent2>();
            NetworkComponent = CreateDefaultComponent<ReplicationComponent>();

            // Setup AnimationComponent
            AnimationComponent.SetAnimation(animationBuilder.GetAnimation<Hero2IdleAnimation>());

            // Setup Character Movement
            CharacterMovement.OnState(MovementState.Idle, animationBuilder.GetAnimation<Hero2IdleAnimation>());
            CharacterMovement.OnState(MovementState.Running, animationBuilder.GetAnimation<Hero2WalkingAnimation>());
            CharacterMovement.OnState(MovementState.Jumping, animationBuilder.GetAnimation<Hero2JumpingAnimation>());
            CharacterMovement.Speed = 10;

            // Setup collisions
            //CollisionComponent.AddHitbox(0, 0, (int)(loadedTexture.Bounds.Width * scale), (int)(loadedTexture.Bounds.Height * scale));
            //var source = animationBuilder.GetAnimation<Hero2IdleAnimation>().CurrentFrame.SourceRectangle;
            //CollisionComponent.AddHitbox(0, 0, (int)(source.Width * scale), (int)(source.Height * scale));
            CollisionComponent.AddHitbox(35, 25, 30, 55);

            // Active components
            ActivateComponents();

            CharacterMovement.Teleport(new Microsoft.Xna.Framework.Vector2(10, 200));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, scale);
            //CollisionComponent.DebugDraw(spriteBatch);
        }
    }
}