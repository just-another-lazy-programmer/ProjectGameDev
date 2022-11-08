using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class Spritesheet
    {
        protected Texture2D texture;
        protected int zIndex;
        protected SpriteBatch spriteBatch;

        public Spritesheet(Texture2D texture, SpriteBatch spriteBatch, int zIndex)
        {
            this.texture = texture;
            this.zIndex = zIndex;
            this.spriteBatch = spriteBatch;
        }
    }
}
