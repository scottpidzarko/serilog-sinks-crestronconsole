using Crestron.SimplSharp;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Serilog.Sinks.CrestronConsole
{
    public static class CrestronConsoleSinkExtensions
    {
        public static LoggerConfiguration CrestronConsoleSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new CrestronConsoleSink(formatProvider));
        }
    }
}
