using ProjectGameDev.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Game.States
{
    internal class GameOverState : GameState
    {
        protected World world;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);

            var gameOverScreen = new GameOverScreen(dependencyManager);
            gameOverScreen.Load();
            world.ActiveScreen = gameOverScreen;
        }

        public override void OnStateLeft()
        {
            base.OnStateLeft();

            world.ActiveScreen = null;
        }
    }
}
