using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class AnimationComponent : Component
    {
        protected Animation currentAnimation;
        protected Texture2D currentTexture;
        protected bool shouldFlip;

        protected RootComponent rootComponent;

        public AnimationComponent()
        {
            WantsTick = true;
            shouldFlip = false;
        }

        public AnimationComponent(Texture2D texture, Animation animation) : this()
        {
            currentAnimation = animation;
            currentTexture = texture;
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponent<RootComponent>();
        }

        public void SetFlip(bool newFlip)
        {
            shouldFlip = newFlip;
        }

        public virtual void Draw(SpriteBatch spriteBatch, double scale)
        {
            spriteBatch.Draw(
                currentTexture,
                new Rectangle(rootComponent.Location.ToPoint(), GetAnimationBoundsScaled(scale)),
                GetAnimationFrame(),
                Color.White,
                0,
                Vector2.Zero,
                GetSpriteEffects(),
                0
            );
        }

        public virtual void SetAnimation(Animation animation)
        {
            currentAnimation = animation;
        }

        public virtual void SetTexture(Texture2D texture)
        {
            currentTexture = texture;
        }

        public virtual Rectangle GetAnimationFrame()
        {
            return currentAnimation.CurrentFrame.SourceRectangle;
        }

        public virtual Texture2D GetTexure()
        {
            return currentTexture;
        }

        public virtual Point GetAnimationBoundsScaled(double scaleFactor)
        {
            var source = GetAnimationFrame();
            return new Point((int)(source.Width * scaleFactor), (int)(source.Height * scaleFactor));
        }

        public override void Tick(GameTime gameTime)
        {
            // @TODO: refactor!
            

            currentAnimation.Update(gameTime);
            /*
            var velocity = physicsComponent.Velocity;

            if (velocity.X > 0 && shouldFlip)
                shouldFlip = false;
            else if (velocity.X < 0 && !shouldFlip)
                shouldFlip = true;
            */

            base.Tick(gameTime);
        }

        public SpriteEffects GetSpriteEffects()
        {
            return shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
