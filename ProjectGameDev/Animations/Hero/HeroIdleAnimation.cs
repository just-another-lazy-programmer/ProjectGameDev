using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Hero
{
    internal class HeroIdleAnimation : IAnimationBuilder
    {
        public int FramesPerSecond { get; } = 15;

        public Animation Build()
        {
            var animation = new Animation(FramesPerSecond);

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

            return animation;
        }
    }
}
