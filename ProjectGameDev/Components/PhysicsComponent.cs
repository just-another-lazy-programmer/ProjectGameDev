using Microsoft.Xna.Framework;
using ProjectGameDev.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class PhysicsComponent : Component
    {
        protected CollisionComponent collisionComponent;

        private Vector2 location;
        private Vector2 velocity;
        private Vector2 acceleration;

        public Vector2 Location { get { return location; } }
        public Vector2 Velocity { get { return velocity; } }
        public Vector2 Acceleration { get { return acceleration; } }

        public float MaxVelocity { get; set; } = 3f;

        public PhysicsComponent()
        {
            WantsTick = true;
        }

        public override void Activate()
        {
            base.Activate();

            collisionComponent = Owner.GetComponent<CollisionComponent>();
        }

        public void Teleport(Vector2 location)
        {
            this.location = location;
        }

        public void SetAcceleration(Vector2 acceleration)
        {
            this.acceleration = acceleration;
        }

        public override void Tick(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += acceleration * deltaTime;

            ClampVector(ref velocity, MaxVelocity);

            Decellerate();

            var newLocation = location + velocity;



            location = newLocation;
        }

        private Vector2 ClampVector(ref Vector2 vector, float max)
        {
            var length = vector.Length();

            if (length > max)
            {
                var ratio = max / length;
                vector.X *= ratio;
                vector.Y *= ratio;
            }

            return vector;
        }

        private void Decellerate()
        {
            if (acceleration.X == 0 && acceleration.Y == 0)
                velocity *= 0.9f;
        }
    }
}
