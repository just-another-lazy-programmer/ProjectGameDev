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

namespace ProjectGameDev
{
    public class Game1 : Game
    {
        public const string GameName = "ProjectGameDev";

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly DependencyManager dependencyManager;
        private readonly Core.Engine engine;
        private readonly World world;

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

            engine = new Core.Engine(dependencyManager);
            Window.AllowUserResizing = true;
        }

        // We want the Crash Handler to be able to access the logger
        public Logger GetLogger()
        {
            // This better not throw lol
            return dependencyManager.GetDependencyChecked<Logger>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();

            graphics.PreferredBackBufferHeight = 32 * 20;
            graphics.PreferredBackBufferWidth = 32 * 30;
            graphics.ApplyChanges();

            dependencyManager.RegisterDependency(GraphicsDevice);
            dependencyManager.RegisterDependency(graphics);

            // @todo: consider moving over to Program.cs
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appRoot = Path.Combine(appData, GameName);

            var newLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    Path.Combine(appRoot, "Logs/log-.log"),
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    outputTemplate: "{Timestamp} [{Level}] {Message}{NewLine}{Exception}"
                )
                .CreateLogger();

            newLogger.Debug("Started!");

            dependencyManager.RegisterDependency(newLogger);

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
            engine.LoadLevel();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //hero.Update(gameTime);

            engine.Tick(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(world.BackgroundColor);

            spriteBatch.Begin();
            engine.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}