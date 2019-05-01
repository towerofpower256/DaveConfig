using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig
{
    public class OptionUpdatedEventArgs : EventArgs
    {
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
    }
}
