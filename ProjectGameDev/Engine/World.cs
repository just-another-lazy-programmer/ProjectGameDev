﻿using Microsoft.Xna.Framework;
using ProjectGameDev.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    public class World
    {
        public Level LoadedLevel { get; set; }
        public Color BackgroundColor { get; set; }
    }
}
