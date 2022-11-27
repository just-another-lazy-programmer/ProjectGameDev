﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ProjectGameDev.Engine;
using ProjectGameDev.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Levels
{
    internal class TestMenuLevel : Level
    {
        Random rng = new Random();

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);

            GlobalEngine.BackgroundColor = Color.Black;

            var bounds = GlobalEngine.GraphicsDevice.PresentationParameters.Bounds;
            const int size = 40;
            var middle = new Vector2(bounds.Size.X / 2, bounds.Size.Y / 2);
            var radius = size * 5;
            var inner = (int)(size * 2);
            // 40x40
            for (int x = 0; x < bounds.Size.X/size; x++)
            {
                for (int y = 0; y < bounds.Size.Y/size; y++)
                {
                    var point = new Vector2(x * size, y * size);
                    var length = (point - middle).Length();
                    var inCircle = length < radius && length >= inner;
                    AddRect((x * 75) + (y*50), point, new Point(size, size), !inCircle);
                }
            }
        }

        public async void AddRect(int delayMs, Vector2 location, Point size, bool isOutline)
        {
            await Task.Delay(delayMs);
            var rect = new DebugRectangle(location, size);
            var bytes = new byte[3];
            rng.NextBytes(bytes);
            //rect.Color = new Color(bytes[0], bytes[1], bytes[2]);
            rect.Color = Color.White;
            rect.IsOutline = isOutline;
            AddObject(rect);
        }
    }
}