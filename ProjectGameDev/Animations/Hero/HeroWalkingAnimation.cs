using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Hero
{
    internal class HeroWalkingAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 15;

        public bool Loop => true;

        public Animation Build()
        {
            var animation = new Animation(FramesPerSecond, Loop);
            var builder = new Spritesheet();

            animation.AddFramesBatch(builder
                .SetPosition(0, 2530)
                .SetSize(415, 507)
                .Take(7)

                .SetPosition(3259, 0)
                .TakeVertically(3)
                .ToList()
            );

            return animation;
        }
    }
}
