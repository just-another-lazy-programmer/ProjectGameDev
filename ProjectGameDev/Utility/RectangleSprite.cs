using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    // class from https://stackoverflow.com/questions/13893959/how-to-draw-the-border-of-a-square
    // used for debugging
    public class SimpleSprites
    {
        static Texture2D _pointTexture;

        private void MakeTexture(SpriteBatch spriteBatch)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }
        }

        public void DrawRectangleOutline(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            MakeTexture(spriteBatch);

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }

        public void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            MakeTexture(spriteBatch);

            spriteBatch.Draw(_pointTexture, rectangle, color);
        }

        public void DrawPoint(SpriteBatch spriteBatch, Point point, Color color, int pointSize)
        {
            MakeTexture(spriteBatch);

            spriteBatch.Draw(_pointTexture, new Rectangle(point, new Point(pointSize, pointSize)), color);
        }
    }
}
