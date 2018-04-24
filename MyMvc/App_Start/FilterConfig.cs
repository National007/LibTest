﻿using System.Web;
using System.Web.Mvc;

namespace MyMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new BaseHandleErrorAttribute(),0);
            filters.Add(new HandleErrorAttribute(),1);
        }
    }
}
