using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Utility;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class AnimationComponent : Component
    {
        protected Animation currentAnimation;
        protected Texture2D currentTexture;

        public AnimationComponent()
        {
            WantsTick = true;
        }

        public AnimationComponent(Texture2D texture, Animation animation)
        {
            WantsTick = true;

            currentAnimation = animation;
            currentTexture = texture;
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
            base.Tick(gameTime);
        }
    }
}
