using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters
{
    internal class Hero : WorldObject, Engine.IDrawable
    {
        public MovementComponent CharacterMovement { get; protected set; }

        private readonly Texture2D texture;
        private readonly Animation animation;
        private const int fps = 15;
        private const double scale = 0.2;

        public Hero(Texture2D texture)
        {
            this.texture = texture;
            animation = new Animation(fps);

            animation.AddFramesBatch(new List<AnimationFrame>
            {
                new AnimationFrame(2940, 0, 319, 486),
                new AnimationFrame(2940, 486, 319, 486),
                new AnimationFrame(2940, 972, 319, 486),
                new AnimationFrame(0, 1458, 319, 486),
                new AnimationFrame(319, 1458, 319, 486),
                new AnimationFrame(638, 1458, 319, 486),
                new AnimationFrame(957, 1458, 319, 486),
                new AnimationFrame(1276, 1458, 319, 486),
                new AnimationFrame(1595, 1458, 319, 486),
                new AnimationFrame(1914, 1458, 319, 486),
            });

            CharacterMovement = new MovementComponent();
            AddComponent(CharacterMovement);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = animation.CurrentFrame.SourceRectangle;
            spriteBatch.Draw(texture, new Rectangle(0, 0, (int)(source.Width*scale), (int)(source.Height*scale)), source, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
    }
}
