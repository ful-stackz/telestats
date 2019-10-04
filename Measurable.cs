using System.Diagnostics;

namespace TeleStats
{
    /// <summary>
    /// Represents a statistics measurable in time.
    /// </summary>
    public class Measurable
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Creates a new instance of the <see cref="Measurable" /> class.
        /// </summary>
        public Measurable()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Gets the ellapsed milliseconds since the measurement was started.
        /// </summary>
        public virtual long Milliseconds { get; private set; }

        /// <summary>
        /// Starts the measurement.
        /// </summary>
        public virtual void Start()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops the measurement and updates the <see cref="Milliseconds"/>.
        /// </summary>
        /// <param name="reset">
        /// Specifies whether to reset the measure after stopping and setting the <see cref="Milliseconds"/>.
        /// </param>
        public virtual void Stop(bool reset = false)
        {
            _stopwatch.Stop();
            Milliseconds = _stopwatch.ElapsedMilliseconds;

            if (reset)
            {
                _stopwatch.Reset();
            }
        }

        /// <summary>
        /// Resets the measurement.
        /// </summary>
        public virtual void Reset()
        {
            _stopwatch.Reset();
            Milliseconds = 0;
        }
    }
}