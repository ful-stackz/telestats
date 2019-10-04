namespace TeleStats
{
    /// <summary>
    /// Creates a new <see cref="IStatisticsWriter" /> instance when invoked.
    /// </summary>
    /// <returns>
    /// A new <see cref="IStatisticsWriter" /> instance.
    /// </returns>
    public delegate IStatisticsWriter StatisticsWriterFactory();
}