using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGameDev.Components;

namespace ProjectGameDev.Levels
{
    internal class TestLevel : Level
    {
        public override void Load(ContentManager contentManager)
        {
            AddObject(new Hero());
            //AddObject(new Hero());
            //objects[1].GetComponent<MovementComponent>().Teleport(new Vector2(40, 40));
            AddObject(new DebugRectangle(new Vector2(0, 400), new Point(800, 100)));
            AddObject(new DebugRectangle(new Vector2(500, 350), new Point(50, 50)));
            AddObject(new DebugRectangle(new Vector2(300, 250), new Point(100, 50)));
        }
    }
}
