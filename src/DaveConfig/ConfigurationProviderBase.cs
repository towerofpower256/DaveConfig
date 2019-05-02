using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig
{
    public abstract class ConfigurationProviderBase : IConfigurationProvider
    {
        public event EventHandler<OptionUpdatedEventArgs> OnOptionUpdated;
        public event EventHandler OnConfigurationSaved;
        public event EventHandler OnConfigurationLoaded;

        public abstract void LoadConfiguration();
        public abstract void SaveConfiguration();
        public abstract ICollection<string> GetOptions();
        public abstract string GetOption(string optionName);
        public abstract string GetOption(string optionName, string defaultValue);
        public abstract void SetOption(string optionName, object value);

        public void DoOptionUpdatedEvent(string optionName, string newValue)
        {
            OnOptionUpdated?.Invoke(this, new OptionUpdatedEventArgs() { OptionName = optionName, OptionValue = newValue });
        }

        public void DoConfigurationLoaded()
        {
            OnConfigurationLoaded?.Invoke(this, new EventArgs());
        }

        public void DoConfigurationSaved()
        {
            OnConfigurationSaved?.Invoke(this, new EventArgs());
        }
    }
}
