using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class WorldObject
    {
        private readonly List<Component> components = new();

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.Activate();
        }

        public List<Component> GetComponents()
        {
            return components;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                if (component.IsActive)
                    component.Tick(gameTime);
            }
        }
    }
}
