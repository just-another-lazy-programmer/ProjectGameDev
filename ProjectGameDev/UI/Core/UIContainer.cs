using Microsoft.Xna.Framework;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.UI.Core
{
    public class UIContainer
    {
        protected List<UIElement> elements = new();
        protected readonly DependencyManager dependencyManager;

        public UIContainer(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
        }

        public DependencyManager GetDependencyManager()
        {
            return dependencyManager;
        }

        public void AddElement(UIElement element)
        {
            elements.Add(element);
        }

        public virtual void Load() { }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var element in GetElements())
            {
                element.Update(gameTime);
            }
        }

        public List<UIElement> GetElements()
        {
            return elements;
        }
    }
}
