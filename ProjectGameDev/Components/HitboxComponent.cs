using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    public class HitboxComponent : Component
    {
        public Hitbox Hitbox { get; set; }

        protected SimpleSprites simpleSprites;
        protected RootComponent rootComponent;

        public HitboxComponent()
        {
            Hitbox = new Hitbox();
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponent<RootComponent>();
        }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.Inject(ref simpleSprites);
        }

        public void AddHitbox(int x, int y, int width, int height)
        {
            AddHitbox(new Rectangle(x, y, width, height));
        }

        public void AddHitbox(Rectangle rectangle)
        {
            Hitbox.Rectangles.Add(rectangle);
        }

        public IEnumerable<Rectangle> GetCollisionRects(Vector2? myLocation = null)
        {
            var location = myLocation ?? rootComponent.Location;
            return Hitbox.Rectangles.Select(r => new Rectangle(
                r.X + (int)location.X, r.Y + (int)location.Y, r.Width, r.Height));
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            foreach (var rectangle in GetCollisionRects(null))
            {
                simpleSprites.DrawRectangleOutline(spriteBatch, rectangle, Color.Red, 3);
            }
        }
    }

    public struct Hitbox
    {
        public List<Rectangle> Rectangles { get; set; }

        public Hitbox()
        {
            Rectangles = new();
        }
    }
}
