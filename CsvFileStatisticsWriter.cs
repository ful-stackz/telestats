using System;
using System.IO;
using TeleStats.Utilities;

namespace TeleStats
{
    /// <summary>
    /// Writes statistics in CSV format to a local .csv file.
    /// </summary>
    public class CsvFileStatisticsWriter : IStatisticsWriter
    {
        private static readonly int DefaultBatchSize = 90;

        private static readonly string LogFilename = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "TeleStats",
            "log.txt");
        
        private readonly string _filename;
        private readonly int _batchSize;
        
        private string _header;
        private string[] _batch;
        private int _batchIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFileStatisticsWriter" /> class.
        /// </summary>
        /// <param name="filename">
        /// Path to the file in which the CSV statistics will be stored.
        /// </param>
        public CsvFileStatisticsWriter(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException(
                    $"Argument '{nameof(filename)}' was null or empty.",
                    nameof(filename));
            }

            _filename = filename;

            _batchSize = ConfigFlags.Has(ConfigFlag.DisableBatchWriting) ? 1 : DefaultBatchSize;
            _batch = new string[_batchSize];

            Directory.CreateDirectory(Path.GetDirectoryName(filename));
        }

        /// <inheritdoc />
        public void Write(IStatistics stats)
        {
            if (_header is null)
            {
                _header = stats.Header;
            }
            
            _batch[_batchIndex++] = stats.GetNextData();

            if (_batchIndex >= _batchSize)
            {
                WriteToFile();

                _batch = new string[_batchSize];
                _batchIndex = 0;
            }
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_header is null)
            {
                return;
            }

            WriteToFile();
            _batch = new string[_batchSize];
            _batchIndex = 0;
        }

        private void WriteToFile()
        {
            try
            {
                if (!File.Exists(_filename))
                {
                    File.WriteAllText(_filename, _header + Environment.NewLine);
                }

                // File.AppendAllText(_filename, stats.GetNextData() + Environment.NewLine);
                File.AppendAllLines(_filename, _batch);
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilename));
                File.AppendAllText(LogFilename, $"[{DateTime.Now.ToString("s")}] [{ex.GetType()}] {ex.Message}");
            }
        }
    }
}