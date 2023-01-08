using Microsoft.Xna.Framework;
using ProjectGameDev.Levels.Level1;
using ProjectGameDev.UI.Screens;
using ProjectGameDev.Utility;
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
        private SoundManager soundManager;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref soundManager);

            soundManager.StartMusic(MusicType.Menu);

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
