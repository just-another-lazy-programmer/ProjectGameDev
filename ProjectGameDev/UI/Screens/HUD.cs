using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Core;
using ProjectGameDev.Core.Game;
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

        public HUD(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref gameManager);

            var player = world.LoadedLevel.GetObject<Hero2>();

            player.HealthComponent.OnHealthChangedEvent += HealthComponent_OnHealthChangedEvent;
            player.HealthComponent.OnDeathEvent += HealthComponent_OnDeathEvent;
        }

        private void HealthComponent_OnDeathEvent(object sender, ProjectGameDev.Components.DeathEventArgs e)
        {
            
        }

        private void HealthComponent_OnHealthChangedEvent(object sender, ProjectGameDev.Components.HealthChangeEventArgs e)
        {
            textLabel.SetText($"Health: {e.CurrentHealth}");
        }

        public override void Load()
        {
            base.Load();

            textLabel = new TextLabel(dependencyManager, new Vector2(20, 30), "Health: 100");
            AddElement(textLabel);
        }

        public void UpdateHealth(float health)
        {
            textLabel.SetText($"Health: {health}");
        }
    }
}
