using System;

namespace TeleStats.Factories
{
    /// <summary>
    /// Provides default <see cref="StatisticsWriterFactory" /> implementations.
    /// </summary>
    public static class DefaultWriterFactory
    {
        /// <summary>
        /// Creates a new <see cref="StatisticsWriterFactory" /> instance which returns a new
        /// <see cref="CsvFileStatisticsWriter" /> instance when invoked.
        /// </summary>
        /// <param name="buildPathToFile">
        /// Returns the path to the file in which the csv stats will be stored.
        /// </param>
        /// <returns>
        /// A new <see cref="StatisticsWriterFactory" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When one or more of the required parameters is <c>null</c>.
        /// </exception>
        public static StatisticsWriterFactory CsvFileWriter(Func<PathBuilder, string> buildPathToFile)
        {
            if (buildPathToFile is null)
            {
                throw new ArgumentNullException(nameof(buildPathToFile));
            }

            return () => new CsvFileStatisticsWriter(buildPathToFile(PathBuilder.Get()));
        }
    }
}