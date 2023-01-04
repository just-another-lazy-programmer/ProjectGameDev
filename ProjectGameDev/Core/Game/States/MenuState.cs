using Microsoft.Xna.Framework;
using ProjectGameDev.Levels.TestLevel;
using ProjectGameDev.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Core.Game.States
{
    internal class MenuState : GameState
    {
        private World world;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);

            var menuScreen = new MainMenu(dependencyManager);
            menuScreen.Load();
            world.ActiveScreen = menuScreen;
        }

        public override void OnStateLeft()
        {
            base.OnStateLeft();

            world.ActiveScreen = null;
        }
    }
}
