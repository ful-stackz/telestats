using System;
using System.Collections.Generic;
using System.Linq;

namespace TeleStats.Utilities
{
    /// <summary>
    /// Provides configuration flag checks.
    /// </summary>
    public class ConfigFlags
    {
        private static HashSet<ConfigFlag> _availableConfigFlags = new HashSet<ConfigFlag>();

        static ConfigFlags()
        {
            Reload();
        }

        /// <summary>
        /// Reloads the configuration flags.
        /// </summary>
        public static void Reload()
        {
            _availableConfigFlags = new HashSet<ConfigFlag>(
                Configuration.Get.Flags.Select(x => new ConfigFlag(x)));
        }

        /// <summary>
        /// Checks whether a specified <paramref name="configFlag" /> is set.
        /// </summary>
        /// <param name="configFlag">
        /// The config to check for.
        /// </param>
        /// <returns>
        /// A <see cref="bool" /> indicating whether the specified <paramref name="configFlag" /> is set.
        /// </returns>
        public static bool Has(ConfigFlag configFlag) =>
            _availableConfigFlags.Contains(configFlag);
    }
}