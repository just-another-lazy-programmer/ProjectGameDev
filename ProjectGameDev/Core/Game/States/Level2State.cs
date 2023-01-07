using Microsoft.Xna.Framework;
using ProjectGameDev.Levels.Level2;
using ProjectGameDev.UI.Screens;

namespace ProjectGameDev.Core.Game.States
{
    internal class Level2State : GameState
    {
        private World world;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            dependencyManager.InjectChecked(ref world);

            var level = new Level2(dependencyManager);
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
            world.ActiveScreen = null;
        }
    }
}
