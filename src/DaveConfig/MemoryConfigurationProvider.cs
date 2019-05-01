using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DaveConfig
{
    public class MemoryConfigurationProvider : ConfigurationProviderBase
    {
        OptionsCollection _options;
        ReaderWriterLockSlim _lock;

        public MemoryConfigurationProvider()
        {
            _options = new OptionsCollection();
            _lock = new ReaderWriterLockSlim();
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
            _lock.EnterReadLock();
            try
            {
                return _options.ContainsKey(optionName) ? _options[optionName] : String.Empty;
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
            throw new NotImplementedException();
        }

        public override void SaveConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
