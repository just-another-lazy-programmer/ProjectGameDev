using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Levels;

namespace ProjectGameDev.Engine
{
    internal static class GlobalEngine
    {
        public static Level LoadedLevel { get; private set; }

        static public void LoadLevel(ContentManager contentManager)
        {
            LoadedLevel = new Level();
            LoadedLevel.Load(contentManager);
        }
    }
}
