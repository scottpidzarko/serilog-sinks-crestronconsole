using Serilog.Events;
using Serilog.Formatting;
using System;
using System.IO;

namespace Serilog.Sinks.CrestronConsole
{
    internal class OutputTemplateRender : ITextFormatter
    {
        private string outputTemplate;
        private IFormatProvider formatProvider;

        public OutputTemplateRender(string outputTemplate, IFormatProvider formatProvider)
        {
            this.outputTemplate = outputTemplate;
            this.formatProvider = formatProvider;
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            throw new NotImplementedException();
        }
    }
}