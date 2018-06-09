using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProject
{
    class Program
    {
        private static string key = "test";
        static void Main(string[] args)
        {
            //var a = RedisCache.Instance.GetString("name");
            //Console.WriteLine(a);

            //RedisCache.Instance.InsertString("dd","1234");
            //var a = RedisCache.Instance.GetString("dd");
            //Console.WriteLine(a);
            var list = new List<User>();
            list=RedisCache.Instance.Get(key, () =>
             {
                 list = new List<User>()
                 {
                     new User() {Name="詹宝华",Sex="男",age=23 },
                     new User() {Name="测试",Sex="未知",age=11 },
                     new User() {Name="王宝强",Sex="女" }
                 };

                 return list;
             });
            foreach (var item in list)
            {
                Console.WriteLine($"姓名：{item.Name}\t性别：{item.Sex}\t年龄：{item.age}");
            }

            Console.ReadKey();
        }
    }

    public class User {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int age { get; set; }
    }


}
