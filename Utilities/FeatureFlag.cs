namespace TeleStats.Utilities
{
    public struct FeatureFlag
    {
        private readonly string _name;

        public FeatureFlag(string name)
        {
            _name = name is null ? string.Empty : name.Trim().ToUpperInvariant();
        }

        public static FeatureFlag EnableTeleStats { get; } = new FeatureFlag("TELESTATS_ENABLED");

        public static bool operator ==(FeatureFlag featureFlagA, FeatureFlag featureFlagB) =>
            Equals(featureFlagA, featureFlagB);

        public static bool operator !=(FeatureFlag featureFlagA, FeatureFlag featureFlagB) =>
            !Equals(featureFlagA, featureFlagB);

        public override string ToString() =>
            _name ?? string.Empty;

        public override int GetHashCode() =>
            _name?.GetHashCode() ?? 0;

        public override bool Equals(object obj) =>
            Equals(_name, (obj is FeatureFlag other) ? other.ToString() : null);

        public bool Equals(FeatureFlag other) =>
            Equals(_name, other.ToString());
    }
}