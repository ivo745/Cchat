using System;
using System.Timers;

namespace Cchat
{
    public class CchatTimer : IDisposable
    {
        private const string MSG_TIMER_NOT_RUNNING = "Timer is not running";
        private Timer timer;
        private int count = 0;

        ~CchatTimer()
        {
            // Finalizer calls Dispose(false)  
            Dispose(false);
        }

        public int ShowDelay()
        {
            if (count > 0)
            {
                Dispose();
                return count;
            }
            else
            {
                Console.WriteLine(MSG_TIMER_NOT_RUNNING);
                return 0;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            count++;
        }

        public void Start(int interval)
        {
            count = 0;
            timer = new Timer(interval);
            timer.Elapsed += TimerTick;
            timer.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (timer != null)
                timer.Dispose();
        }
    }
}