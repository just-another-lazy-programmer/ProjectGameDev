using ProjectGameDev.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Game.States
{
    internal class VictoryState : GameState
    {
        protected World world;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);

            var victoryScreen = new VictoryScreen(dependencyManager);
            victoryScreen.Load();
            world.ActiveScreen = victoryScreen;
        }

        public override void OnStateLeft()
        {
            base.OnStateLeft();

            world.ActiveScreen = null;
        }
    }
}
