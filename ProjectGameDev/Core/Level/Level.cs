using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Core;
using SharpDX.MediaFoundation.DirectX;

namespace ProjectGameDev.Core.Level
{
    public abstract class Level
    {
        protected List<WorldObject> objects = new();

        protected List<WorldObject> pendingAdd = new();
        protected List<WorldObject> pendingRemove = new();

        protected bool locked = false;

        protected readonly DependencyManager dependencyManager;

        public Level(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
        }

        public virtual void Load()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            locked = true;
            foreach (var worldObject in GetObjects())
            {
                worldObject.Update(gameTime);
            }
            locked = false;

            ResolvePending();
        }

        public DependencyManager GetDependencyManager()
        {
            return dependencyManager;
        }

        public void AddObject(WorldObject worldObject)
        {
            objects.Add(worldObject);
        }

        private void ResolvePending()
        {
            if (pendingAdd.Count > 0)
            {
                objects.AddRange(pendingAdd);
                pendingAdd.Clear();
            }

            if (pendingRemove.Count > 0)
            {
                objects = objects.Except(pendingRemove).ToList();
                pendingRemove.Clear();
            }
        }

        public void RemoveObject(WorldObject worldObject)
        {
            if (!locked)
                objects.Remove(worldObject);
            else
                pendingRemove.Add(worldObject);
        }

        public void AddObjectSafe(WorldObject worldObject)
        {
            if (!locked)
                objects.Add(worldObject);
            else
                pendingAdd.Add(worldObject);
        }

        public T GetObject<T>()
        {
            return objects.OfType<T>().Take(1).SingleOrDefault();
        }

        public List<WorldObject> GetObjects()
        {
            return objects;
        }
    }
}
