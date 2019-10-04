using System;
using System.Runtime.Serialization;

namespace TeleStats
{
    /// <summary>
    /// Represents an error that occurred during an <see cref="IStatisticsWriter" /> operation.
    /// </summary>
    public class StatisticsWriterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsWriterException" /> class.
        /// </summary>
        public StatisticsWriterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsWriterException" /> class with
        /// the specified <paramref name="message" />.
        /// </summary>
        /// /// <param name="message">
        /// Describes the error that occurred.
        /// </param>
        public StatisticsWriterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsWriterException" /> class with
        /// the specified <paramref name="message" /> and <paramref name="inner" /> exception.
        /// </summary>
        /// <param name="message">
        /// Describes the error that occurred.
        /// </param>
        /// <param name="inner">
        /// The <see cref="Exception" /> that was the cause of this exception.
        /// </param>
        public StatisticsWriterException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsWriterException" /> class with the
        /// specified <paramref name="info" /> and <paramref name="context" />.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StatisticsWriterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}