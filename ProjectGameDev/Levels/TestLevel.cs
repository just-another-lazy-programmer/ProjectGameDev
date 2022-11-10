using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Levels
{
    internal class TestLevel : Level
    {
        public override void Load(ContentManager contentManager)
        {
            AddObject(new Hero());
            AddObject(new DebugRectangle(new Vector2(0, 400), new Point(800, 100)));
            AddObject(new DebugRectangle(new Vector2(500, 350), new Point(50, 50)));
        }
    }
}
