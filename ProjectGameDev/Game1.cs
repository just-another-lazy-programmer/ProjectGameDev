using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Characters;
using ProjectGameDev.Engine;
using ProjectGameDev.Utility;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace ProjectGameDev
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly DependencyManager dependencyManager;
        private readonly Engine.Engine engine;
        private readonly World world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            dependencyManager = new DependencyManager();
            world = new World();

            dependencyManager.RegisterDependency(Content);
            dependencyManager.RegisterDependency(world);

            engine = new Engine.Engine(dependencyManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();

            graphics.PreferredBackBufferHeight = 16 * 45;
            graphics.PreferredBackBufferWidth = 16 * 80;
            graphics.ApplyChanges();

            dependencyManager.RegisterDependency(GraphicsDevice);

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

            foreach (var worldObject in world.LoadedLevel.GetObjects())
            {
                worldObject.Update(gameTime);
            }

            engine.Tick();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(world.BackgroundColor);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            /*
            var source = new Rectangle(2940, 0, 319, 486);
            var destination = new Rectangle(0, 0, (int)(319*0.3), (int)(486*0.3));
            _spriteBatch.Draw(hero, destination, source, Color.White);
            */

            SortedDictionary<DrawLayer, List<Engine.IDrawable>> objects = new();

            foreach (var worldObject in world.LoadedLevel.GetObjects())
            {
                if (worldObject is Engine.IDrawable drawable)
                {
                    if (!objects.ContainsKey(drawable.DrawLayer))
                        objects.Add(drawable.DrawLayer, new());

                    objects[drawable.DrawLayer].Add(drawable);
                }
            }

            foreach (var layer in objects)
                foreach (var obj in layer.Value)
                    obj.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}