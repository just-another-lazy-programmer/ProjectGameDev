using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Slime
{
    internal class OrangeSlimeIdleAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 12;

        public string Texture => "orange_slime";

        public bool Loop => true;

        public Animation Build(DependencyManager dependencyManager)
        {
            var animation = new Animation(
                FramesPerSecond,
                Loop,
                dependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>(Texture)
            );

            var builder = new Spritesheet();

            const int width = 510;
            const int height = 410;

            animation.AddFramesBatch(builder
                .SetRowSize(4 * width)
                .SetSize(width, height)
                .Take(24)

                .SetPosition(2040, 0)
                .TakeVertically(6)

                .ToList()
            );

            return animation;
        }
    }
}
