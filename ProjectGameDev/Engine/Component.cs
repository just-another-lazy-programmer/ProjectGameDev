using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    public class Component
    {
        public bool IsActive { get; private set; }
        public bool WantsTick { get; protected set; }
        public WorldObject Owner { get; protected set; }

        //public DependencyManager DependencyManager { protected get; init; }
        private bool dependenciesRegistered = false;

        public virtual void RegisterDependencies(DependencyManager dependencyManager)
        {
            if (dependenciesRegistered) throw new InvalidOperationException("Dependencies cannot be registered twice");
            dependenciesRegistered = true;
        }

        public virtual void Activate()
        {
            IsActive = true;
        }
        public virtual void Deactivate()
        {
            IsActive = false;
        }

        public virtual void SetOwner(WorldObject newOwner)
        {
            Owner = newOwner;
        }

        public virtual void Tick(GameTime gameTime) { }

    }
}
