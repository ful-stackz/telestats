using System;

namespace TeleStats
{
    /// <summary>
    /// Represents a base implementation of the <see cref="IStatistics" /> interface.
    /// </summary>
    public abstract class StatisticsBase : IStatistics
    {
        private readonly IStatisticsWriter _writer;

        /// <summary>
        /// Initializes the <see cref="StatisticsBase" /> with an <see cref="IStatisticsWriter" /> instance
        /// from the specified <paramref name="writerFactory" />.
        /// </summary>
        /// <param name="writerFactory">
        /// Creates a new <see cref="IStatisticsWriter" /> instance.
        /// </param>
        protected StatisticsBase(StatisticsWriterFactory writerFactory)
        {
            _writer = writerFactory is null
                ? throw new ArgumentNullException(nameof(writerFactory))
                : writerFactory();
        }

        /// <inheritdoc />
        public virtual string Header => string.Empty;

        /// <inheritdoc />
        public virtual string GetNextData() => string.Empty;

        /// <inheritdoc />
        public virtual void Save()
        {
            _writer.Write(this);
        }

        /// <inheritdoc />
        public virtual void Reset()
        {
        }
    }
}