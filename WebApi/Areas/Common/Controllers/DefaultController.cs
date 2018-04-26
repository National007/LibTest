using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Areas.Common.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// Api测试
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="limit">页容量</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic Index(object obj,int limit)
        {
            return Json(new {data=obj,count=limit });
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="a">变量a</param>
        /// <param name="b">变量b</param>
        /// <returns></returns>
        [HttpPost]
        public int Sum(int a,int b)
        {
            return a + b;
        }

    }
}