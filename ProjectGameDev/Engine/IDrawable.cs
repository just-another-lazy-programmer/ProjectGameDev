using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal interface IDrawable
    {
        public DrawLayer DrawLayer { get; }
        public void Draw(SpriteBatch spriteBatch);
    }
}
