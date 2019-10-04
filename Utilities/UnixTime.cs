using System;

namespace TeleStats.Utilities
{
    /// <summary>
    /// Provides functionality for working with UNIX timestamps.
    /// </summary>
    public static class UnixTime
    {
        private static readonly DateTime UnixEpochStart = new DateTime(year: 1970, month: 1, day: 1);

        /// <summary>
        /// Gets the current UTC time as a UNIX timestamp.
        /// </summary>
        public static long UtcUnixTimestamp =>
            (int)DateTime.UtcNow.Subtract(UnixEpochStart).TotalSeconds;
    }
}