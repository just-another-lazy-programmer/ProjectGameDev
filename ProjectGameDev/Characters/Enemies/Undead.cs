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

        private readonly AnimationBuilder animationBuilder;

        private const double scale = 2f;

        public Undead(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);

            rootComponent = CreateDefaultComponent<RootComponent>();

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<UndeadIdleAnimation>());
            animationComponent.SetFlip(true);

            ActivateComponents();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
        }
    }
}
