using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class AnimationBuilder
    {
        private static readonly Dictionary<Type, Animation> cachedAnimations = new();

        public static Animation GetAnimation<T>() where T : IAnimationBuilder, new()
        {
            // overkill? nah!
            var type = typeof(T);

            if (cachedAnimations.ContainsKey(type))
                return cachedAnimations[type];

            var animation = new T().Build();
            cachedAnimations[type] = animation;

            return animation;
        }

        public static void PurgeCache()
        {
            cachedAnimations.Clear();
        }
    }
}
