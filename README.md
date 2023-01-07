# Serilog.Sinks.CrestronConsole 

A Serilog sink that writes log events to the Text Console of a Crestron 4-series appliance such as a CP4(N) or RMC4. The default output is plain text; JSON formatting can be plugged in using a package such as [_Serilog.Formatting.Compact_](https://github.com/serilog/serilog-formatting-compact).

This repository was built upon the efforts made by the folks behind the [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) repository. 

It is our intention to maintain as much feature parity with the Serilog.Sinks.Console package where possible.

### Getting started

To use the Crestron console sink, first install the <s>[NuGet package](https://nuget.org/packages/serilog.sinks.crestronconsole):

```shell
dotnet add package Serilog.Sinks.CrestronConsole
```
</s>

Nuget package support is ready, however there is already a closed source one on the nuget marketplace written by Dario Dusper. Attempts to get his project open source or otherwise hand the package name over to me have been unsuccessful so far.

Then enable the sink using `WriteTo.CrestronConsole()`:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.CrestronConsole()
    .CreateLogger();

Log.Information("Hello world");
```

When connected via ssh or using toolbox text console tool, you'll see the output:

```
[12:50:51 INF] Hello, world!
```

### Themes

The sink will not colorize output by default as Crestron Toolbox doesn't support colorized output. 

If colorized output is configured it will result in output like this:

![Colorized Console](https://raw.githubusercontent.com/serilog/serilog-sinks-console/dev/assets/Screenshot.png)

Themes can be specified when configuring the sink:

```csharp
    .WriteTo.CrestronConsole(theme: AnsiConsoleTheme.Code)
```

The following built-in themes are available:

 * `ConsoleTheme.None` - no styling
 * `SystemConsoleTheme.Literate` - styled to replicate _Serilog.Sinks.Literate_, using the `System.Console` coloring modes supported on all Windows/.NET targets; **this is the default when no theme is specified**
 * `SystemConsoleTheme.Grayscale` - a theme using only shades of gray, white, and black
 * `AnsiConsoleTheme.Literate` - an ANSI 256-color version of the "literate" theme
 * `AnsiConsoleTheme.Grayscale` - an ANSI 256-color version of the "grayscale" theme
 * `AnsiConsoleTheme.Code` - an ANSI 256-color Visual Studio Code-inspired theme
 * `AnsiConsoleTheme.Sixteen` - an ANSI 16-color theme that works well with both light and dark backgrounds

 Adding a new theme is straightforward; examples can be found in the [`SystemConsoleThemes`](https://github.com/serilog/serilog-sinks-console/blob/dev/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/SystemConsoleThemes.cs) and [`AnsiConsoleThemes`](https://github.com/serilog/serilog-sinks-console/blob/dev/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/AnsiConsoleThemes.cs) classes.

### Output templates

The format of events to the console can be modified using the `outputTemplate` configuration parameter:

```csharp
    .WriteTo.CrestronConsole(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
```

The default template, shown in the example above, uses built-in properties like `Timestamp` and `Level`. Properties from events, including those attached using [enrichers](https://github.com/serilog/serilog/wiki/Enrichment), can also appear in the output template.

### JSON output

The sink can write JSON  output instead of plain text. `CompactJsonFormatter` or `RenderedCompactJsonFormatter` from [Serilog.Formatting.Compact](https://github.com/serilog/serilog-formatting-compact) is recommended:

```shell
dotnet add package Serilog.Formatting.Compact
```

Pass a formatter to the `CrestronConsole()` configuration method:

```csharp
    .WriteTo.CrestronConsole(new RenderedCompactJsonFormatter())
	
```

Output theming is not available when custom formatters are used.

### XML `<appSettings>` configuration

To use the console sink with the [Serilog.Settings.AppSettings](https://github.com/serilog/serilog-settings-appsettings) package, first install that package if you haven't already done so:

```shell
dotnet add package Serilog.Settings.AppSettings
```

Instead of configuring the logger in code, call `ReadFrom.AppSettings()`:

```csharp
var log = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .CreateLogger();
```

In your application's `App.config` or `Web.config` file, specify the console sink assembly under the `<appSettings>` node:

```xml
<configuration>
  <appSettings>
    <add key="serilog:using:CrestronConsole" value="Serilog.Sinks.CrestronConsole" />
    <add key="serilog:write-to:CrestronConsole" />
```


To configure the console sink with a different theme and include the `SourceContext` in the output, change your `App.config`/`Web.config` to:
```xml
<configuration>
  <appSettings>
    <add key="serilog:using:CrestronConsole" value="Serilog.Sinks.CrestronConsole" />
    <add key="serilog:write-to:CrestronConsole.theme" value="Serilog.Sinks.CrestronSystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.CrestronConsole" />
    <add key="serilog:write-to:CrestronConsole.outputTemplate" value="[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} &lt;s:{SourceContext}&gt;{NewLine}{Exception}" />
```

### JSON `appsettings.json` configuration

To use the console sink with _Microsoft.Extensions.Configuration_, use the [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration) package. First install that package if you have not already done so:

```shell
dotnet add package Serilog.Settings.Configuration
```

Instead of configuring the sink directly in code, call `ReadFrom.Configuration()`:

```csharp
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
```

In your `appsettings.json` file, under the `Serilog` node, :
```json
{
  "Serilog": {
    "WriteTo": [{"Name": "CrestronConsole"}]
  }
}
```

To configure the console sink with a different theme and include the `SourceContext` in the output, change your `appsettings.json` to:
```json
{
  "Serilog": {
    "WriteTo": [
      {
          "Name": "CrestronConsole",
          "Args": {
            "theme": "Serilog.Sinks.CrestronSystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.CrestronConsole",
            "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}"
          }
      }
    ]
  }
}
```


### Performance

Console logging is synchronous and this can cause bottlenecks in some deployment scenarios. For high-volume console logging, consider using [_Serilog.Sinks.Async_](https://github.com/serilog/serilog-sinks-async) to move console writes to a background thread:

```csharp
// dotnet add package serilog.sinks.async

Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(wt => wt.CrestronConsoleSink())
    .CreateLogger();
```

### Contributing

See CONTRIBUTING.md

_Copyright &copy; All Contributers - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
