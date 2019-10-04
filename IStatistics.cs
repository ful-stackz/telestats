namespace TeleStats
{
    /// <summary>
    /// Represents a set of statistics data.
    /// </summary>
    public interface IStatistics
    {
        /// <summary>
        /// Gets the header of the statistics, representing the data definitions.
        /// </summary>
        string Header { get; }

        /// <summary>
        /// Creates a <see cref="string" /> with the next set of statistics data.
        /// </summary>
        /// <returns>
        /// A new <see cref="string" /> instance with the next set of statistics data.
        /// </returns>
        string GetNextData();

        /// <summary>
        /// Saves the currently taken statistics values.
        /// </summary>
        void Save();

        /// <summary>
        /// Resets the current statistics values to their defaults.
        /// </summary>
        void Reset();
    }
}
