using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.UI.Elements
{
    internal class TextLabel : UIElement, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.MenuFront;
        protected RootComponent rootComponent;
        protected ContentManager contentManager;
        protected SpriteFont spriteFont;

        public TextLabel(DependencyManager dependencyManager, Vector2 location) : base(dependencyManager)
        {
            rootComponent = CreateDefaultComponent<RootComponent>();
            rootComponent.Location = location;

            dependencyManager.InjectChecked(ref contentManager);
            spriteFont = contentManager.Load<SpriteFont>("BasicFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Hello world!", rootComponent.Location, Color.White);
        }
    }
}
