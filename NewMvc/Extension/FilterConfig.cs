using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Extension
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // ExceptionLogAttribute继承自HandleError，主要作用是将异常信息写入日志系统中
            filters.Add(new ExceptionLogAttribute());
            ////默认的异常记录类
            filters.Add(new HandleErrorAttribute());
        }
    }
}