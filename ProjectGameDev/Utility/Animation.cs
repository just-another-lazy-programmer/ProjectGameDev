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
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int currentIndex;
        private int fps;
        private double secondsElapsed;

        public Animation(int fps)
        {
            this.fps = fps;
            frames = new List<AnimationFrame>();
            secondsElapsed = 0;
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void AddFramesBatch(List<AnimationFrame> frames)
        {
            this.frames.AddRange(frames);
            CurrentFrame = frames[0];
        }

        public void AddFramesBatch(params AnimationFrame[] frames)
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
