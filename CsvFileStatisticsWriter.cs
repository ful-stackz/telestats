using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TeleStats
{
    /// <summary>
    /// Writes statistics in CSV format to a local .csv file.
    /// </summary>
    public class CsvFileStatisticsWriter : IStatisticsWriter
    {
        private static readonly int BatchSize = 90;
        
        private readonly ILogger<CsvFileStatisticsWriter> _logger;
        private readonly string _filename;
        
        private bool _canWrite;
        private string _header;
        private string[] _batch = new string[BatchSize];
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

            _logger = Configuration.Get.CreateLogger<CsvFileStatisticsWriter>();
            _filename = filename;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                _canWrite = true;
            }
            catch (Exception ex)
            {
                _canWrite = false;
                _logger.LogError(ex, $"Could not create directory for {filename}.");
            }
        }

        /// <inheritdoc />
        public void Write(IStatistics stats)
        {
            if (!_canWrite)
            {
                _logger.LogWarning("Resetting writinig batch, because writer cannot write.");
                Reset();
                return;
            }

            if (_header is null)
            {
                _header = stats.Header;
            }
            
            _batch[_batchIndex++] = stats.GetNextData();

            if (_batchIndex == BatchSize)
            {
                WriteToFile();

                _batch = new string[BatchSize];
                _batchIndex = 0;
            }
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (!_canWrite)
            {
                _logger.LogWarning("Resetting writinig batch, because writer cannot write.");
                Reset();
                return;
            }

            if (_header is null)
            {
                return;
            }

            WriteToFile();
            Reset();
        }

        private void WriteToFile()
        {
            try
            {
                if (!File.Exists(_filename))
                {
                    File.WriteAllText(_filename, _header + Environment.NewLine);
                }

                File.AppendAllLines(_filename, _batch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not store stats data.");
            }
        }

        private void Reset()
        {
            _batch = new string[BatchSize];
            _batchIndex = 0;
        }
    }
}