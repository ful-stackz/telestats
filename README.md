# telestats
Low-overhead, simple telemetry/statistics library.

## Simple stats, fast solutions
Sometimes you need to measure the execution time of a method or track the average value of a very important property. Sometimes
you need this information fast and it doesn't make sense to bother yourself with full-scale telemetry frameworks. Sometimes a one-run
measurements are all thats needed to fix that bug. **Enter _telestats_!**

## Configuration
Stats are not always required and it doesn't make sense to change the code every time you want them enabled or disabled. For that reason they `telestats` are configurable through Command Line Arguments and Environment Variables! You only need to make sure to specify which one to use, or why not both?! It's recommended to do the configuration somewhere in the top layers of the application and add stats in deeper layers without depending on the configuration.

**Note:** By default, `telestats` are **disabled**, in order to enable them you need to provide `TELESTATS_ENABLED` either as an environment variable or CLA.

```csharp
// Use environment variable with the key 'TELESTATS' with values separated by ';'
// For example, the environment variable TELESTATS=TELESTATS_ENABLED will be matched
TeleStats.Configuration.Get
  .UseEnvironmentVariables("TELESTATS")
  .Build();

// Use CLAs starting with -telestats:
// For example, the CLA -telestats:TELESTATS_ENABLED will be matched
TeleStats.Configuration.Get
  .UseCommandLineArguments(prefix: "-telestats:")
  .Build();
  
// Extra awesomeness - you can use both by chaining!
TeleStats.Configuration.Get
  .UseEnvironmentVariables("TELESTATS")
  .UseCommandLineArguments(prefix: "-telestats:")
  .Build();

// Extra extra awesomeness - you can provide hard-coded values!
TeleStats.Configuration.Get
  .UseCommandLineArguments(prefix: "-telestats:")
  .AddCommandLineArguments("-telestats:TELESTATS_ENABLED")
  .Build();
```

## Generic stats
For quick and simple measurements you'd find it painless to use the `GenericStats` implementation. It allows defining the data you
want to track/measure dynamically and storing it for you. Unlike the fully functional method of deriving from `StatisticsBase` or
`IStatistics` and providing your fully customized implementations, `GenericStats` is a _quick & dirty_ way of getting some stats from
your code. Definitely not intended for `git push`!

```csharp
// 1. Use TeleStats
using TeleStats;
using TeleStats.Factories;
using TeleStats.Generics;

// ...

// 2. Create a GenericStats instance for quick measurements -or-
// derive from StatisticsBase for more complicated scenarios
private readonly GenericStats _stats = new GenericStats(
  DefaultWriterFactory.CsvFileWriter(
    builder => builder.PathFrom(builder.Desktop, "MyAwesomeApp", "Stats.csv"));

// ..

// 3. Define the data you want to track
// Note: you should add the data in the beginning and not temper with it in the rest of the code
_stats.Add<int>("attempts");
_stats.Add<bool>("processing_failed");
_stats.Add<int>("processed_items");
_stats.AddMeasurable("processing_duration");

// ..

// 4. Get to work!
_stats.GetMeasurable("processing_duration").Start();
try
{
  var items = ProcessItems();
  _stats.Set("processed_items", items.Count);
}
catch (Exception)
{
  _stats.Set("processing_failed", true);
}
_stats.GetMeasurable("processing_duration").Stop();
_stats.Save();
_stats.Reset();
```
