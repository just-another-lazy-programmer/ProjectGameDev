using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core
{
    internal interface IAnimationBuilder
    {
        public int FramesPerSecond { get; }
        public string Texture { get; }
        public bool Loop { get; }
        public Animation Build(DependencyManager dependencyManager);
    }
}
