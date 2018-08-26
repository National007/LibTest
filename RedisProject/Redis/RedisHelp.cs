using System;
using System.Collections.Generic;
using RedisProject.Redis;
using ServiceStack.Redis;


namespace RedisProject.Redis
{
    public class RedisHelp : IDisposable
    {
        private IRedisClient _redisCli = null;

        /// <summary>
        /// 造函数
        /// </summary>
        /// <param name="OpenPooledRedis">是否开启缓冲池</param>
        public RedisHelp(bool OpenPooledRedis = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(CacheKey.RedisPassword))
                {
                    _redisCli = new RedisClient(CacheKey.RedisHost, CacheKey.RedisHostPort) { Password = CacheKey.RedisPassword, Db = CacheKey.RedisDB };
                }
                else
                {
                    _redisCli = new RedisClient(CacheKey.RedisHost, CacheKey.RedisHostPort) { Db = CacheKey.RedisDB };
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void setValue<T>(string key, T value, TimeSpan? expire = null)
        {
            try
            {
                if (expire != null)
                {
                    _redisCli.Set(key, value, expire.Value);
                }
                else
                {
                    _redisCli.Set(key, value);
                }
            }
            catch
            {

            }
        }

        public void setValueString(string key, string value, TimeSpan? expire = null)
        {
            try
            {
                if (expire != null)
                {
                    _redisCli.Set(key, value, expire.Value);
                }
                else
                {
                    _redisCli.Set(key, value);
                }
            }
            catch
            {

            }
        }

        public T getValue<T>(string key)
        {
            try
            {
                T value = _redisCli.Get<T>(key);
                return value;
            }
            catch
            {

            }
            return default(T);
        }

        /// <summary>
        /// 获取key,返回string格式
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getValueString(string key)
        {
            try
            {
                string value = _redisCli.GetValue(key);
                return value;
            }
            catch
            {

            }
            return "";
        }

        /// <summary>
        /// 获得某个hash型key下的所有字段
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<string> GetHashFields(string hashId)
        {
            try
            {
                List<string> hashFields = _redisCli.GetHashKeys(hashId);
                return hashFields;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 获得某个hash型key下的所有值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<string> GetHashValues(string hashId)
        {
            try
            {
                List<string> hashValues = _redisCli.GetHashKeys(hashId);
                return hashValues;
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// 获得hash型key某个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        public string GetHashField(string key, string field)
        {
            try
            {
                string value = _redisCli.GetValueFromHash(key, field);
                return value;
            }
            catch
            {

            }
            return "";
        }
        /// <summary>
        /// 设置hash型key某个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void SetHashField(string key, string field, string value)
        {
            try
            {
                _redisCli.SetEntryInHash(key, field, value);
            }
            catch
            {

            }
        }
        ///// <summary>
        /////使某个字段增加
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="field"></param>
        ///// <returns></returns>
        //public static void SetHashIncr(string key, string field, long incre)
        //{
        //    redisCli.IncrementValueInHash(key, field, incre);

        //}
        /// <summary>
        /// 向list类型数据添加成员，向列表底部(右侧)添加
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="list"></param>
        public void AddItemToListRight(string list, string item)
        {
            try
            {
                _redisCli.AddItemToList(list, item);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 从list类型数据读取所有成员
        /// </summary>
        public List<string> GetAllItems(string list)
        {
            try
            {
                List<string> listMembers = _redisCli.GetAllItemsFromList(list);
                return listMembers;
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// 从list类型数据指定索引处获取数据，支持正索引和负索引
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetItemFromList(string list, int index)
        {
            try
            {
                string item = _redisCli.GetItemFromList(list, index);
                return item;
            }
            catch
            {
            }
            return "";
        }
        /// <summary>
        /// 向列表底部（右侧）批量添加数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="values"></param>
        public void GetRangeToList(string list, List<string> values)
        {
            try
            {
                _redisCli.AddRangeToList(list, values);
            }
            catch
            { }
        }
        /// <summary>
        /// 向集合中添加数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="set"></param>
        public void GetItemToSet(string item, string set)
        {
            try
            {
                _redisCli.AddItemToSet(item, set);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 获得集合中所有数据
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public HashSet<string> GetAllItemsFromSet(string set)
        {
            try
            {
                HashSet<string> items = _redisCli.GetAllItemsFromSet(set);
                return items;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 获取fromSet集合和其他集合不同的数据
        /// </summary>
        /// <param name="fromSet"></param>
        /// <param name="toSet"></param>
        /// <returns></returns>
        public HashSet<string> GetSetDiff(string fromSet, params string[] toSet)
        {
            try
            {
                HashSet<string> diff = _redisCli.GetDifferencesFromSet(fromSet, toSet);
                return diff;
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 获得所有集合的并集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public HashSet<string> GetSetUnion(params string[] set)
        {
            try
            {
                HashSet<string> union = _redisCli.GetUnionFromSets(set);
                return union;
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 获得所有集合的交集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public HashSet<string> GetSetInter(params string[] set)
        {
            try
            {
                HashSet<string> inter = _redisCli.GetIntersectFromSets(set);
                return inter;
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 向有序集合中添加元素
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public void AddItemToSortedSet(string set, string value, long score)
        {
            try
            {
                _redisCli.AddItemToSortedSet(set, value, score);
            }
            catch
            { }
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的降序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long GetItemIndexInSortedSetDesc(string set, string value)
        {
            try
            {
                long index = _redisCli.GetItemIndexInSortedSetDesc(set, value);
                return index;
            }
            catch
            {
            }
            return -1;
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的升序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long GetItemIndexInSortedSet(string set, string value)
        {
            try
            {
                long index = _redisCli.GetItemIndexInSortedSet(set, value);
                return index;
            }
            catch
            {
            }
            return -1;
        }
        /// <summary>
        /// 获得有序集合中某个值得分数
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetItemScoreInSortedSet(string set, string value)
        {
            try
            {
                double score = _redisCli.GetItemScoreInSortedSet(set, value);
                return score;
            }
            catch
            {
            }
            return -1;
        }
        /// <summary>
        /// 获得有序集合中，某个排名范围的所有值
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginRank"></param>
        /// <param name="endRank"></param>
        /// <returns></returns>
        public List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
        {
            try
            {
                List<string> valueList = _redisCli.GetRangeFromSortedSet(set, beginRank, endRank);
                return valueList;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，升序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
        {
            try
            {
                List<string> valueList = _redisCli.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
                return valueList;
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，降序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        {
            try
            {
                List<string> vlaueList = _redisCli.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
                return vlaueList;
            }
            catch
            { }
            return null;
        }
        public void EnqueueItemOnList(string listId, string value)
        {
            _redisCli.EnqueueItemOnList(listId, value);
        }
        public string DequeueItemFromList(string listId)
        {
            return _redisCli.DequeueItemFromList(listId);
        }

        public string PopItemFromList(string listId)
        {
            return _redisCli.PopItemFromList(listId);
        }

        public void PushItemToList(string listId, string value)
        {
            _redisCli.PushItemToList(listId, value);
        }

        public bool Remove(string key)
        {
            return _redisCli.Remove(key);
        }

        public void FlushDB()
        {
            _redisCli.FlushDb();
        }
        public void FlushALL()
        {
            _redisCli.FlushAll();
        }

        //释放资源
        public void Dispose()
        {
            if (_redisCli != null)
            {
                _redisCli.Dispose();
                _redisCli = null;
            }
            GC.Collect();
        }
    }

}
