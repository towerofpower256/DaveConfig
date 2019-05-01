using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig
{
    public interface IConfigurationProvider
    {
        void LoadConfiguration();
        void SaveConfiguration();
        ICollection<string> GetOptions();
        string GetOption(string optionName);
        void SetOption(string optionName, object value);

        event EventHandler<OptionUpdatedEventArgs> OnOptionUpdated;
        event EventHandler OnConfigurationSaved;
        event EventHandler OnConfigurationLoaded;
    }
}
