using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core;
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
    /// <summary>
    /// This class is deprecated and should be removed in the future
    /// Keeping it in for now until we're sure none of its functionality is needed
    /// </summary>
    internal class CollisionComponent : Component
    {
        protected RootComponent rootComponent;
        protected List<Rectangle> collisionRectangles = new();
        protected Point lastImpactPoint = new Point(-1, -1);
        protected Rectangle lastIntersectingRect = new Rectangle();
        private SimpleSprites simpleSprites;

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.Inject(ref simpleSprites);
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponent<RootComponent>();
        }

        public bool TestCollision(CollisionComponent other, out HitInfo hitInfo, Vector2? myLocation)
        {
            foreach (var collisionRectangle in this.GetCollisionRects(myLocation))
            {
                foreach (var otherRectangle in other.GetCollisionRects(null))
                {
                    // @todo: should impact direction be determined based on component hit or center?
                    // @fixme: this code sucks

                    var _collisionRectangle = collisionRectangle;
                    var _otherRectangle = otherRectangle;

                    if (TestCollisionSingle(ref _collisionRectangle, ref _otherRectangle, out hitInfo))
                    {
                        hitInfo.ObjectHit = other.Owner;
                        hitInfo.ComponentHit = other;
                        hitInfo.ComponentLocation = other.Owner.GetComponent<RootComponent>()?.Location ?? Vector2.Zero;
                        hitInfo.ComponentSize = otherRectangle.Size.ToVector2();

                        return true;
                    }
                }
            }

            hitInfo = new HitInfo();

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

        private bool TestCollisionSingle(ref Rectangle rect1, ref Rectangle rect2, out HitInfo hitInfo)
        {
            hitInfo = new HitInfo();

            if (rect1.Intersects(rect2))
            {
                var intersectingRect = Rectangle.Intersect(rect1, rect2);
                lastIntersectingRect = intersectingRect;
                var impactNormal = (rect1.Location - rect2.Location).ToVector2();

                Vector2 impactPoint = lastIntersectingRect.Center.ToVector2();
                lastImpactPoint = lastIntersectingRect.Center;

                impactNormal.Normalize();

                hitInfo.ImpactPoint = impactPoint;
                hitInfo.ImpactNormal = impactNormal;
                hitInfo.OverlappingRect = intersectingRect;

                return true;
            }

            return false;
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            foreach (var rectangle in GetCollisionRects(null))
            {
                simpleSprites.DrawRectangleOutline(spriteBatch, rectangle, Color.Red, 3);
            }

            //if (lastImpactPoint != new Point(-1, -1))
            //    SimpleSprites.DrawPoint(spriteBatch, lastImpactPoint, Color.Yellow, 3);

            if (lastIntersectingRect != Rectangle.Empty)
            {
                //SimpleSprites.DrawRectangle(spriteBatch, lastIntersectingRect, Color.White);
                simpleSprites.DrawPoint(spriteBatch, lastIntersectingRect.Center, Color.White, 10);
            }
        }

        public IEnumerable<Rectangle> GetCollisionRects(Vector2? myLocation)
        {
            var location = myLocation ?? rootComponent.Location;
            return collisionRectangles.Select(r => new Rectangle(
                r.X+(int)location.X, r.Y+(int)location.Y, r.Width, r.Height));
        }
    }

    internal struct HitInfo
    {
        const int collisionMargin = 2;

        public WorldObject ObjectHit { get; set; }
        public Component ComponentHit { get; set; }
        public Vector2 ComponentLocation { get; set; }
        public Vector2 ComponentSize { get; set; }
        public Vector2 ImpactNormal { get; set; }
        public Vector2 ImpactPoint { get; set; }
        public Rectangle OverlappingRect { get; set; }

        public bool TopHit
        {
            get
            {
                return Math.Abs(ComponentLocation.Y - ImpactPoint.Y + collisionMargin) <= collisionMargin;
            }
        }

        public bool BottomHit
        {
            get
            {
                return Math.Abs(ComponentLocation.Y - ImpactPoint.Y + ComponentSize.Y - collisionMargin) <= collisionMargin;
            }
        }

        public bool LeftHit
        {
            get
            {
                return Math.Abs(ComponentLocation.X - ImpactPoint.X + collisionMargin) <= collisionMargin;
            }
        }

        public bool RightHit
        {
            get
            {
                return Math.Abs(ComponentLocation.X - ImpactPoint.X + ComponentSize.X - collisionMargin) <= collisionMargin;
            }
        }
    }
}
