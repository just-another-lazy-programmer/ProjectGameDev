using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class Animation
    {
        public Sprite CurrentFrame { get; set; }
        private List<Sprite> frames;
        private Texture2D texture;
        public int CurrentIndex { get; private set; }
        private int fps;
        private double secondsElapsed;
        private bool loop;
        public bool Finished { get; private set; }

        public Animation(int fps, bool loop, Texture2D texture)
        {
            this.fps = fps;
            this.loop = loop;
            this.texture = texture;
            Finished = false;
            frames = new List<Sprite>();
            secondsElapsed = 0;
        }

        public void Play()
        {
            CurrentIndex = 0;
            Finished = false;
        }

        public void AddFrame(Sprite frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void AddFramesBatch(List<Sprite> frames)
        {
            this.frames.AddRange(frames);
            CurrentFrame = frames[0];
        }

        public void AddFramesBatch(params Sprite[] frames)
        {
            this.frames.AddRange(frames);
            CurrentFrame = frames[0];
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void Update(GameTime gameTime)
        {
            secondsElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            CurrentFrame = frames[CurrentIndex];

            if (secondsElapsed >= 1d / fps)
            {
                if (!Finished)
                {
                    CurrentIndex++;
                }
                secondsElapsed = 0;
            }

            if (CurrentIndex >= frames.Count)
            {
                if (loop)
                    CurrentIndex = 0;
                else
                {
                    Finished = true;
                    CurrentIndex = frames.Count - 1;
                }
            }
        }
    }
}
