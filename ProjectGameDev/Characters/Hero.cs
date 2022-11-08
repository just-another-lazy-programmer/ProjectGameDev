using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Hero;
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
        public MovementComponent MyCharacterMovement { get; protected set; }
        public AnimationComponent MyAnimationComponent { get; protected set; }

        private const int fps = 15;
        private const double scale = 0.2;

        public Hero(Texture2D texture)
        {
            MyCharacterMovement = new MovementComponent();
            MyAnimationComponent = new AnimationComponent(texture, AnimationBuilder.GetAnimation<HeroIdleAnimation>());

            AddComponent(MyCharacterMovement);
            AddComponent(MyAnimationComponent);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                MyAnimationComponent.GetTexure(),
                new Rectangle(new Point(100, 0), MyAnimationComponent.GetAnimationBoundsScaled(scale)),
                MyAnimationComponent.GetAnimationFrame(),
                Color.White
            );
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
