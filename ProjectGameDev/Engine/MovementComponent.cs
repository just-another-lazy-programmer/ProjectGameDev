using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class MovementComponent : Component
    {
        public MovementComponent()
        {
            WantsTick = true;
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Tick(GameTime gameTime)
        {
            
        }
    }
}
