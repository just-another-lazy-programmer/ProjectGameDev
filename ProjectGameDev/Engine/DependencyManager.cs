using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectGameDev.Utility;

namespace ProjectGameDev.Engine
{
    /// <summary>
    /// A simple dependency manager so we only need to pass 1 class to a ctor
    /// Classes can choose which dependencies to obtain and which dependencies to add
    /// Dependencies must be unique per Type, only one dependency of each class may exist at a time
    /// Dependencies share their state between classes
    /// </summary>
    internal class DependencyManager
    {
        private readonly Dictionary<Type, object> dependencies = new();

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

        // Gets a dependency or creates one if it doesn't exist
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

        // Even more alternative syntax so we can directly inject into a field
        // Doesn't work with properties though since we can't access those with ref
        public void InjectChecked<T>(ref T dependencyProperty)
        {
            if (dependencies.TryGetValue(typeof(T), out object dependency))
                dependencyProperty = (T)dependency;
            else
                throw new Exception($"Dependency of type {typeof(T)} doesn't exist!");
        }
    }
}
