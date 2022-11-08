using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public IPhysics Target { get; protected set; }

        protected Animation currentAnimation;
        protected Texture2D currentTexture;
        protected bool shouldFlip;

        public AnimationComponent(IPhysics target)
        {
            WantsTick = true;
            shouldFlip = false;
            Target = target;
        }

        public AnimationComponent(IPhysics target, Texture2D texture, Animation animation) : this(target)
        {
            currentAnimation = animation;
            currentTexture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch, double scale)
        {
            spriteBatch.Draw(
                currentTexture,
                new Rectangle(Target.PhysicsComponent.Location.ToPoint(), GetAnimationBoundsScaled(scale)),
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
            currentAnimation.Update(gameTime);

            var velocity = Target.PhysicsComponent.Velocity;

            if (velocity.X > 0 && shouldFlip)
                shouldFlip = false;
            else if (velocity.X < 0 && !shouldFlip)
                shouldFlip = true;

            base.Tick(gameTime);
        }

        public SpriteEffects GetSpriteEffects()
        {
            return shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
