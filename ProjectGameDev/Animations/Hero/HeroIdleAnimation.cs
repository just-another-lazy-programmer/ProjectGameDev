using ProjectGameDev.Core;
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

        public bool Loop { get; } = true;

        public string Texture => "hero";

        public Animation Build(DependencyManager _)
        {
            var animation = new Animation(FramesPerSecond, Loop, null);
            var builder = new Spritesheet();

            /*
            animation.AddFramesBatch(new List<Sprite>
            {
                new Sprite(2940, 0, 319, 486),
                new Sprite(2940, 486, 319, 486),
                new Sprite(2940, 972, 319, 486),
                new Sprite(0, 1458, 319, 486),
                new Sprite(319, 1458, 319, 486),
                new Sprite(638, 1458, 319, 486),
                new Sprite(957, 1458, 319, 486),
                new Sprite(1276, 1458, 319, 486),
                new Sprite(1595, 1458, 319, 486),
                new Sprite(1914, 1458, 319, 486),
            });
            */

            // 3 vertically then 7 horizontally, wtf?
            animation.AddFramesBatch(builder
                .SetPosition(2940, 0)
                .SetSize(319, 486)
                .TakeVertically(3)

                .SetPosition(0, 1458)
                .Take(7)

                .ToList()
            );


            return animation;
        }
    }
}
