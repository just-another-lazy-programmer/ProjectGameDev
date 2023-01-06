using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Animations.Undead
{
    internal class UndeadIdleAnimation : IAnimationBuilder
    {
        public int FramesPerSecond => 4;

        public string Texture => "Undead/idle";

        public bool Loop => true;

        public Animation Build(DependencyManager dependencyManager)
        {
            var animation = new Animation(
                FramesPerSecond,
                Loop,
                dependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>(Texture)
            );

            var builder = new Spritesheet();

            const int width = 100;
            const int height = 100;

            animation.AddFramesBatch(builder
                .SetSize(width, height)
                .Take(4)

                .ToList()
            );

            return animation;
        }
    }
}
