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
    internal class Hero2JumpingAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 10;

        public string Texture => "hero_jumping";

        public bool Loop => false;

        public Animation Build(DependencyManager dependencyManager)
        {
            var animation = new Animation(
                FramesPerSecond,
                Loop,
                dependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>(Texture)
            );

            var builder = new Spritesheet();

            animation.AddFramesBatch(builder
                .SetSize(512, 512)
                .Take(7)

                .ToList()
            );

            return animation;
        }
    }
}
