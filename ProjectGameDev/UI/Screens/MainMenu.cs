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
    internal class MainMenu : UIContainer
    {
        protected readonly World world;
        protected readonly GraphicsDevice graphicsDevice;
        protected readonly GameManager gameManager;

        protected readonly int windowWidth;
        protected readonly int windowHeight;

        public MainMenu(DependencyManager dependencyManager) : base(dependencyManager)
        {
            dependencyManager.InjectChecked(ref world);
            dependencyManager.InjectChecked(ref graphicsDevice);
            dependencyManager.InjectChecked(ref gameManager);

            windowWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
            windowHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
        }

        public override void Load()
        {
            world.BackgroundColor = Color.Black;
            var middle = new Vector2(windowWidth / 2, windowHeight / 2);

            var title = new TextLabel(dependencyManager, middle+new Vector2(0, -100), "Hello world!", HorizontalTextAlignment.Center, VerticalTextAlignment.Center);
            AddElement(title);

            var button1 = new Button(dependencyManager, middle, new Point(100, 50), "Button1", HorizontalAlignment.Center, VerticalAlignment.Center, textColor: Color.Black);
            AddElement(button1);

            button1.MouseComponent.OnClickEvent += OnStartClick;
        }

        private void OnStartClick(object sender, EventArgs e)
        {
            gameManager.TransitionTo<Level1State>();
        }
    }
}
