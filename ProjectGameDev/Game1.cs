using Microsoft.Xna.Framework;
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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //hero = new Hero(Content.Load<Texture2D>("hero"));
            GlobalEngine.LoadLevel(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //hero.Update(gameTime);

            foreach (var worldObject in GlobalEngine.LoadedLevel.GetObjects())
            {
                worldObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            /*
            var source = new Rectangle(2940, 0, 319, 486);
            var destination = new Rectangle(0, 0, (int)(319*0.3), (int)(486*0.3));
            _spriteBatch.Draw(hero, destination, source, Color.White);
            */

            SortedDictionary<DrawLayer, List<Engine.IDrawable>> objects = new();

            foreach (var worldObject in GlobalEngine.LoadedLevel.GetObjects())
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
                    obj.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}