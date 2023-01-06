using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    internal class GameOverScreen : UIContainer
    {
        protected readonly World world;
        protected readonly GameManager gameManager;
        protected readonly GraphicsDevice graphicsDevice;

        protected readonly int windowWidth;
        protected readonly int windowHeight;

        public GameOverScreen(DependencyManager dependencyManager) : base(dependencyManager)
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

            world.BackgroundColor = Color.Black;
            var middle = new Vector2(windowWidth / 2, windowHeight / 2);

            var title = new TextLabel(dependencyManager, middle + new Vector2(0, -100), "You died!", HorizontalTextAlignment.Center, VerticalTextAlignment.Center, "TitleFont");
            AddElement(title);

            var button1 = new Button(dependencyManager, middle, new Point(100, 50), "To Menu", HorizontalAlignment.Center, VerticalAlignment.Center, textColor: Color.Black);
            AddElement(button1);

            button1.MouseComponent.OnClickEvent += MouseComponent_OnClickEvent;
        }

        private void MouseComponent_OnClickEvent(object sender, EventArgs e)
        {
            gameManager.TransitionTo<MenuState>();
        }
    }
}
