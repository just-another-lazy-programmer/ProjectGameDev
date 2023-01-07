using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Objects
{
    internal class Background : WorldObject, Core.IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Background;
        private readonly ContentManager contentManager;
        private readonly Texture2D texture;
        private readonly GraphicsDevice graphicsDevice;

        private readonly Rectangle destination;

        public Background(DependencyManager dependencyManager, string texturePath) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref contentManager);
            dependencyManager.InjectChecked(ref graphicsDevice);

            destination = graphicsDevice.PresentationParameters.Bounds;

            texture = contentManager.Load<Texture2D>(texturePath);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destination, Color.White);
        }
    }
}
