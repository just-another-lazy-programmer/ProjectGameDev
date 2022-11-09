using Microsoft.Xna.Framework;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine;
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
        protected List<Rectangle> collisionRectangles;

        public bool TestCollision(CollisionComponent other, ref Vector2 outImpactDirection)
        {
            foreach (var collisionRectangle in collisionRectangles)
            {
                foreach (var otherRectangle in other.GetCollisionRects())
                {
                    // @todo: should impact direction be determined based on component hit or center?
                    // @fixme: this code sucks

                    var _collisionRectangle = collisionRectangle;
                    var _otherRectangle = otherRectangle;

                    if (TestCollisionSingle(ref _collisionRectangle, ref _otherRectangle, ref outImpactDirection))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool TestCollisionSingle(ref Rectangle rect1, ref Rectangle rect2, ref Vector2 outImpactDirection)
        {
            if (rect1.Intersects(rect2))
            {
                outImpactDirection = (rect1.Location - rect2.Location).ToVector2();
                outImpactDirection.Normalize();

                return true;
            }

            return false;
        }

        public List<Rectangle> GetCollisionRects()
        {
            return collisionRectangles;
        }
    }
}
