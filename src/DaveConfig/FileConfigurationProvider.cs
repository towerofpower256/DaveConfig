using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DaveConfig
{
    public class FileConfigurationProvider : ConfigurationProviderBase
    {
        IConfigurationFileStore _configStore;
        OptionsCollection _options;
        ReaderWriterLockSlim _lock;

        public FileConfigurationProvider(string configFilename)
        {
            _lock = new ReaderWriterLockSlim();
            _options = new OptionsCollection();
            _configStore = new XmlConfigurationFileStore(configFilename);
        }

        public override ICollection<string> GetOptions()
        {
            _lock.EnterReadLock();
            try
            {
                return _options.Keys;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public override string GetOption(string optionName)
        {
            return GetOption(optionName, String.Empty);
        }

        public override string GetOption(string optionName, string defaultValue)
        {
            _lock.EnterReadLock();
            try
            {
                return _options.ContainsKey(optionName) ? _options[optionName] : defaultValue;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public override void SetOption(string optionName, object value)
        {
            var valueAsString = value.ToString();

            _lock.EnterWriteLock();
            try
            {
                _options[optionName] = valueAsString;

            }
            finally
            {
                _lock.ExitWriteLock();
            }

            DoOptionUpdatedEvent(optionName, valueAsString);
        }

        public override void LoadConfiguration()
        {
            _lock.EnterWriteLock();
            try
            {
                _options = _configStore.LoadConfiguration();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public override void SaveConfiguration()
        {
            _lock.EnterReadLock();
            try
            {
                _configStore.SaveConfiguration(_options);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
