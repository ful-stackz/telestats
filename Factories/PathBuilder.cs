using System;
using System.IO;

namespace TeleStats.Factories
{
    /// <summary>
    /// Provides functionality for building paths.
    /// </summary>
    public class PathBuilder
    {
        private static PathBuilder s_instance;

        private PathBuilder()
        {
        }

        /// <summary>
        /// Gets the path to the Desktop directory.
        /// </summary>
        public string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// Gets the path to the Common Application Data directory.
        /// </summary>
        public string CommonApplicationData => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        /// <summary>
        /// Gets or creates an initialized <see cref="PathBuilder" /> instance.
        /// </summary>
        public static PathBuilder Get()
        {
            var instance = s_instance is null ? new PathBuilder() : s_instance;

            if (s_instance is null)
            {
                s_instance = instance;
            }

            return instance;
        }

        /// <summary>
        /// Constructs a path from the given path <paramref name="parts" />.
        /// </summary>
        /// <param name="parts">
        /// Separata parts to a location to be combined into a single path.
        /// </param>
        /// <returns>
        /// A new <see cref="string" /> instance containing a complete path.
        /// </returns>
        public string PathFrom(params string[] parts) => Path.Combine(parts);
    }
}