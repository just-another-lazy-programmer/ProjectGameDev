using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Characters;
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
        }
    }
}
