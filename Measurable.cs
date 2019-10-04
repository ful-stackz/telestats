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
        public virtual long Milliseconds => _stopwatch.ElapsedMilliseconds;

        /// <summary>
        /// Starts the measurement.
        /// </summary>
        public virtual void Start()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops the measurement.
        /// </summary>
        public virtual void Stop()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// Resets the measurement.
        /// </summary>
        public virtual void Reset()
        {
            _stopwatch.Reset();
        }
    }
}