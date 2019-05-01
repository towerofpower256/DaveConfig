using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig
{
    interface IConfigurationFileStore
    {
        void SaveConfiguration(OptionsCollection options);
        OptionsCollection LoadConfiguration();
    }
}
