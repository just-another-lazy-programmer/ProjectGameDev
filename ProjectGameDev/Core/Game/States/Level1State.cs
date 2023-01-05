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
    internal class Level1State : GameState
    {
        private World world;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);

            var level = new TestLevel(dependencyManager);
            level.Load();
            world.LoadedLevel = level;

            var hud = new HUD(dependencyManager);
            hud.Load();
            world.ActiveScreen = hud;

            world.BackgroundColor = Color.Gray;
        }

        public override void OnStateLeft()
        {
            base.OnStateLeft();

            world.LoadedLevel = null;
        }
    }
}
