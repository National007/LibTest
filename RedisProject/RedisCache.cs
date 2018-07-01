
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProject
{
    public class RedisCache
    {
        //redis 服务地址
        private string redisServerAddress;
        //redis 服务默认超时时间（分钟）
        private int redisServerTimeoutMins;

        private ConnectionMultiplexer connectionMultiplexer;
        private IDatabase database;

        private static RedisCache instance;

        #region 单例
        /// <summary>
        /// 构造函数
        /// </summary>
        private RedisCache()
        {
            redisServerAddress = "127.0.0.1:8888,allowAdmin=true,password=123456";
            //redisServerAddress = "127.0.0.1:6379,allowAdmin=true,password=123456";
            //redisServerAddress = "192.168.0.111:8888,allowAdmin=true,password=123456";
            redisServerTimeoutMins = 60;

            if (redisServerAddress == null || string.IsNullOrWhiteSpace(redisServerAddress))
                throw new ApplicationException("配置文件中未找到RedisServer的有效配置");

            connectionMultiplexer = ConnectionMultiplexer.Connect(redisServerAddress);
            database = connectionMultiplexer.GetDatabase();
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        public static RedisCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RedisCache();
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            string result = "";
            RedisValue redisValue = database.StringGet(key);
            if (!redisValue.IsNull)
            {
                result = redisValue.ToString();
            }
            return result;
        }

        /// <summary>
        /// 取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            T result = default(T);
            RedisValue redisValue = database.StringGet(key);
            if (!redisValue.IsNull)
            {
                result = JsonFormatter.ToObj<T>(redisValue.ToString());
            }

            return result;
        }

        /// <summary>
        /// 取对象
        /// 如果缓存中不存在，则调用getDataFun取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getDataFun"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> getDataFun)
        {
            if (Exists(key))
            {
                return Get<T>(key);
            }
            else
            {
                var result = getDataFun();
                Insert(key, result);
                return result;
            }
        }

        /// <summary>
        /// 插入字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void InsertString(string key, string value)
        {
            TimeSpan timeoutSpan = new TimeSpan(0, redisServerTimeoutMins, 0);
            database.StringSet(key, value, timeoutSpan);
        }

        /// <summary>
        /// 插入字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="mins"></param>
        public void InsertString(string key, string value, int mins)
        {
            TimeSpan timeoutSpan = new TimeSpan(0, mins, 0);
            database.StringSet(key, value, timeoutSpan);
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(string key, object value)
        {
            string jsonStr = JsonFormatter.ToString(value);
            TimeSpan timeoutSpan = new TimeSpan(0, redisServerTimeoutMins, 0);
            database.StringSet(key, jsonStr, timeoutSpan);
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="mins">过期时间（分钟）</param>
        public void Insert(string key, object value, int mins)
        {
            string jsonStr = JsonFormatter.ToString(value);
            TimeSpan timeoutSpan = new TimeSpan(0, mins, 0);
            database.StringSet(key, jsonStr, timeoutSpan);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }

        /// <summary>
        /// 从缓存中移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        public void RemoveAll()
        {
            var endpoints = connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }


    }
    /// <summary>
    /// Json格式化类
    /// </summary>
    public class JsonFormatter
    {
        /// <summary>
        /// obj to json string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(Object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// json string to obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToObj<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
