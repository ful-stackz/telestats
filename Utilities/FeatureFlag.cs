namespace TeleStats.Utilities
{
    /// <summary>
    /// Represents a feature flag, used for toggling features.
    /// </summary>
    public struct FeatureFlag
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlag" /> struct.
        /// </summary>
        /// <param name="name"></param>
        public FeatureFlag(string name)
        {
            _name = name is null ? string.Empty : name.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Gets a <see cref="FeatureFlag" /> toggling the TeleStats enabled state.
        /// </summary>
        public static FeatureFlag EnableTeleStats { get; } = new FeatureFlag("TELESTATS_ENABLED");

        /// <summary>
        /// Checks whether <paramref name="featureFlagA" /> and <paramref name="featureFlagB" />
        /// are equal.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether <paramref name="featureFlagA" /> and
        /// <paramref name="featureFlagB" /> are equal.
        /// </returns>
        public static bool operator ==(FeatureFlag featureFlagA, FeatureFlag featureFlagB) =>
            Equals(featureFlagA, featureFlagB);

        /// <summary>
        /// Checks whether <paramref name="featureFlagA" /> and <paramref name="featureFlagB" />
        /// are not equal.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether <paramref name="featureFlagA" /> and
        /// <paramref name="featureFlagB" /> are not equal.
        /// </returns>
        public static bool operator !=(FeatureFlag featureFlagA, FeatureFlag featureFlagB) =>
            !Equals(featureFlagA, featureFlagB);

        /// <inheritdoc />
        public override string ToString() =>
            _name ?? string.Empty;

        /// <inheritdoc />
        public override int GetHashCode() =>
            _name?.GetHashCode() ?? 0;

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            Equals(_name, (obj is FeatureFlag other) ? other.ToString() : null);

        /// <summary>
        /// Checks whether the specified <paramref name="other" /> <see cref="FeatureFlag" />
        /// is equal to this one.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether the specified <paramref name="other" /> <see cref="FeatureFlag" />
        /// and this one are equal.
        /// </returns>
        public bool Equals(FeatureFlag other) =>
            Equals(_name, other.ToString());
    }
}