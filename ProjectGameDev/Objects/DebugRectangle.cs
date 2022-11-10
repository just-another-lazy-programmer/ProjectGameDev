using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Objects
{
    internal class DebugRectangle : WorldObject, ICollision, Engine.IDrawable
    {
        public RootComponent RootComponent { get; protected set; }
        public CollisionComponent CollisionComponent { get; protected set; }
        protected Color Color { get; set; }

        public DrawLayer DrawLayer => DrawLayer.Debug;

        protected Texture2D texture;
        protected Point size;

        public DebugRectangle(Vector2 location, Point size)
        {
            RootComponent = CreateDefaultComponent<RootComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent>();

            texture = new Texture2D(GlobalEngine.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
            Color = Color.GreenYellow;

            ActivateComponents();

            CollisionComponent.AddHitbox(0, 0, size.X, size.Y);
            //RootComponent.Move(new Vector2(50, 400));
            RootComponent.Move(location);

            this.size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)RootComponent.Location.X, (int)RootComponent.Location.Y, size.X, size.Y), Color);
            CollisionComponent.DebugDraw(spriteBatch);
        }
    }
}
