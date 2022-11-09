using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            component.SetOwner(this);
            //component.Activate();
        }

        protected static Texture2D LoadTexture(string texture)
        {
            // dependency injection? nah..
            return GlobalEngine.LoadTexture(texture);
        }

        public T CreateDefaultComponent<T>() where T : Component, new()
        {
            var component = new T();
            component.SetOwner(this);
            components.Add(component);
            return component;
        }

        public void ActivateComponents()
        {
            foreach (var component in components)
            {
                component.Activate();
            }
        }

        public T GetComponent<T>() where T : Component
        {
            return components.Find(p => p.GetType() == typeof(T)) as T;
        }

        public List<Component> GetComponents()
        {
            return components;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                if (component.IsActive && component.WantsTick)
                    component.Tick(gameTime);
            }
        }
    }
}
