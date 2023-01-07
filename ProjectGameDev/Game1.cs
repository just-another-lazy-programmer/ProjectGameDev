using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Characters;
using ProjectGameDev.Core;
using ProjectGameDev.Utility;
using System.Collections.Generic;
using System.Security.AccessControl;
using Serilog;
using System.IO;
using System;
using Serilog.Core;
using ProjectGameDev.Core.Game;
using ProjectGameDev.UI.Services;
using ProjectGameDev.Core.Game.States;

namespace ProjectGameDev
{
    // Scaling code was taken from https://community.monogame.net/t/how-do-i-make-full-screen-stretch-to-the-entire-screen-and-have-black-bars-on-the-sides-if-the-screen-aspect-ratio-isnt-16-9/17364/2

    public class Game1 : Game
    {
        public const string GameName = "ProjectGameDev";

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly DependencyManager dependencyManager;
        private readonly Core.Engine engine;
        private readonly World world;

        private readonly Point gameResolution = new(32 * 30, 32 * 20);
        private RenderTarget2D renderTarget;
        private Rectangle renderTargetDestination;
        private MouseService mouseService;
        private CooldownManager cooldownManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            dependencyManager = new DependencyManager();
            world = new World();
            world.BackgroundColor = Color.Gray;

            // We want the ContentManager and World to be accessible to all classes
            dependencyManager.RegisterDependency(Content);
            dependencyManager.RegisterDependency(world);

            var animationBuilder = new AnimationBuilder(dependencyManager);
            dependencyManager.RegisterDependency(animationBuilder);

            mouseService = new MouseService();
            dependencyManager.RegisterDependency(mouseService);

            dependencyManager.Inject(ref cooldownManager);

            engine = new Core.Engine(dependencyManager);
            //Window.AllowUserResizing = true;
        }

        public void SetLogger(Logger logger)
        {
            dependencyManager.RegisterDependency(logger);
        }

        public Logger GetLogger()
        {
            // This better not throw lol
            return dependencyManager.GetDependencyChecked<Logger>();
        }

        Rectangle GetRenderTargetDestination(Point resolution, int preferredBackBufferWidth, int preferredBackBufferHeight)
        {
            float resolutionRatio = (float)resolution.X / resolution.Y;
            float screenRatio;
            Point bounds = new Point(preferredBackBufferWidth, preferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;
            Rectangle rectangle = new Rectangle();

            if (resolutionRatio < screenRatio)
                scale = (float)bounds.Y / resolution.Y;
            else if (resolutionRatio > screenRatio)
                scale = (float)bounds.X / resolution.X;
            else
            {
                // Resolution and window/screen share aspect ratio
                rectangle.Size = bounds;
                return rectangle;
            }
            rectangle.Width = (int)(resolution.X * scale);
            rectangle.Height = (int)(resolution.Y * scale);
            return CenterRectangle(new Rectangle(Point.Zero, bounds), rectangle);
        }

        static Rectangle CenterRectangle(Rectangle outerRectangle, Rectangle innerRectangle)
        {
            Point delta = outerRectangle.Center - innerRectangle.Center;
            innerRectangle.Offset(delta);
            return innerRectangle;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();

            graphics.PreferredBackBufferHeight = gameResolution.Y;
            graphics.PreferredBackBufferWidth = gameResolution.X;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, gameResolution.X, gameResolution.Y);
            renderTargetDestination = GetRenderTargetDestination(gameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            dependencyManager.RegisterDependency(GraphicsDevice);
            dependencyManager.RegisterDependency(graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //hero = new Hero(Content.Load<Texture2D>("hero"));
            /*
            GlobalEngine.LoadLevel(Content, GraphicsDevice);
            GlobalEngine
                .ConnectMultiplayer("127.0.0.1", 6571)
                .GetAwaiter()
                .GetResult();
            */
            //engine.LoadLevel();

            var gameManager = new GameManager(dependencyManager);
            dependencyManager.RegisterDependency(gameManager);
            gameManager.TransitionTo<MenuState>();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (!cooldownManager.IsOnCooldown(this, null, 1))
                {
                    ToggleFullScreen();
                    cooldownManager.SetCooldown(this, null);
                }
            }

            // TODO: Add your update logic here
            //hero.Update(gameTime);

            engine.Tick(gameTime);

            base.Update(gameTime);
        }

        void ToggleFullScreen()
        {
            if (!graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                graphics.PreferredBackBufferWidth = gameResolution.X;
                graphics.PreferredBackBufferHeight = gameResolution.Y;
            }
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();

            renderTargetDestination = GetRenderTargetDestination(gameResolution, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mouseService.UpdateScaleFactor(gameResolution, renderTargetDestination);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(world.BackgroundColor);

            spriteBatch.Begin();
            engine.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            //renderTargetDestination = GraphicsDevice.Viewport.Bounds;

            spriteBatch.Begin();
            //engine.Draw(spriteBatch);
            spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}