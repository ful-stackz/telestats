namespace TeleStats.Utilities
{
    /// <summary>
    /// Represents a configuration flag.
    /// </summary>
    public struct ConfigFlag
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFlag" /> struct.
        /// </summary>
        /// <param name="name">
        /// The name of the flag.
        /// </param>
        public ConfigFlag(string name)
        {
            _name = name is null ? string.Empty : name.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Gets a <see cref="ConfigFlag" /> toggling the TeleStats enabled state.
        /// </summary>
        public static ConfigFlag EnableTeleStats { get; } = new ConfigFlag("TELESTATS_ENABLED");

        /// <summary>
        /// Gets a <see cref="ConfigFlag" /> toggling the batch writing state.
        /// </summary>
        public static ConfigFlag DisableBatchWriting { get; } = new ConfigFlag("DISABLE_BATCH_WRITING");

        /// <summary>
        /// Checks whether <paramref name="flagA" /> and <paramref name="flagB" />
        /// are equal.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether <paramref name="flagA" /> and
        /// <paramref name="flagB" /> are equal.
        /// </returns>
        public static bool operator ==(ConfigFlag flagA, ConfigFlag flagB) =>
            Equals(flagA, flagB);

        /// <summary>
        /// Checks whether <paramref name="flagA" /> and <paramref name="flagB" />
        /// are not equal.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether <paramref name="flagA" /> and
        /// <paramref name="flagB" /> are not equal.
        /// </returns>
        public static bool operator !=(ConfigFlag flagA, ConfigFlag flagB) =>
            !Equals(flagA, flagB);

        /// <inheritdoc />
        public override string ToString() =>
            _name ?? string.Empty;

        /// <inheritdoc />
        public override int GetHashCode() =>
            _name?.GetHashCode() ?? 0;

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            Equals(_name, (obj is ConfigFlag other) ? other.ToString() : null);

        /// <summary>
        /// Checks whether the specified <paramref name="other" /> <see cref="ConfigFlag" />
        /// is equal to this one.
        /// </summary>
        /// <returns>
        /// A <see cref="bool" /> indicating whether the specified <paramref name="other" /> <see cref="ConfigFlag" />
        /// and this one are equal.
        /// </returns>
        public bool Equals(ConfigFlag other) =>
            Equals(_name, other.ToString());
    }
}