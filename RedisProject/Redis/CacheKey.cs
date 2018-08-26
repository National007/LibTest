using System;
using System.Configuration;

namespace RedisProject.Redis
{
    public class CacheKey
    {
        public static string RedisHost = ConfigurationManager.AppSettings["RedisHost"];

        public static int RedisHostPort = Convert.ToInt32(ConfigurationManager.AppSettings["RedisHostPort"]);

        public static string RedisPassword = ConfigurationManager.AppSettings["RedisPassword"];
        
        public static readonly int RedisDB = Convert.ToInt32(ConfigurationManager.AppSettings["RedisDB"] ?? "3");

    }
}
