using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.SerializedSections
{
    [Serializable]
    public abstract class BaseSettingsSection : ISettingsSection
    {
        public string GetSectionName()
        {
            return this.GetType().Name;
        }
    }
}
