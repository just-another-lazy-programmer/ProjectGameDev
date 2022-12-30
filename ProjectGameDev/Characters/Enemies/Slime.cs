using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations.GreenSlime;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Characters.Enemies
{
    internal class Slime : WorldObject, IDrawable
    {
        public DrawLayer DrawLayer => DrawLayer.Enemies;
        protected AnimationBuilder animationBuilder;
        protected AnimationComponent animationComponent;
        protected const double scale = 0.2;

        public Slime(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref animationBuilder);

            CreateDefaultComponent<RootComponent>();

            var collisionComponent = CreateDefaultComponent<CollisionComponent>();
            collisionComponent.IsTrigger = true;

            animationComponent = CreateDefaultComponent<AnimationComponent>();
            animationComponent.SetAnimation(animationBuilder.GetAnimation<GreenSlimeIdleAnimation>());

            ActivateComponents();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, scale);
        }
    }
}
