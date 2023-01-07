using Microsoft.Xna.Framework;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ProjectGameDev.Objects
{
    internal class MovingPlatformComponent : Component
    {
        public double Speed { get; set; } = 1;
        public Vector2 Velocity = Vector2.Zero;
        public int LocationLeft { get; set; }
        public int LocationRight { get; set; }

        protected RootComponent rootComponent;
        protected bool isMovingRight;

        public MovingPlatformComponent()
        {
            WantsTick = true;
            isMovingRight = true;
        }

        public override void Activate()
        {
            base.Activate();

            rootComponent = Owner.GetComponentFast<RootComponent>();
        }

        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);

            var delta = gameTime.ElapsedGameTime.TotalSeconds * Speed;
            delta *= isMovingRight ? 1 : -1;

            var velocity = new Vector2((float)delta, 0);

            rootComponent.Location += velocity;
            var newX = rootComponent.Location.X;

            Velocity = velocity;

            if (isMovingRight && newX > LocationRight)
                isMovingRight = false;
            else if (!isMovingRight && newX < LocationLeft)
                isMovingRight = true;

            /*
            isMovingRight = (isMovingRight && newX > LocationRight) || 
                            (!isMovingRight && newX < LocationLeft);
            */
        }
    }
}
