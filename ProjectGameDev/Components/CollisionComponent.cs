using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class CollisionComponent : Component
    {
        protected RootComponent rootComponent;
        protected List<Rectangle> collisionRectangles = new();

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponent<RootComponent>();
        }

        public bool TestCollision(CollisionComponent other, out Vector2 outImpactDirection, Vector2? myLocation)
        {
            foreach (var collisionRectangle in this.GetCollisionRects(myLocation))
            {
                foreach (var otherRectangle in other.GetCollisionRects(null))
                {
                    // @todo: should impact direction be determined based on component hit or center?
                    // @fixme: this code sucks

                    var _collisionRectangle = collisionRectangle;
                    var _otherRectangle = otherRectangle;

                    if (TestCollisionSingle(ref _collisionRectangle, ref _otherRectangle, out outImpactDirection))
                    {
                        return true;
                    }
                }
            }

            outImpactDirection = new Vector2();

            return false;
        }

        public void AddHitbox(int x, int y, int width, int height)
        {
            AddHitbox(new Rectangle(x, y, width, height));
        }

        public void AddHitbox(Rectangle rectangle)
        {
            collisionRectangles.Add(rectangle);
        }

        private static bool TestCollisionSingle(ref Rectangle rect1, ref Rectangle rect2, out Vector2 outImpactDirection)
        {
            if (rect1.Intersects(rect2))
            {
                outImpactDirection = (rect1.Location - rect2.Location).ToVector2();
                outImpactDirection.Normalize();

                return true;
            }

            outImpactDirection = new Vector2();

            return false;
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            foreach (var rectangle in GetCollisionRects(null))
            {
                RectangleSprite.DrawRectangle(spriteBatch, rectangle, Color.Red, 3);
            }
        }

        public IEnumerable<Rectangle> GetCollisionRects(Vector2? myLocation)
        {
            var location = myLocation ?? rootComponent.Location;
            return collisionRectangles.Select(r => new Rectangle(
                r.X+(int)location.X, r.Y+(int)location.Y, r.Width, r.Height));
        }
    }
}
