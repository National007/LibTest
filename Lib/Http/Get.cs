using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lib.Http
{
   public class Get
    {
        #region  //HTTP GET请求
        /// <summary>
        /// HTTP GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "*/*";
            using (var res = (HttpWebResponse)req.GetResponse())
            {
                using (var reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion

        #region  //GET 获取数据
        public static T GetResult<T>(string url)
        {
            var retdata = HttpGet(url);
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        #endregion


    }
}
