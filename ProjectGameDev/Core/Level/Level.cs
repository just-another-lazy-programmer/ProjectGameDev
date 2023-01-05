﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Core;
using ProjectGameDev.UI.Core;

namespace ProjectGameDev.Core.Level
{
    public class Level
    {
        protected List<WorldObject> objects = new();

        protected readonly DependencyManager dependencyManager;

        public Level(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
        }

        public virtual void Load()
        {
        }

        public DependencyManager GetDependencyManager()
        {
            return dependencyManager;
        }

        public void AddObject(WorldObject worldObject)
        {
            objects.Add(worldObject);
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
