using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetConfigurationValueString(string Key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Key))
                return ConfigurationManager.AppSettings[Key];

            return "";
        }
    }
}
