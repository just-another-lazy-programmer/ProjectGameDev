using Microsoft.Xna.Framework;
using ProjectGameDev.UI.Core;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Components;
using ProjectGameDev.Utility;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ProjectGameDev.UI.Components;

namespace ProjectGameDev.UI.Elements
{
    public class Button : UIElement, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.MenuFront;
        protected RootComponent rootComponent;
        protected ContentManager contentManager;
        protected TextLabel innerText;
        protected SimpleSprites simpleSprites;
        protected HorizontalAlignment horizontalAlignment;
        protected VerticalAlignment verticalAlignment;
        protected Vector2 offsetForAlignment;
        protected Color color;
        protected Point size;
        public MouseComponent MouseComponent { get; set; }

        public Button(
            DependencyManager dependencyManager,
            Vector2 location,
            Point size,
            string text,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment verticalAlignment = VerticalAlignment.Top,
            string font = "BasicFont",
            Color? backgroundColor = null,
            Color? textColor = null
        ) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref contentManager);
            dependencyManager.Inject(ref simpleSprites);

            if (!backgroundColor.HasValue)
                backgroundColor = Color.White;

            rootComponent = CreateDefaultComponent<RootComponent>();
            rootComponent.Location = location;

            innerText = new TextLabel(
                dependencyManager,
                rootComponent.Location,
                text,
                HorizontalTextAlignment.Center,
                VerticalTextAlignment.Center,
                font,
                textColor
            );


            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            this.color = backgroundColor.Value;
            this.size = size;

            CalculateOffsetForAlignment();

            MouseComponent = CreateDefaultComponent<MouseComponent>();
            MouseComponent.AddHitbox(new Rectangle((location + offsetForAlignment).ToPoint(), size));

            MouseComponent.OnHoverStartEvent += MouseComponent_OnHoverStartEvent;
            MouseComponent.OnHoverEndEvent += MouseComponent_OnHoverEndEvent;

            ActivateComponents();
        }

        private void MouseComponent_OnHoverEndEvent(object sender, EventArgs e)
        {
            color = Color.White;
        }

        private void MouseComponent_OnHoverStartEvent(object sender, EventArgs e)
        {
            color = Color.Red;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            simpleSprites.DrawRectangle(spriteBatch, new Rectangle((rootComponent.Location+offsetForAlignment).ToPoint(), size), color);
            innerText.Draw(spriteBatch);
        }

        public Vector2 GetActualSize()
        {
            return size.ToVector2();
        }

        private void CalculateOffsetForAlignment()
        {
            var size = GetActualSize();
            float offsetX = horizontalAlignment switch
            {
                HorizontalAlignment.Left => 0,
                HorizontalAlignment.Center => -size.X / 2f,
                HorizontalAlignment.Right => -size.X,
                _ => throw new ArgumentOutOfRangeException($"Horizontal alignment type {horizontalAlignment} does not exist!")
            };

            float offsetY = verticalAlignment switch
            {
                VerticalAlignment.Top => 0,
                VerticalAlignment.Center => -size.Y / 2f,
                VerticalAlignment.Bottom => -size.Y,
                _ => throw new ArgumentOutOfRangeException($"Vertical alignment type {verticalAlignment} does not exist!")
            };

            offsetForAlignment = new Vector2(offsetX, offsetY);
        }
    }


    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }
}
