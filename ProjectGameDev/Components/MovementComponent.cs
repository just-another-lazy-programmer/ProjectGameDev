using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core;
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
        Running,
        Jumping,
        Falling,
        Sliding
    }

    internal class MovementComponent : Component
    {
        public MovementState MovementState { get; protected set; }
        public float Speed { get; set; }

        protected PhysicsComponent physicsComponent;
        protected AnimationComponent animationComponent;
        protected bool facingRight = true;
        protected MovementState lastState;

        protected InputManager inputManager;

        public Dictionary<MovementState, Animation> Animations { get; protected set; } = new(); 

        public MovementComponent()
        {
            WantsTick = true;
        }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.Inject(ref inputManager);
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
            var inAirMultiplier = physicsComponent.Floor != null ? 1f : 0.5f;
            physicsComponent.SetAcceleration(direction * Speed * inAirMultiplier);
        }

        public override void Tick(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var direction = new Vector2();

            if (state.IsKeyDown(inputManager.GetKeyForAction(InputAction.MoveLeft)))
                direction.X -= 1;

            if (state.IsKeyDown(inputManager.GetKeyForAction(InputAction.MoveRight)))
                direction.X += 1;

            if (state.IsKeyDown(inputManager.GetKeyForAction(InputAction.Jump)) && physicsComponent.Floor != null)
            {
                physicsComponent.Impulse(new Vector2(0, -5));
                physicsComponent.RemoveFloor();
                MovementState = MovementState.Jumping;
            }

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
            switch (MovementState)
            {
                case MovementState.Idle:
                case MovementState.Running:
                case MovementState.Falling:
                    {
                        if (physicsComponent.Velocity.Y > float.Epsilon)
                            break;

                        if (Math.Abs(vector.X) > 0.1)
                        {
                            MovementState = MovementState.Running;
                        }
                        else
                        {
                            MovementState = MovementState.Idle;
                        }

                        break;
                    }
                case MovementState.Jumping:
                    {
                        if (physicsComponent.Velocity.Y > 0)
                            MovementState = MovementState.Falling;

                        // In case the jump failed
                        if (physicsComponent.Floor != null)
                            MovementState = MovementState.Idle;

                        break;
                    }
            }
        }

        public void UpdateAnimation()
        {
            if (lastState != MovementState)
            {
                // @todo: default animation?
                if (Animations.ContainsKey(MovementState))
                    animationComponent.SetAnimation(Animations[MovementState]);
                lastState = MovementState;
            }
        }
    }
}
