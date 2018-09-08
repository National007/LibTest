using RedisProject.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace RedisProject
{
    class Program
    {
        private static string key = "df";
        static void Main(string[] args)
        {
            //var a = RedisCache.Instance.GetString("name");
            //Console.WriteLine(a);

            //RedisCache.Instance.InsertString("dd","1234");
            //var a = RedisCache.Instance.GetString("dd");
            //Console.WriteLine(a);
            //var list = new List<User>();
            //list=RedisCache.Instance.Get(key, () =>
            // {
            //     list = new List<User>()
            //     {
            //         new User() {Name="詹宝华1",Sex="男",age=23 },
            //         new User() {Name="测试2",Sex="未知",age=11 },
            //         new User() {Name="王宝强3",Sex="女" }
            //     };

            //     return list;
            // });
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"姓名：{item.Name}\t性别：{item.Sex}\t年龄：{item.age}");
            //}

            RedisHelp redis = new RedisHelp();
            var list = new List<User>()
             {
                      new User() {Name="詹宝华1",Sex="男",age=23 },
                      new User() {Name="测试2",Sex="未知",age=11 },
                      new User() {Name="王宝强3",Sex="女" }
             };
            redis.setValue<List<User>>("whys",list);
            var whys = redis.getValue<List<User>>("whys");
            if (whys != null && whys.Count > 0)
            {
                whys.ForEach(f=>Console.WriteLine($"{f.Name}\t{f.Sex}\t{f.age}"));
            }
            else
            {
                list.ForEach(f => Console.WriteLine($"{f.Name}\t{f.Sex}\t{f.age}"));
            }

            //whys.ForEach(a => Console.WriteLine($"{a.Name}\t{a.Sex}\t{a.age}"));
            // redis.FlushALL();
            //redis.FlushDB();

            //redis.setValueString("test","zhanbaohua",new TimeSpan(0,0,5));
            Console.WriteLine(redis.getValueString("test"));
            


            Console.ReadKey();
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int age { get; set; }
    }


}
