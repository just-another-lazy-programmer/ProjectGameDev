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
    internal class Level1State : GameState
    {
        private World world;
        private SoundManager soundManager;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref soundManager);

            var level = new Level1(dependencyManager);
            level.Load();
            world.LoadedLevel = level;

            var hud = new HUD(dependencyManager);
            hud.Load();
            world.ActiveScreen = hud;

            soundManager.StartMusic(MusicType.Game);

            world.BackgroundColor = Color.Gray;
        }

        public override void OnStateLeft()
        {
            base.OnStateLeft();

            world.LoadedLevel = null;
            world.ActiveScreen = null;
        }
    }
}
