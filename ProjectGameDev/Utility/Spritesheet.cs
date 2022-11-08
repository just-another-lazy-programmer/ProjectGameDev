using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class Spritesheet
    {
        public List<Sprite> Sprites { get; protected set; } = new();

        private int currentPosX = 0;
        private int currentPosY = 0;

        private int currentHeight = 0;
        private int currentWidth = 0;

        private int currentPadding = 0;
        private int currentRowPadding = 0;

        public Spritesheet SetPositionX(int x)
        {
            currentPosX = x;
            return this;
        }

        public Spritesheet SetPositionY(int y)
        {
            currentPosY = y;
            return this;
        }

        public Spritesheet SetPosition(int x, int y)
        {
            currentPosX = x;
            currentPosY = y;
            return this;
        }

        public Spritesheet SetHeight(int height)
        {
            currentHeight = height;
            return this;
        }

        public Spritesheet SetWidth(int width)
        {
            currentWidth = width;
            return this;
        }

        public Spritesheet SetSize(int width, int height)
        {
            currentWidth = width;
            currentHeight = height;
            return this;
        }

        public Spritesheet SetPadding(int padding)
        {
            currentPadding = padding;
            return this;
        }

        public Spritesheet SetRowPadding(int padding)
        {
            currentRowPadding = padding;
            return this;
        }

        public Spritesheet Next()
        {
            currentPosX += currentWidth + currentPadding;
            return this;
        }

        public Spritesheet NextRow(int startLocationX=0)
        {
            currentPosX = startLocationX;
            currentPosY += currentHeight + currentRowPadding;
            return this;
        }

        public Spritesheet Take(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Sprites.Add(new Sprite(new Microsoft.Xna.Framework.Rectangle(currentPosX, currentPosY, currentWidth, currentHeight)));
                Next();
            }

            return this;
        }

        public List<Sprite> ToList()
        {
            return Sprites;
        }
    }
}
