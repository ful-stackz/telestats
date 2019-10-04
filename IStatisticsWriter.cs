namespace TeleStats
{
    /// <summary>
    /// Represents a writer which writes <see cref="IStatistics" /> data to a target.
    /// </summary>
    public interface IStatisticsWriter
    {
        /// <summary>
        /// Writes the data specified from the specified <paramref name="stats" />.
        /// </summary>
        /// <param name="stats">
        /// Provides the set of statistics values.
        /// </param>
        /// <exception cref="StatisticsWriterException">
        /// When the statistics data could not be written.
        /// </exception>
        void Write(IStatistics stats);

        /// <summary>
        /// Stores all queued statistics immediately.
        /// </summary>
        void Flush();
    }
}