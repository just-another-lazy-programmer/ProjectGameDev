using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.UI.Elements
{
    internal class HealthBar : UIElement, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.MenuFront;
        public RootComponent RootComponent { get; set; }

        protected SimpleSprites simpleSprites;
        protected Point size;

        protected Vector2 offset;
        protected Point innerSize;

        protected float maxHealth;
        protected float health;

        protected Color backgroundColor;
        protected Color fillColor;

        public HealthBar(
            DependencyManager dependencyManager,
            Vector2 location,
            Point size,
            float maxHealth,
            Color? backgroundColor=null,
            Color? fillColor=null
        ) : base(dependencyManager)
        {
            dependencyManager.Inject(ref simpleSprites);

            RootComponent = CreateDefaultComponent<RootComponent>();
            RootComponent.Move(location);

            var margin = 2;

            offset = new Vector2(margin/2, margin/2);
            innerSize = new Point(size.X - margin, size.Y - margin);

            if (!backgroundColor.HasValue) backgroundColor = Color.DarkGray;
            if (!fillColor.HasValue) fillColor = Color.OrangeRed;

            this.backgroundColor = backgroundColor.Value;
            this.fillColor = fillColor.Value;

            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.size = size;
        }

        public void SetHealth(float health)
        {
            this.health = health;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var backgroundBar = new Rectangle(RootComponent.Location.ToPoint(), size);
            var fill = (int)(innerSize.X * (health/maxHealth));
            var bar = new Rectangle((RootComponent.Location + offset).ToPoint(), new Point(fill, innerSize.Y));
            simpleSprites.DrawRectangle(spriteBatch, backgroundBar, this.backgroundColor);
            simpleSprites.DrawRectangle(spriteBatch, bar, this.fillColor);
        }
    }
}
