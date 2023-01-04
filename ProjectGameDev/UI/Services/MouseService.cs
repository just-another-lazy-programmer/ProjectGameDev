using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.UI.Services
{
    public class MouseService
    {
        private double scaleFactorX = 1;
        private double scaleFactorY = 1;

        private int offsetX = 0;
        private int offsetY = 0;

        public Point GetMouseLocation()
        {
            var state = Mouse.GetState();
            return new Point((int)((state.X-offsetX) * scaleFactorX), (int)((state.Y-offsetY)*scaleFactorY));
        }

        public void UpdateScaleFactor(Point renderScale, Rectangle destination)
        {
            scaleFactorX = renderScale.X / (double)destination.Width;
            scaleFactorY = renderScale.Y / (double)destination.Height;

            offsetX = destination.X;
            offsetY = destination.Y;

            Debug.WriteLine($"sx: {scaleFactorX}, sy: {scaleFactorY}, ox: {offsetX}, oy: {offsetY}");
        }
    }
}
