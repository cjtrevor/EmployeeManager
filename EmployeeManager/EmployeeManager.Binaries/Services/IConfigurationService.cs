﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public interface IConfigurationService
    {
        string GetConfigurationValueString(string Key);
    }
}
