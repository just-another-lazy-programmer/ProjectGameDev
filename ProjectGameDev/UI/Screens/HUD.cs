using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Game;
using ProjectGameDev.Core.Game.States;
using ProjectGameDev.UI.Core;
using ProjectGameDev.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.UI.Screens
{
    internal class HUD : UIContainer
    {
        protected readonly World world;
        protected readonly GameManager gameManager;
        protected TextLabel textLabel;
        protected HealthBar healthBar;
        protected Hero2 player;

        public HUD(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref gameManager);

            player = world.LoadedLevel.GetObject<Hero2>();

            player.HealthComponent.OnHealthChangedEvent += HealthComponent_OnHealthChangedEvent;
            player.HealthComponent.OnDeathEvent += HealthComponent_OnDeathEvent;
        }

        private void HealthComponent_OnDeathEvent(object sender, ProjectGameDev.Components.DeathEventArgs e)
        {
            gameManager.TransitionTo<GameOverState>();
        }

        private void HealthComponent_OnHealthChangedEvent(object sender, ProjectGameDev.Components.HealthChangeEventArgs e)
        {
            textLabel.SetText($"Health: {e.CurrentHealth}");
            healthBar.SetHealth(e.CurrentHealth);
        }

        public override void Load()
        {
            base.Load();

            textLabel = new TextLabel(dependencyManager, new Vector2(20, 30), $"Health: {player.HealthComponent.MaxHealth}");
            healthBar = new HealthBar(dependencyManager, new Vector2(15, 30), new Point(130, 10), player.HealthComponent.MaxHealth);

            //AddElement(textLabel);
            AddElement(healthBar);
        }
    }
}
