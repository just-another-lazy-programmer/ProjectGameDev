using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core
{
    internal class AnimationBuilder
    {
        private readonly Dictionary<Type, Animation> cachedAnimations = new();
        private readonly DependencyManager dependencyManager;

        public AnimationBuilder(DependencyManager dependencyManager)
        {
            this.dependencyManager = dependencyManager;
        }

        public Animation GetAnimation<T>() where T : IAnimationBuilder, new()
        {
            var type = typeof(T);

            if (cachedAnimations.ContainsKey(type))
                return cachedAnimations[type];

            var animation = new T().Build(dependencyManager);
            cachedAnimations[type] = animation;

            return animation;
        }

        public void PurgeCache()
        {
            cachedAnimations.Clear();
        }
    }
}
