using ProjectGameDev.ComponentInterfaces;
using ProjectGameDev.Components;
using ProjectGameDev.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Objects
{
    internal class DebugRectangle : WorldObject, ICollision
    {
        public CollisionComponent CollisionComponent { get; protected set; }

        public DebugRectangle()
        {
            CollisionComponent = CreateDefaultComponent<CollisionComponent>();
        }
    }
}
