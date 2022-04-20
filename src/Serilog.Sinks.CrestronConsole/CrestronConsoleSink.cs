using Crestron.SimplSharp;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using System;

namespace Serilog.Sinks.CrestronConsole
{
    public class CrestronConsoleSink : ILogEventSink
    {
        readonly LogEventLevel? _crestronErrorLogFromLevel;
        readonly ITextFormatter _formatter;
        readonly object _syncRoot;

        const int DefaultWriteBufferCapacity = 256;

        public CrestronConsoleSink(ITextFormatter formatter,
            LogEventLevel? crestronErrorLogFromLevel,
            object syncRoot)
        {
            _crestronErrorLogFromLevel = crestronErrorLogFromLevel;
            _formatter = formatter;
            _syncRoot = syncRoot ?? throw new ArgumentNullException(nameof(syncRoot));
        }

        public void Emit(LogEvent logEvent)
        {
            /*var output = SelectOutputStream(logEvent.Level);

            // ANSI escape codes can be pre-rendered into a buffer; however, if we're on Windows and
            // using its console coloring APIs, the color switches would happen during the off-screen
            // buffered write here and have no effect when the line is actually written out.
            if (_theme.CanBuffer)
            {
                var buffer = new StringWriter(new StringBuilder(DefaultWriteBufferCapacity));
                _formatter.Format(logEvent, buffer);
                var formattedLogEventText = buffer.ToString();
                lock (_syncRoot)
                {
                    output.Write(formattedLogEventText);
                    output.Flush();
                }
            }
            else
            {
                lock (_syncRoot)
                {
                    _formatter.Format(logEvent, output);
                    output.Flush();
                }
            } */
        }
    }
}
