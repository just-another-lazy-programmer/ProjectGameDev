using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Hero
{
    internal class HeroJumpingAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 10;
        public bool Loop => false;

        public Animation Build()
        {
            var animation = new Animation(FramesPerSecond, Loop);
            var builder = new Spritesheet();

            animation.AddFramesBatch(builder
                .SetPosition(2233, 1200)
                .SetSize(407, 536)
                .Take(1)

                .SetPosition(2640, 1458)
                .Take(1)

                .SetPosition(0, 1994)
                .Take(8)

                .ToList()
            );

            return animation;
        }
    }
}
