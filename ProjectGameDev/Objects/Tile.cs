using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.Objects
{
    internal class Tile : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Tile;

        protected RootComponent rootComponent;
        protected CollisionComponent2 collisionComponent;

        protected Texture2D texture;
        protected Rectangle source;

        protected Point size;

        public Tile(
            DependencyManager dependencyManager, 
            Vector2 location, 
            Point size, 
            Texture2D texture,
            Rectangle source
            ) : base(dependencyManager)
        {
            rootComponent = CreateDefaultComponent<RootComponent>();
            collisionComponent = CreateDefaultComponent<CollisionComponent2>();

            rootComponent.Move(location);
            collisionComponent.AddHitbox(0, 0, size.X, size.Y);

            this.texture = texture;
            this.source = source;

            this.size = size;

            ActivateComponents();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(rootComponent.Location.ToPoint(), size),
                source,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                0
            );
        }
    }
}
