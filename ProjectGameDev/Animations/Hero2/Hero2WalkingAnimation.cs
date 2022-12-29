using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Hero2
{
    internal class Hero2WalkingAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 15;

        public bool Loop => true;

        public string Texture => "hero_walking";

        public Animation Build(DependencyManager dependencyManager)
        {
            var animation = new Animation(
                FramesPerSecond,
                Loop,
                dependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>(Texture)
            );

            var builder = new Spritesheet();

            animation.AddFramesBatch(builder
                .SetRowSize(4 * 512)
                .SetSize(512, 512)
                .Take(16)

                .SetPosition(2048, 0)
                .TakeVertically(4)

                .ToList()
            );

            return animation;
        }
    }
}
