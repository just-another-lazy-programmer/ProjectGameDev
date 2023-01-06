using Microsoft.Xna.Framework;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class PhysicsComponent : Component
    {
        protected CollisionComponent2 collisionComponent;
        protected RootComponent rootComponent;

        protected WorldObject movingObjectFloor;
        protected RootComponent movingObjectFloorRoot;
        protected float lastMovingObjectLocationX;

        private Vector2 velocity;
        private Vector2 acceleration;

        public Vector2 Location { get { return rootComponent.Location; } }
        public Vector2 Velocity { get { return velocity; } }
        public Vector2 Acceleration { get { return acceleration; } }

        public float MaxVelocityX { get; set; } = 4f;
        public float MaxVelocityY { get; set; } = 14f;

        public WorldObject Floor { get; protected set; }

        public PhysicsComponent()
        {
            WantsTick = true;
        }

        public override void Activate()
        {
            base.Activate();

            collisionComponent = Owner.GetComponent<CollisionComponent2>();
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

        public void RemoveFloor()
        {
            this.Floor = null;
        }

        public override void Tick(GameTime gameTime)
        {
            Floor = null;
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            acceleration += new Vector2(0, 10);
            velocity += acceleration * deltaTime;

            //ClampVector(ref velocity, MaxVelocity);
            ClampVelocity();

            Decellerate();

            var newLocationHorizontal = rootComponent.Location + new Vector2(velocity.X, 0);
            var newLocationVertical = rootComponent.Location + new Vector2(0, velocity.Y);

            var horizontallyColliding = collisionComponent.TestCollision(newLocationHorizontal);
            var verticallyColliding = collisionComponent.TestCollision(newLocationVertical);

            if (verticallyColliding != null && velocity.Y > 0)
                Floor = verticallyColliding;

            if (horizontallyColliding != null)
                velocity.X = 0;

            if (verticallyColliding != null)
                velocity.Y = 0;

            if (Floor != null && movingObjectFloor == null)
            {
                if (Floor.HasComponentFast<MovingPlatformComponent>())
                {
                    movingObjectFloor = Floor;
                    if (movingObjectFloor.TryGetComponentFast(out RootComponent rootComponent))
                    {
                        lastMovingObjectLocationX = rootComponent.Location.X;
                        movingObjectFloorRoot = rootComponent;
                    }
                }
            }

            if (movingObjectFloor != null)
            {
                if (Floor != null)
                {
                    this.rootComponent.Location += new Vector2(movingObjectFloorRoot.Location.X - lastMovingObjectLocationX, 0);
                    lastMovingObjectLocationX = movingObjectFloorRoot.Location.X;
                }
                else
                {
                    movingObjectFloor = null;
                    movingObjectFloorRoot = null;
                }
            }

            rootComponent.Location += velocity;
            acceleration = Vector2.Zero;

            //var newLocation = rootComponent.Location + velocity;
            //var collidingObjects = GetCollidingObjects();

            /*
            foreach (var obj in collidingObjects)
            {
                if (obj.TopHit)
                {
                    if (velocity.Y > 0)
                    {
                        acceleration.Y = 0;
                        velocity.Y = 0;
                    }

                    Floor = obj.ComponentHit;
                }
                else if (obj.BottomHit)
                {
                    if (velocity.Y < 0)
                    {
                        acceleration.Y = 0;
                        velocity.Y = 0;
                    }
                }
                else if (obj.LeftHit)
                {
                    if (velocity.X > 0)
                    {
                        acceleration.X = 0;
                        velocity.X = 0;
                    }
                }

                else if (obj.RightHit)
                {
                    if (velocity.X < 0)
                    {
                        acceleration.X = 0;
                        velocity.X = 0;
                    }
                }
            }

            rootComponent.Location += velocity;
            */
        }

        /*
        private List<HitInfo> GetCollidingObjects()
        {
            var result = new List<HitInfo>();
            foreach (var worldObject in GlobalEngine.LoadedLevel.GetObjects())
            {
                if (worldObject != Owner && worldObject is ICollision collision)
                {
                    var doesCollide = collisionComponent.TestCollision(
                        collision.CollisionComponent,
                        out HitInfo hitInfo,
                        rootComponent.Location);

                    if (doesCollide)
                        result.Add(hitInfo);
                }
            }
            return result;
        }
        */

        /*
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
        */

        public void ClampVelocity()
        {
            velocity.X = Math.Clamp(velocity.X, -MaxVelocityX, MaxVelocityX);
            velocity.Y = Math.Clamp(velocity.Y, -MaxVelocityY, MaxVelocityY);
        }

        private void Decellerate()
        {
            //if (acceleration.X == 0 && acceleration.Y == 0)
            //    velocity *= 0.9f;

            if (acceleration.X == 0)
                velocity.X *= 0.9f;
        }

        public void Impulse(Vector2 impulse)
        {
            velocity += impulse;
        }
    }
}
