using Microsoft.Xna.Framework;
using ProjectGameDev.ComponentInterfaces;
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
        protected RootComponent rootComponent;

        private Vector2 velocity;
        private Vector2 acceleration;

        public Vector2 Location { get { return rootComponent.Location; } }
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
            rootComponent = Owner.GetComponent<RootComponent>();
        }

        public void Teleport(Vector2 location)
        {
            rootComponent.Location = location;
        }

        public void SetAcceleration(Vector2 acceleration)
        {
            this.acceleration = acceleration;
        }

        public override void Tick(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            acceleration += new Vector2(0, 1);
            velocity += acceleration * deltaTime;

            ClampVector(ref velocity, MaxVelocity);

            Decellerate();

            var newLocation = rootComponent.Location + velocity;

            if (IsMoveAllowed(out Vector2 impactDirection, out WorldObject obj))
            {
                rootComponent.Location = newLocation;
            }
            else
            {
                if (Math.Abs(impactDirection.X) > Math.Abs(impactDirection.Y))
                {
                    velocity.X = 0;
                    acceleration.X = 0;
                }
                else
                {
                    velocity.Y = 0;
                    acceleration.Y = 0;
                }
                //acceleration = Vector2.Zero;
                //velocity = Vector2.Zero;
                //velocity = impactDirection*velocity;

                rootComponent.Location += velocity;
            }
        }

        bool IsMoveAllowed(out Vector2 outImpactDirection, out WorldObject obj)
        {
            foreach (var worldObject in GlobalEngine.LoadedLevel.GetObjects())
            {
                if (worldObject != Owner && worldObject is ICollision collision)
                {
                    var doesCollide = collisionComponent.TestCollision(
                        collision.CollisionComponent, 
                        out outImpactDirection, 
                        rootComponent.Location);

                    if (doesCollide)
                    {
                        obj = worldObject;
                        return false;
                    }
                }
            }

            outImpactDirection = new Vector2();
            obj = null;
            return true;
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
            //if (acceleration.X == 0 && acceleration.Y == 0)
            //    velocity *= 0.9f;

            if (acceleration.X == 0)
                velocity.X *= 0.9f;
        }
    }
}
