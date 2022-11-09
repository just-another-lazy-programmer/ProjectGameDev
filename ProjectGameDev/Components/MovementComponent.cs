using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        public MovementComponent()
        {
            WantsTick = true;
        }

        public override void Activate()
        {
            base.Activate();

            physicsComponent = Owner.GetComponent<PhysicsComponent>();
        }

        public void Teleport(Vector2 location)
        {
            var physics = physicsComponent;

            physics.Teleport(location);
        }

        private void UpdatePhysics(Vector2 direction)
        {
            var physics = physicsComponent;

            physics.SetAcceleration(direction * Speed);
        }

        public override void Tick(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var direction = new Vector2();

            if (state.IsKeyDown(Keys.Left))
                direction.X -= 1;

            if (state.IsKeyDown(Keys.Right))
                direction.X += 1;

            UpdatePhysics(direction);
        }
    }
}
