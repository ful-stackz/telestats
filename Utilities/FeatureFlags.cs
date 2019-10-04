using System;
using System.Collections.Generic;
using System.Linq;

namespace TeleStats.Utilities
{
    public class FeatureFlags
    {
        private const string EnvironmentVariableName = "TELESTATS_FLAGS";

        private static HashSet<FeatureFlag> _availableFeatureFlags = new HashSet<FeatureFlag>();

        static FeatureFlags()
        {
            Reload();
        }

        public static void Reload()
        {
            _availableFeatureFlags = new HashSet<FeatureFlag>(
                Environment.GetEnvironmentVariable(EnvironmentVariableName)
                .Split(',', ';')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => new FeatureFlag(x)));
        }

        public static bool Has(FeatureFlag featureFlag) =>
            _availableFeatureFlags.Contains(featureFlag);
    }
}