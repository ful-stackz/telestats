using System;
using System.Collections.Generic;
using System.Linq;
using TeleStats.Utilities;

namespace TeleStats
{
    /// <summary>
    /// Provides configuration options.
    /// </summary>
    public class Configuration
    {
        private static readonly Configuration Instance = new Configuration();

        private readonly HashSet<string> _configFlags = new HashSet<string>();

        private Configuration()
        {
        }

        internal IEnumerable<string> Flags => _configFlags;

        /// <summary>
        /// Gets the <see cref="TeleStats.Configuration" /> object.
        /// </summary>
        public static Configuration Get => Instance;

        /// <summary>
        /// Indicates that TeleStats should use environment variables for configuration and
        /// specifies the <paramref name="variableNames" /> of the environment variables to be used
        /// for configuring TeleStats.
        /// </summary>
        /// <param name="variableNames">
        /// The names of the environment variables to be used.
        /// </param>
        /// <returns>
        /// The <see cref="TeleStats.Configuration" /> object.
        /// </returns>
        public Configuration UseEnvironmentVariables(params string[] variableNames)
        {
            variableNames
                .Select(x => x is null ? null : Environment.GetEnvironmentVariable(x))
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(',', ';'))
                .SelectMany(x => x)
                .Select(x => x.Trim().ToUpperInvariant())
                .ToList()
                .ForEach(x => _configFlags.Add(x));

            return this;
        }

        /// <summary>
        /// Indicates that TeleStats should use command line arguments for configuration and
        /// specifies the <paramref name="prefix" /> of the arguments to be used
        /// for configuring TeleStats.
        /// </summary>
        /// <param name="prefix">
        /// Used to match the command line arguments which are to be used for configuring TeleStats.
        /// </param>
        /// <returns>
        /// The <see cref="TeleStats.Configuration" /> object.
        /// </returns>
        public Configuration UseCommandLineArguments(string prefix)
        {
            Environment.GetCommandLineArgs()
                .Select(x => x.StartsWith(prefix) ? x : null)
                .Where(x => !(x is null))
                .Select(x => x.Substring(prefix.Length))
                .Select(x => x.Trim().ToUpperInvariant())
                .ToList()
                .ForEach(x => _configFlags.Add(x));

                return this;
        }

        /// <summary>
        /// Rebuilds the TeleStats configuration.
        /// </summary>
        public void Build()
        {
            FeatureFlags.Reload();
        }
    }
}