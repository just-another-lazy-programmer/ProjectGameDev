﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Engine
{
    internal class Component
    {
        public bool IsActive { get; private set; }

        public virtual void Activate() 
        {
            IsActive = true;
        }
        public virtual void Deactivate() 
        {
            IsActive = false;
        }

        public virtual void Tick(GameTime gameTime) { }

    }
}
