using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }

        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }

        public AnimationFrame(int x, int y, int width, int height)
        {
            SourceRectangle = new Rectangle(x, y, width, height);
        }
    }
}
