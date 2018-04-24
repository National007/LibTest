using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Lib.Configuration
{
   public class Config
    {
        public static string GetAppSettingValue(string key)
        {
            return WebConfigurationManager.AppSettings[key]; 
        }
    }
}
