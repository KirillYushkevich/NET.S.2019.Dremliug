using System;
using System.Timers;

namespace NET.S._2019.Dremliug._11
{
    /// <summary>
    /// A custom timer for a single callback at an interval in seconds.
    /// </summary>
    public class TimerAfter
    {
        #region Private timer
        /// <summary>
        /// Internal timer measures an interval once.
        /// </summary>
        private Timer timer = new Timer() { AutoReset = false };
        #endregion

        #region Constructor
        public TimerAfter(int seconds, EventHandler<TimeElapsedEventArgs> callback)
        {
            // Convert seconds to ms for timer.Interval.
            timer.Interval = seconds * 1000;
            timer.Elapsed += OnTimeElapsed;

            TimeElapsedEventHandler += callback;
        }
        #endregion

        #region Event
        /// <summary>
        /// Occurs when interval elapsed.
        /// </summary>
        public event EventHandler<TimeElapsedEventArgs> TimeElapsedEventHandler;
        #endregion

        #region Timer control methods
        /// <summary>
        /// Starts the timer or restarts if it is already running.
        /// </summary>
        public void Start()
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// Sets the new interval and restarts the timer.
        /// </summary>
        /// <param name="seconds"></param>
        public void Start(int seconds)
        {
            timer.Interval = seconds * 1000;
            this.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }
        #endregion

        #region OnEvent
        /// <summary>
        /// Raises TimerAfter event when the time expires.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTimeElapsed(object sender, ElapsedEventArgs e)
        {
            TimeElapsedEventHandler?.Invoke(this, new TimeElapsedEventArgs() { EndTime = e.SignalTime });
        } 
        #endregion

        #region EventArgs provider.
        /// <summary>
        /// Provides arguments for TimerAfter event.
        /// </summary>
        public class TimeElapsedEventArgs : EventArgs
        {
            public DateTime EndTime { get; set; }
        }
        #endregion
    }
}
