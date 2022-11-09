using Microsoft.Xna.Framework;
using ProjectGameDev.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Components
{
    internal class RootComponent : Component
    {
        public Vector2 Location { get; set; }

        public void Move(Vector2 location)
        {
            Location = location;
        }
    }
}
