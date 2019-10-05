using System;
using System.Collections.Generic;
using System.Linq;
using TeleStats.Utilities;

namespace TeleStats.Generics
{
    /// <summary>
    /// Provides custom string formatting for a <paramref name="value"/> of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type.
    /// </typeparam>
    /// <param name="value">
    /// The value that must be converted to a <see cref="string"/>.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> representation of the specified <paramref name="value"/>.
    /// </returns>
    public delegate string StringFormatter<T>(T value);

    /// <summary>
    /// Provides custom string formatting for a <see cref="Measurable.Milliseconds"/> property of a
    /// <see cref="Measurable"/> stat.
    /// </summary>
    /// <param name="milliseconds">
    /// The milliseconds value that must be converted to a <see cref="string"/>.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> representation of the specified <paramref name="milliseconds"/>.
    /// </returns>
    public delegate string MeasurableStringFormatter(long milliseconds);

    /// <summary>
    /// Provides custom reset logic for a stat. Provides the <paramref name="currentValue"/> and expects the reset
    /// value to be returned.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the <paramref name="currentValue"/> and the expected, reset value.
    /// </typeparam>
    /// <param name="currentValue">
    /// The current value of the stat.
    /// </param>
    /// <returns>
    /// The value to be set to a stat after resetting it.
    /// </returns>
    public delegate T ValueResetter<T>(T currentValue);

    /// <summary>
    /// Represents a collection of generic dynamic stats. Stats can be added and their values set without implementing
    /// a new class dericed from <see cref="StatisticsBase" />.
    /// </summary>
    public class GenericStats : StatisticsBase
    {
        private readonly IList<GenericStat> _stats = new List<GenericStat>();

        /// <inheritdoc />
        public override string Header =>
            string.Join(",", _stats.Select(x => x.Name));

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericStats" /> class.
        /// </summary>
        /// <param name="writerFactory">
        /// Creates a new <see cref="IStatisticsWriter" /> instance when invoked.
        /// </param>
        public GenericStats(StatisticsWriterFactory writerFactory)
            : base(writerFactory)
        {
        }

        /// <summary>
        /// Starts tracking a new stat with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">
        /// Identifies the stat.
        /// </param>
        /// <param name="value">
        /// Optional initial value.
        /// </param>
        /// <param name="formatter">
        /// Optional custom formatting to be applied to the value of the stat when saving it.
        /// </param>
        /// <param name="resetter">
        /// Optional resetting logic, invoked when resetting the value of the stat.
        /// </param>
        /// <typeparam name="T">
        /// Specifies the value type of the stat.
        /// </typeparam>
        /// <exception cref="InvalidOperationException">
        /// When a stat identified by the specified <paramref name="name" /> already exists.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// When a stat with the specified <paramref name="name" /> does not exist.
        /// </exception>
        public void Add<T>(
            string name,
            T value = default,
            StringFormatter<T> formatter = null,
            ValueResetter<T> resetter = null)
        {
            var preparedName = PrepareName(name);

            if (_stats.Any(x => x.Name == preparedName))
            {
                throw new InvalidOperationException($"Stat with name '{name}' already exists.");
            }

            StringFormatter<object> preparedFormatter = formatter is null
                ? new StringFormatter<object>(v => v.ToString())
                : new StringFormatter<object>(v => formatter((T)v));

            ValueResetter<object> preparedResetter = resetter is null
                ? new ValueResetter<object>(_ => default(T))
                : new ValueResetter<object>(v => resetter((T)v));

            _stats.Add(new GenericStat(
                name: preparedName,
                value: value,
                formatter: preparedFormatter,
                resetter: preparedResetter));
        }

        /// <summary>
        /// Sets the value of the stat with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">
        /// Identifies the stat whose value to set.
        /// </param>
        /// <param name="value">
        /// The next value of the stat.
        /// </param>
        /// <typeparam name="T">
        /// Specifies the value type of the stat.
        /// </typeparam>
        public void Set<T>(string name, T value)
        {
            var stat = _stats.FirstOrDefault(x => x.Name == PrepareName(name));

            if (stat is null)
            {
                throw new KeyNotFoundException($"Stat with name '{name}' could not be found.");
            }

            stat.Value = value;
        }

        /// <summary>
        /// Starts tracking a new <see cref="Measurable" /> stat with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">
        /// Identifies the stat.
        /// </param>
        /// <param name="formatter">
        /// Optional custom formatting to be applied to the amount of milliseconds of the <see cref="Measurable" /> stat when saving it.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// When a stat identified by the specified <paramref name="name" /> already exists.
        /// </exception>
        public void AddMeasurable(string name, MeasurableStringFormatter formatter = null)
        {
            var preparedName = PrepareName(name);

            if (_stats.Any(x => x.Name == preparedName))
            {
                throw new InvalidOperationException($"Stat with name '{name}' already exists.");
            }

            StringFormatter<object> preparedFormatter = formatter is null
                ? new StringFormatter<object>(v => (v as Measurable).Milliseconds.ToString("0"))
                : new StringFormatter<object>(v => formatter((v as Measurable).Milliseconds));

            _stats.Add(new GenericStat(
                name: preparedName,
                value: new Measurable(),
                formatter: preparedFormatter,
                resetter: (val) =>
                {
                    (val as Measurable).Reset();
                    return val;
                }));
        }

        /// <summary>
        /// Retrieves a <see cref="Measurable" /> stat with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">
        /// Identifies the stat whose value to be retrieved.
        /// </param>
        /// <returns>
        /// A <see cref="Measurable" /> instance.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// When a stat with the specified <paramref name="name" /> does not exist.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// When the stat identified by the specified <paramref name="name" /> is not a <see cref="Measurable" />.
        /// </exception>
        public Measurable GetMeasurable(string name)
        {
            var stat = _stats.FirstOrDefault(x => x.Name == PrepareName(name));

            if (stat is null)
            {
                throw new KeyNotFoundException($"Measurable stat with name '{name}' could not be found.");
            }

            return stat.Value is Measurable measurable
                ? measurable
                : throw new InvalidOperationException($"Stat with name '{name}' is not a {typeof(Measurable)}.");
        }
        
        private static string PrepareName(string name) =>
            name.Trim().ToUpperInvariant();

        /// <inheritdoc />
        public override string GetNextData() =>
            string.Join(",", _stats.Select(x => x.Format()));

        /// <inheritdoc />
        public override void Save()
        {
            if (FeatureFlags.Has(FeatureFlag.EnableTeleStats))
            {
                base.Save();
            }
        }

        /// <inheritdoc />
        public override void Reset() =>
            _stats.ToList().ForEach(x => x.Reset());

        private class GenericStat
        {
            private readonly StringFormatter<object> _formatter;
            private readonly ValueResetter<object> _resetter;

            public GenericStat(
                string name,
                object value,
                StringFormatter<object> formatter,
                ValueResetter<object> resetter)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException($"Argument {nameof(name)} was null or empty.", nameof(name));
                }

                Name = name;
                Value = value ?? string.Empty;
                _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
                _resetter = resetter ?? throw new ArgumentNullException(nameof(resetter));
            }

            public string Name { get; }

            public object Value { get; set; }

            public string Format() =>
                _formatter(Value);

            public void Reset() =>
                Value = _resetter(Value);
        }
    }
}