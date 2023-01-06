using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Level.Model;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.Objects
{
    internal class Platform : WorldObject, IDrawable
    {
        protected RootComponent rootComponent;
        protected CollisionComponent2 collisionComponent;

        protected Texture2D texture;
        protected Rectangle source;

        protected Point size;

        // @TODO: move scalefactor?
        protected const double scaleFactor = 1/16d;
        private const int tilesize = 512;

        public Platform(DependencyManager dependencyManager, Vector2 location) : base(dependencyManager)
        {
            texture = dependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>("MossyTileSet");

            rootComponent = CreateDefaultComponent<RootComponent>();
            collisionComponent = CreateDefaultComponent<CollisionComponent2>();

            // Takes 3 tiles (GIDs 22-24) from tileset
            var textureLocation = TextureUtils.GetLocationInSetFromGID(22, 1, tilesize, 7);
            var textureSize = new Point(tilesize * 3, tilesize);

            source = new Rectangle(textureLocation, textureSize);
            rootComponent.Move(location);

            size = new Point((int)(textureSize.X * scaleFactor), (int)(textureSize.Y * scaleFactor));

            // To explain these 'magic' numbers, the 'platform' consists of 3 tiles in which the first and last are only half-length
            // so we offset the start of the hitbox by the length of half a tile to compensate
            var collisionSize = new Point((int)((tilesize * 2) * scaleFactor), (int)(tilesize * scaleFactor));
            collisionComponent.AddHitbox(new Rectangle(new Point((int)((tilesize / 2)*scaleFactor), 0), collisionSize));

            ActivateComponents();
        }

        public DrawLayer DrawLayer => DrawLayer.MovingObjects;

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
