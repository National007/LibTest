using System.Configuration;

namespace RedisProject.Redis
{
    public class RedisConfig: ConfigurationSection
    {
        public static RedisConfig GetConfig()
        {
            RedisConfig section = GetConfig("RedisConfig");
            return section;
        }

        public static RedisConfig GetConfig(string sectionName)
        {
            RedisConfig section = (RedisConfig)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        [ConfigurationProperty("WriteServerConStr", IsRequired = false)]
        public string WriteServerConStr
        {
            get
            {
                return (string)base["WriteServerConStr"];
            }
            set
            {
                base["WriteServerConStr"] = value;
            }
        }


        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        [ConfigurationProperty("ReadServerConStr", IsRequired = false)]
        public string ReadServerConStr
        {
            get
            {
                return (string)base["ReadServerConStr"];
            }
            set
            {
                base["ReadServerConStr"] = value;
            }
        }


    }
}
