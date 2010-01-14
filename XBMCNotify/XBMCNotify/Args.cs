using System;
using System.Collections.Generic;
using System.Text;
using Unified.CommandLine;

namespace XBMCNotify
{
    public class Args
    {
        [Argument(ArgumentType.AtMostOnce, HelpText = "The Caption or Title.", ShortName="c")]
        public string Caption;
        [Argument(ArgumentType.AtMostOnce, HelpText = "The Message body.", ShortName="m")]
        public string Message;
    }
}
