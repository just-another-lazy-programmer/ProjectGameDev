using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Game
{
    internal abstract class GameState
    {
        protected GameManager gameManager;
        protected DependencyManager dependencyManager;

        public void SetContext(GameManager gameManager, DependencyManager dependencyManager)
        {
            this.gameManager = gameManager;
            this.dependencyManager = dependencyManager;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void OnStateEnter() { }
        public virtual void OnStateLeft() { }
    }
}
