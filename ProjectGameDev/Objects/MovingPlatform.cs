using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Level.Model;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.Objects
{
    internal class MovingPlatform : Platform, IDrawable
    {
        protected MovingPlatformComponent movementComponent;

        public MovingPlatform(DependencyManager dependencyManager, Vector2 location, int speed, int reach) : base(dependencyManager, location)
        {
            movementComponent = CreateDefaultComponent<MovingPlatformComponent>();
            movementComponent.LocationLeft = (int)location.X - reach;
            movementComponent.LocationRight = (int)location.X + reach;
            movementComponent.Speed = speed;

            ActivateComponents();
        }
    }
}
