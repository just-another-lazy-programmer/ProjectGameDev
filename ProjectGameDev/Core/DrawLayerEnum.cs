﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core
{
    public enum DrawLayer
    {
        Background = 1,
        Tile = 2,
        MovingObjects = 5,
        Enemies = 6,
        EnemiesTop = 7,
        Player = 10,
        Debug = 11,
        MenuBackground = 15,
        MenuFront = 20,
        DebugTop = 100
    }
}
