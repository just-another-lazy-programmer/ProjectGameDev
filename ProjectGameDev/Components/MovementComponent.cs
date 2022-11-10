﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal enum MovementState
    {
        Idle,
        Running
    }

    internal class MovementComponent : Component
    {
        public MovementState MovementState { get; protected set; }
        public float Speed { get; set; }

        protected PhysicsComponent physicsComponent;
        protected AnimationComponent animationComponent;
        protected bool facingRight = true;
        protected MovementState lastState;

        public Dictionary<MovementState, Animation> Animations { get; protected set; } = new(); 

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

        public void OnState(MovementState state, Animation animation)
        {
            Animations.Add(state, animation);
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

            if (state.IsKeyDown(Keys.Space) && physicsComponent.Floor != null)
                physicsComponent.Impulse(new Vector2(0, -5));

            UpdateFacing(direction);
            UpdateState(direction);
            UpdateAnimation();

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

        public void UpdateState(Vector2 vector)
        {
            if (vector.Length() > 0.1)
            {
                MovementState = MovementState.Running;
            }
            else
            {
                MovementState = MovementState.Idle;
            }
        }

        public void UpdateAnimation()
        {
            if (lastState != MovementState)
            {
                // @todo: default animation?
                animationComponent.SetAnimation(Animations[MovementState]);
                lastState = MovementState;
            }
        }
    }
}
