using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc
{
    public class BaseHandleErrorAttribute: ActionFilterAttribute
    {
        public void OnActionExecuting(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true)
            {
                HttpException httpExce = filterContext.Exception as HttpException;
                if (httpExce != null && httpExce.GetHttpCode() != 500)//为什么要特别强调500 因为MVC处理HttpException的时候，如果为500 则会自动将其ExceptionHandled设置为true，那么我们就无法捕获异常
                {
                    return;
                }
            }
            Exception exception = filterContext.Exception;
            if (exception != null)
            {
                HttpException httpException = exception as HttpException;
                if (httpException != null)
                {
                    //网络错误
                    filterContext.Controller.ViewBag.UrlRefer = filterContext.HttpContext.Request.UrlReferrer;
                    int DataEroorCode = httpException.GetHttpCode();
                    if (DataEroorCode == 404)
                    {
                        filterContext.HttpContext.Response.Redirect("~/SysError/404");
                    }
                    else if (DataEroorCode == 500)
                    {
                        filterContext.HttpContext.Response.Redirect("~/SysError/500");
                    }
                    else
                        filterContext.HttpContext.Response.Redirect("~/SysError/" + DataEroorCode);

                    //写入日志 记录
                    filterContext.ExceptionHandled = true;//设置异常已经处理
                }
                else
                {
                    //编程或者系统错误，不处理，留给HandError处理
                }
            }
        }
    }
}