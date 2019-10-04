using System;
using System.Collections.Generic;
using System.Linq;

namespace TeleStats.Utilities
{
    /// <summary>
    /// Provides feature flag checks.
    /// </summary>
    public class FeatureFlags
    {
        private static HashSet<FeatureFlag> _availableFeatureFlags = new HashSet<FeatureFlag>();

        static FeatureFlags()
        {
            Reload();
        }

        /// <summary>
        /// Reloads the feature flags.
        /// </summary>
        public static void Reload()
        {
            _availableFeatureFlags = new HashSet<FeatureFlag>(
                Configuration.Get.Flags.Select(x => new FeatureFlag(x)));
        }

        /// <summary>
        /// Checks whether a specified <paramref name="featureFlag" /> is set.
        /// </summary>
        /// <param name="featureFlag">
        /// The feature to check for.
        /// </param>
        /// <returns>
        /// A <see cref="bool" /> indicating whether the specified <paramref name="featureFlag" /> is set.
        /// </returns>
        public static bool Has(FeatureFlag featureFlag) =>
            _availableFeatureFlags.Contains(featureFlag);
    }
}