using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.Undead;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters.Enemies
{
    internal class Undead : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;

        private readonly AnimationComponent animationComponent;
        private readonly RootComponent rootComponent;
        public HealthComponent HealthComponent { get; private set; }

        private readonly AnimationBuilder animationBuilder;
        private readonly World world;
        private bool isSummoning = false;

        private const double scale = 2f;
        private float lifespan = 2f;

        public Undead(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);
            dependencyManager.InjectChecked(ref world);

            rootComponent = CreateDefaultComponent<RootComponent>();

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());
            animationComponent.SetFlip(true);

            animationComponent.OnAnimationFinishedEvent += AnimationComponent_OnAnimationFinishedEvent;

            HealthComponent = CreateDefaultComponent<HealthComponent>();
            HealthComponent.MaxHealth = 4;

            DelayedTestSummon();

            ActivateComponents();
        }

        private void AnimationComponent_OnAnimationFinishedEvent(object sender, EventArgs e)
        {
            if (isSummoning)
            {
                isSummoning = false;
                animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());

                DelayedTestSummon();
            }
        }

        private async void DelayedTestSummon()
        {
            await Task.Delay(1000 * 4);
            Summon();
        }

        private void Summon()
        {
            isSummoning = true;
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadSummonAnimation>());
            const int frameBound = 2;
            animationComponent.BindFrame(frameBound, () =>
            {
                SpawnSummon();
                animationComponent.RemoveBinding(frameBound);
            });
        }

        private void SpawnSummon()
        {
            var summon = new UndeadSummon(DependencyManager, rootComponent.Location + new Microsoft.Xna.Framework.Vector2(0, 0));
            world.LoadedLevel?.AddObjectSafe(summon);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
        }
    }
}
