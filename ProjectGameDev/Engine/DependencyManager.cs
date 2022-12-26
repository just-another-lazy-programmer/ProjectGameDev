using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class DependencyManager
    {
        private readonly Dictionary<Type, object> dependencies;

        // For default ctors so we can write less code
        public void RegisterDependency<T>() where T : new()
        {
            dependencies.Add(typeof(T), new T());
        }

        // For custom ctors inject the dependency
        public void RegisterDependency<T>(T dependency)
        {
            dependencies.Add(typeof(T), dependency);
        }

        // For the sake of providing a complete API
        // The generic version is preferred though
        public void RegisterDependency(object dependency)
        {
            dependencies.Add(dependency.GetType(), dependency);
        }

        // Gets or creates a new dependency
        public T GetDependency<T>() where T : new()
        {
            if (dependencies.TryGetValue(typeof(T), out object dependency))
            {
                return (T)dependency;
            }

            var newDependency = new T();
            dependencies.Add(typeof(T), newDependency);
            return newDependency;
        }

        // Gets a dependency or throws if it doesn't exist
        public T GetDependencyChecked<T>()
        {
            if (dependencies.TryGetValue(typeof(T), out object dependency))
            {
                return (T)dependency;
            }

            throw new Exception($"Dependency of type {typeof(T)} doesn't exist!");
        }
    }
}
