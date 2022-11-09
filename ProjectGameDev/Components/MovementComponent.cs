using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class MovementComponent : Component
    {
        public IPhysics Target { get; protected set; }
        public float Speed { get; set; }

        protected PhysicsComponent physicsComponent;
        protected AnimationComponent animationComponent;

        protected bool facingRight = true;

        public MovementComponent()
        {
            WantsTick = true;
        }

        public override void Activate()
        {
            base.Activate();

            physicsComponent = Owner.GetComponent<PhysicsComponent>();
            animationComponent = Owner.GetComponent<AnimationComponent>();
        }

        public void Teleport(Vector2 location)
        {
            physicsComponent.Teleport(location);
        }

        private void UpdatePhysics(Vector2 direction)
        {
            physicsComponent.SetAcceleration(direction * Speed);
        }

        public override void Tick(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var direction = new Vector2();

            if (state.IsKeyDown(Keys.Left))
                direction.X -= 1;

            if (state.IsKeyDown(Keys.Right))
                direction.X += 1;

            UpdateFacing(direction);
            UpdateAnimation(direction);

            animationComponent.SetFlip(!facingRight);

            UpdatePhysics(direction);
        }

        public void UpdateFacing(Vector2 vector)
        {
            if (vector.X > 0 && !facingRight)
                facingRight = true;
            else if (vector.X < 0 && facingRight)
                facingRight = false;
        }

        public void UpdateAnimation(Vector2 vector)
        {
            if (vector.Length() > 0.1)
            {
                animationComponent.SetAnimation(AnimationBuilder.GetAnimation<HeroWalkingAnimation>());
            }
            else
            {
                animationComponent.SetAnimation(AnimationBuilder.GetAnimation<HeroIdleAnimation>());
            }
        }
    }
}
