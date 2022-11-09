using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters
{
    internal class Hero : WorldObject, Engine.IDrawable, IPhysics
    {
        public MovementComponent CharacterMovement { get; protected set; }
        public AnimationComponent AnimationComponent { get; protected set; }
        public PhysicsComponent PhysicsComponent { get; protected set; }

        private const double scale = 0.2;

        public Hero(Texture2D texture)
        {
            CharacterMovement = CreateDefaultComponent<MovementComponent>();
            AnimationComponent = CreateDefaultComponent<AnimationComponent>();
            PhysicsComponent = CreateDefaultComponent<PhysicsComponent>();

            AnimationComponent.SetAnimation(AnimationBuilder.GetAnimation<HeroIdleAnimation>());
            AnimationComponent.SetTexture(texture);

            CharacterMovement.Speed = 2;

            ActivateComponents();

            CharacterMovement.Teleport(new Vector2(10, 200));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, scale);
        }
    }
}
