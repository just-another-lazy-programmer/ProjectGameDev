using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Core;
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
        public event EventHandler OnAnimationFinishedEvent;
        protected Animation currentAnimation;
        protected bool shouldFlip;

        protected RootComponent rootComponent;

        public AnimationComponent()
        {
            WantsTick = true;
            shouldFlip = false;
        }

        public AnimationComponent(Animation animation) : this()
        {
            currentAnimation = animation;
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

        public virtual void Draw(SpriteBatch spriteBatch, double scale, Color? color=null)
        {
            if (!color.HasValue)
                color = Color.White;

            spriteBatch.Draw(
                currentAnimation.GetTexture(),
                new Rectangle(rootComponent.Location.ToPoint(), GetAnimationBoundsScaled(scale)),
                GetAnimationFrame(),
                color.Value,
                0,
                Vector2.Zero,
                GetSpriteEffects(),
                0
            );
        }

        public virtual void SetAnimation(Animation animation)
        {
            currentAnimation = animation;
            currentAnimation.Play();
        }

        public virtual Rectangle GetAnimationFrame()
        {
            return currentAnimation.CurrentFrame.SourceRectangle;
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

            if (currentAnimation.Finished)
            {
                OnAnimationFinishedEvent?.Invoke(this, new EventArgs());
            }
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
