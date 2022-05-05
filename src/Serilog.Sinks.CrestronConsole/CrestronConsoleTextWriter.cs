using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crestron.SimplSharp;

namespace Serilog.Sinks.CrestronSystemConsole
{
    public class CrestronConsoleTextWriter : TextWriter
    {
        
        public CrestronConsoleTextWriter()
        {
        }

        public CrestronConsoleTextWriter(IFormatProvider formatProvider) : base(formatProvider)
        {
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }

        public override void Write(char value)
        {
            if (FormatProvider != null)
            {
                CrestronConsole.Print(value.ToString(FormatProvider));
            }
            else
            {
                CrestronConsole.Print(value.ToString());
            }
        }

        public override void Write(string value)
        {
            CrestronConsole.Print(value);
        }

        public override void Write(string message, params object[] args)
        {
            CrestronConsole.Print(message, args);
        }

        public override void WriteLine()
        {
            CrestronConsole.PrintLine("");
        }

        public override void WriteLine(string value)
        {
            CrestronConsole.PrintLine(value);
        }

        public override void WriteLine(string message, params object[] args)
        {
            CrestronConsole.PrintLine(message, args);
        }

        public override void Flush()
        {
            // Do nothing since we don't flush Crestron Console
        }
    }
}
