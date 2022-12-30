using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class CollisionComponent2 : HitboxComponent
    {
        public bool ShouldTrigger { get; set; }
        protected World world;

        public CollisionComponent2() : base() { }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.InjectChecked(ref world);
        }

        public override void Activate()
        {
            base.Activate();
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

        public void TriggerObjects()
        {
            var objects = world.LoadedLevel.GetObjects();

            foreach (var obj in objects)
            {
                if (obj != Owner && obj.TryGetComponentFast(out TriggerComponent triggerComponent) && triggerComponent.IsActive)
                {
                    if (TestCollisionSingle(GetCollisionRects(null), triggerComponent))
                    {
                        triggerComponent.ProcessHitEvent(Owner);
                    }
                }
            }
        }

        public static bool TestCollisionSingle(IEnumerable<Rectangle> rectangles, HitboxComponent other)
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
    }
}
