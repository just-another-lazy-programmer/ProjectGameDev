using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDrawable = ProjectGameDev.Core.IDrawable;

namespace ProjectGameDev.Objects
{
    internal class DestinationPlatform : Platform, IDrawable
    {
        public TriggerComponent TriggerComponent { get; private set; }
        protected readonly ArrowComponent arrowComponent;

        public DestinationPlatform(DependencyManager dependencyManager, Vector2 location) : base(dependencyManager, location)
        {
            arrowComponent = CreateDefaultComponent<ArrowComponent>();
            arrowComponent.Offset = new Vector2(20, -60);

            TriggerComponent = CreateDefaultComponent<TriggerComponent>();
            TriggerComponent.AddHitbox(20, -10, 50, 20);

            ActivateComponents();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            arrowComponent.Draw(spriteBatch);
            //TriggerComponent.DebugDraw(spriteBatch);
        }
    }
}
