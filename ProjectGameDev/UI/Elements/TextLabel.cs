using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Core;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.UI.Elements
{
    public class TextLabel : UIElement, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.MenuFront;

        protected RootComponent rootComponent;
        protected ContentManager contentManager;
        protected SpriteFont spriteFont;
        protected string text;
        protected Color color;
        protected HorizontalTextAlignment horizontalAlignment;
        protected VerticalTextAlignment verticalAlignment;
        protected Vector2 offsetForAlignment;

        public TextLabel(
            DependencyManager dependencyManager, 
            Vector2 location, 
            string text,
            HorizontalTextAlignment horizontalAlignment = HorizontalTextAlignment.Left,
            VerticalTextAlignment verticalAlignment = VerticalTextAlignment.Top,
            string font="BasicFont",
            Color? color=null) : base(dependencyManager)
        {
            rootComponent = CreateDefaultComponent<RootComponent>();
            rootComponent.Location = location;

            dependencyManager.InjectChecked(ref contentManager);
            spriteFont = contentManager.Load<SpriteFont>(font);

            this.color = color ?? Color.White;
            this.text = text;

            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;

            CalculateOffsetForAlignment();
        }

        private void CalculateOffsetForAlignment()
        {
            var size = GetActualSize();
            float offsetX = horizontalAlignment switch
            {
                HorizontalTextAlignment.Left => 0,
                HorizontalTextAlignment.Center => -size.X / 2f,
                HorizontalTextAlignment.Right => -size.X,
                _ => throw new ArgumentOutOfRangeException($"Horizontal alignment type {horizontalAlignment} does not exist!")
            };

            float offsetY = verticalAlignment switch
            {
                VerticalTextAlignment.Top => 0,
                VerticalTextAlignment.Center => -size.Y / 2f,
                VerticalTextAlignment.Bottom => -size.Y,
                _ => throw new ArgumentOutOfRangeException($"Vertical alignment type {verticalAlignment} does not exist!")
            };

            offsetForAlignment = new Vector2(offsetX, offsetY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, rootComponent.Location+offsetForAlignment, color);
        }

        public Vector2 GetActualSize()
        {
            return spriteFont.MeasureString(text);
        }
    }

    public enum HorizontalTextAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalTextAlignment
    {
        Top,
        Center,
        Bottom
    }
}
