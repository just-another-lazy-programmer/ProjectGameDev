using Microsoft.Xna.Framework;
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
        private int currentIndex;
        private int fps;
        private double secondsElapsed;

        public Animation(int fps)
        {
            this.fps = fps;
            frames = new List<Sprite>();
            secondsElapsed = 0;
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

        public void Update(GameTime gameTime)
        {
            secondsElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            CurrentFrame = frames[currentIndex];

            if (secondsElapsed >= 1d / fps)
            {
                currentIndex++;
                secondsElapsed = 0;
            }

            if (currentIndex >= frames.Count)
            {
                currentIndex = 0;
            }
        }
    }
}
