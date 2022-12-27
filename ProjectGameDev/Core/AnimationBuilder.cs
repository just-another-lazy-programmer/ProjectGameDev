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

        public Animation GetAnimation<T>() where T : IAnimationBuilder, new()
        {
            var type = typeof(T);

            if (cachedAnimations.ContainsKey(type))
                return cachedAnimations[type];

            var animation = new T().Build();
            cachedAnimations[type] = animation;

            return animation;
        }

        public void PurgeCache()
        {
            cachedAnimations.Clear();
        }
    }
}
