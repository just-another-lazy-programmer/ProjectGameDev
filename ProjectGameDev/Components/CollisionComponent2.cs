using Microsoft.Xna.Framework;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class CollisionComponent2 : Component
    {
        public Hitbox Hitbox { get; set; }
        protected RootComponent rootComponent;
        protected World world;

        public CollisionComponent2()
        {
            Hitbox = new Hitbox();
        }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.InjectChecked(ref world);
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponent<RootComponent>();

        }

        public WorldObject TestCollision(Vector2 location)
        {
            var objects = world.LoadedLevel.GetObjects();

            foreach (var obj in objects)
            {
                if (obj != Owner && obj.TryGetComponentFast(out CollisionComponent2 collisionComponent) && collisionComponent.IsActive)
                {
                    if (TestCollisionSingle(GetCollisionRects(location), collisionComponent))
                        return obj;
                }
            }

            return null;
        }

        public bool TestCollisionSingle(IEnumerable<Rectangle> rectangles, CollisionComponent2 other)
        {
            foreach (var rect in other.GetCollisionRects())
            {
                foreach (var part in rectangles)
                {
                    if (part.Intersects(rect))
                        return true;
                }
            }

            return false;
        }

        public void AddHitbox(int x, int y, int width, int height)
        {
            AddHitbox(new Rectangle(x, y, width, height));
        }

        public void AddHitbox(Rectangle rectangle)
        {
            Hitbox.Rectangles.Add(rectangle);
        }

        public IEnumerable<Rectangle> GetCollisionRects(Vector2? myLocation=null)
        {
            var location = myLocation ?? rootComponent.Location;
            return Hitbox.Rectangles.Select(r => new Rectangle(
                r.X+(int)location.X, r.Y+(int)location.Y, r.Width, r.Height));
        }
    }

    internal struct Hitbox
    {
        public List<Rectangle> Rectangles { get; set; }

        public Hitbox()
        {
            Rectangles = new();
        }
    }
}
