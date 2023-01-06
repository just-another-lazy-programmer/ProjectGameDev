using Microsoft.Xna.Framework;
using ProjectGameDev.Characters;
using ProjectGameDev.Characters.Enemies;
using ProjectGameDev.Components;
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
        protected HealthBar playerHealthBar;
        protected HealthBar bossHealthBar;
        protected Undead undeadBoss;
        protected Hero2 player;

        public HUD(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref gameManager);

            player = world.LoadedLevel.GetObject<Hero2>();
            undeadBoss = world.LoadedLevel.GetObject<Undead>();

            player.HealthComponent.OnHealthChangedEvent += Player_OnHealthChangedEvent;
            player.HealthComponent.OnDeathEvent += Player_OnDeathEvent;

            if (undeadBoss != null)
            {
                undeadBoss.HealthComponent.OnHealthChangedEvent += Undead_OnHealthChangedEvent1;
                undeadBoss.HealthComponent.OnDeathEvent += Undead_OnDeathEvent;
            }
        }

        private void Undead_OnDeathEvent(object sender, ProjectGameDev.Components.DeathEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Undead_OnHealthChangedEvent1(object sender, ProjectGameDev.Components.HealthChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Player_OnDeathEvent(object sender, ProjectGameDev.Components.DeathEventArgs e)
        {
            gameManager.TransitionTo<GameOverState>();
        }

        private void Player_OnHealthChangedEvent(object sender, ProjectGameDev.Components.HealthChangeEventArgs e)
        {
            textLabel.SetText($"Health: {e.CurrentHealth}");
            playerHealthBar.SetHealth(e.CurrentHealth);
        }

        public override void Load()
        {
            base.Load();

            textLabel = new TextLabel(dependencyManager, new Vector2(20, 30), $"Health: {player.HealthComponent.MaxHealth}");
            playerHealthBar = new HealthBar(dependencyManager, new Vector2(15, 30), new Point(130, 10), player.HealthComponent.MaxHealth);

            if (undeadBoss != null)
            {
                bossHealthBar = new HealthBar(
                    dependencyManager, 
                    undeadBoss.GetComponentFast<RootComponent>().Location+new Vector2(55, 15), 
                    new Point(90, 8), 
                    undeadBoss.HealthComponent.MaxHealth
                );
                AddElement(bossHealthBar);
            }

            //AddElement(textLabel);
            AddElement(playerHealthBar);
        }
    }
}
