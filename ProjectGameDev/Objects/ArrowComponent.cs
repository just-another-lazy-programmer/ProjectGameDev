using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Objects
{
    internal class ArrowComponent : Component
    {
        public Vector2 Offset { get; set; }

        protected ContentManager contentManager;
        protected RootComponent rootComponent;
        protected Vector2 animationOffset;
        protected Sprite sprite;
        protected Texture2D texture;
        protected Point size;

        private double counter = 0;
        
        public ArrowComponent()
        {
            WantsTick = true;

            const int width = 32;
            const int height = 32;

            sprite = new Sprite(0, height * 2, width, height);
            size = new Point(50, 50);
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponentFast<RootComponent>();
        }

        public override void RegisterDependencies(DependencyManager dependencyManager)
        {
            base.RegisterDependencies(dependencyManager);

            dependencyManager.InjectChecked(ref contentManager);
            texture = contentManager.Load<Texture2D>("Misc/icons_0");
        }

        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);

            counter += gameTime.ElapsedGameTime.TotalSeconds;
            animationOffset = new Vector2(0, (float)Math.Sin(counter)*10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive) return;

            var destinationRectangle = new Rectangle(
                (rootComponent.Location + Offset + animationOffset).ToPoint(),
                size
            );
            spriteBatch.Draw(
                texture,
                destinationRectangle,
                sprite.SourceRectangle,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.FlipVertically,
                0
            );
        }
    }
}
