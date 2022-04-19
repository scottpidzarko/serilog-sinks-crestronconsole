using Crestron.SimplSharp;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Serilog.Sinks.CrestronConsole
{
    public class CrestronConsoleSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private object _consoleLock;

        public CrestronConsoleSink(IFormatProvider formatProvider)
        {
            _consoleLock = new object();
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            lock (_consoleLock)
            {
                Crestron.SimplSharp.CrestronConsole.PrintLine(DateTimeOffset.Now.ToString() + " " + message);
            }
        }
    }
}
