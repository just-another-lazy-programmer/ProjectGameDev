using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Utility
{
    internal class TimerManager
    {
        public void Delay(float seconds, Action action)
        {
            DelayInternal(seconds, action);
        }

        private async void DelayInternal(float seconds, Action action)
        {
            await Task.Delay((int)(seconds * 1000));
            action();
        }
    }
}
