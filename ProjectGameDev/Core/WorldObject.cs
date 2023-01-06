using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core
{
    public abstract class WorldObject
    {
        private readonly List<Component> components = new();
        private readonly Dictionary<Type, Component> defaultComponents = new();

        protected DependencyManager DependencyManager { get; private set; }

        public WorldObject(DependencyManager dependencyManager)
        {
            DependencyManager = dependencyManager;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.SetOwner(this);
            //component.Activate();
        }

        protected Texture2D LoadTexture(string texture)
        {
            //return GlobalEngine.LoadTexture(texture);
            return DependencyManager.GetDependencyChecked<ContentManager>().Load<Texture2D>(texture);
        }

        public T CreateDefaultComponent<T>() where T : Component, new()
        {
            var component = new T();
            component.SetOwner(this);
            components.Add(component);
            component.RegisterDependencies(DependencyManager);
            defaultComponents.Add(typeof(T), component);
            return component;
        }

        public void ActivateComponents()
        {
            foreach (var component in components)
            {
                if (!component.IsActive)
                    component.Activate();
            }
        }

        public T GetComponent<T>() where T : Component
        {
            return components.Find(p => p.GetType() == typeof(T)) as T;
        }

        public bool HasComponentFast<T>() where T : Component
        {
            return defaultComponents.ContainsKey(typeof(T));
        }

        public bool TryGetComponentFast<T>(out T component) where T : Component
        {
            component = null;

            if (defaultComponents.TryGetValue(typeof(T), out Component temp))
            {
                component = (T)temp;
                return true;
            }

            return false;
        }

        // Gets component from Dictionary, only works for default components!
        // Does not check whether component exists!

        public T GetComponentFast<T>() where T : Component
        {
            return (T)defaultComponents[typeof(T)];
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
