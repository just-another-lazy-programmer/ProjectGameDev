﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Components;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Objects
{
    internal class DebugRectangle : WorldObject, Core.IDrawable
    {
        public RootComponent RootComponent { get; protected set; }
        public CollisionComponent2 CollisionComponent { get; protected set; }
        public Color Color { get; set; }
        public bool IsOutline { get; set; }

        public DrawLayer DrawLayer => DrawLayer.Debug;

        protected Texture2D texture;
        protected Point size;

        private SimpleSprites simpleSprites;

        public DebugRectangle(DependencyManager dependencyManager, Vector2 location, Point size) : base(dependencyManager)
        {
            RootComponent = CreateDefaultComponent<RootComponent>();
            CollisionComponent = CreateDefaultComponent<CollisionComponent2>();

            texture = new Texture2D(dependencyManager.GetDependencyChecked<GraphicsDevice>(), 1, 1);
            texture.SetData(new[] { Color.White });
            Color = Color.GreenYellow;

            ActivateComponents();

            CollisionComponent.AddHitbox(0, 0, size.X, size.Y);
            RootComponent.Move(location);

            dependencyManager.Inject(ref simpleSprites);

            this.size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rectangle = new Rectangle((int)RootComponent.Location.X, (int)RootComponent.Location.Y, size.X, size.Y);

            if (IsOutline)
            {
                simpleSprites.DrawRectangleOutline(spriteBatch, rectangle, Color, 2);
            }
            else
            {
                simpleSprites.DrawRectangle(spriteBatch, rectangle, Color);
            }
        }
    }
}
