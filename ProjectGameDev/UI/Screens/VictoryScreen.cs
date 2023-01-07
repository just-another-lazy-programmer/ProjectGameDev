using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Core.Game;
using ProjectGameDev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGameDev.UI.Core;
using ProjectGameDev.Core.Game.States;
using ProjectGameDev.UI.Elements;
using Microsoft.Xna.Framework;
using ProjectGameDev.Objects;

namespace ProjectGameDev.UI.Screens
{
    internal class VictoryScreen : UIContainer
    {
        protected readonly World world;
        protected readonly GameManager gameManager;
        protected readonly GraphicsDevice graphicsDevice;

        protected readonly int windowWidth;
        protected readonly int windowHeight;

        public VictoryScreen(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref gameManager);
            dependencyManager.InjectChecked(ref graphicsDevice);

            windowWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
            windowHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
        }

        public override void Load()
        {
            base.Load();

            world.BackgroundColor = Color.DimGray;
            var middle = new Vector2(windowWidth / 2, windowHeight / 2);

            var title = new TextLabel(dependencyManager, middle + new Vector2(0, -100), "You won!", HorizontalTextAlignment.Center, VerticalTextAlignment.Center, "TitleFont", color: Color.LightGreen);
            AddElement(title);

            var button1 = new Button(dependencyManager, middle, new Point(100, 40), "To Menu", HorizontalAlignment.Center, VerticalAlignment.Center, textColor: Color.Black);
            AddElement(button1);

            button1.MouseComponent.OnClickEvent += MouseComponent_OnClickEvent;
        }

        private void MouseComponent_OnClickEvent(object sender, EventArgs e)
        {
            gameManager.TransitionTo<MenuState>();
        }
    }
}
