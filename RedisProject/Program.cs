using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = RedisCache.Instance.GetString("name");
            //Console.WriteLine(a);

            RedisCache.Instance.InsertString("dd","z");
            var a = RedisCache.Instance.GetString("dd");
            Console.WriteLine(a);

            Console.ReadKey();
        }
    }
}
