using System.Globalization;
using TeleStats.Utilities;

namespace TeleStats.Samples
{
    /// <summary>
    /// Represents a sample ringbuffer statistics for CytoSMART Omni.
    /// </summary>
    public class ProcessRingBufferStats : StatisticsBase
    {
        private static int _index = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessRingBufferStats" /> class.
        /// </summary>
        /// <param name="writerFactory">
        /// Creates a new <see cref="IStatisticsWriter" /> instance.
        /// </param>
        public ProcessRingBufferStats(StatisticsWriterFactory writerFactory)
            : base(writerFactory)
        {
        }

        public Measurable StageInfoRetrieveDuration { get; } = new Measurable();
        public Measurable BufferReadDuration { get; } = new Measurable();
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public bool PositionHasValue { get; set; }
        public int StageInfoRetrieveRetries { get; set; }
        public bool StageInfoRetrieveSkipped { get; set; }
        public bool StageIsMoving { get; set; }

        /// <inheritdoc />
        public override string Header =>
            "Index," +
            "Timestamp," +
            "StageInfoRetrieveDuration," +
            "StageInfoRetrieveAttempts," +
            "StageInfoRetrieveSkipped," +
            "StageIsMoving," +
            "PositionX," +
            "PositionY," +
            "HasPosition," +
            "BufferReadDuration";

        /// <inheritdoc />
        public override string GetNextData() =>
            $"{_index++}," +
            $"{UnixTime.UtcUnixTimestamp}," +
            $"{StageInfoRetrieveDuration.Milliseconds}," +
            $"{StageInfoRetrieveRetries}," +
            $"{StageInfoRetrieveSkipped}," +
            $"{StageIsMoving}," +
            $"{PositionX.ToString("0.0000", CultureInfo.InvariantCulture)}," +
            $"{PositionY.ToString("0.0000", CultureInfo.InvariantCulture)}," +
            $"{PositionHasValue}," +
            $"{BufferReadDuration.Milliseconds}";

        /// <inheritdoc />
        public override void Save()
        {
            if (FeatureFlags.Has(FeatureFlag.EnableTeleStats))
            {
                base.Save();
            }
        }

        /// <inheritdoc />
        public override void Reset()
        {
            if (FeatureFlags.Has(FeatureFlag.EnableTeleStats))
            {
                StageInfoRetrieveDuration.Reset();
                BufferReadDuration.Reset();
                StageInfoRetrieveRetries = 0;
                StageInfoRetrieveSkipped = false;
                StageIsMoving = false;
                PositionX = default;
                PositionY = default;
                PositionHasValue = false;
                base.Reset();
            }
        }
    }
}