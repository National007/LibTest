﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult WebApiDemo()
        {
            return View();
        }

        public ActionResult WebServicesDemo()
        {
            var i = ServicesDemo("3","5");
            return View();
        }
        public int ServicesDemo(string a, string b)
        {
            TestService.TestDemo webservice = new TestService.TestDemo();
            int sum = webservice.Sum(a, b);
            return sum;
        }
        

        public ActionResult List(string id)
        {
            //try
            //{
            //    var s = list.Where(m => m.Id == id).FirstOrDefault().Name;
            //}
            //catch (Exception ex)
            //{
            //     Lib.Tool.LogHelp.WriteLogFile(ex.Message);
            //}

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult loadData()
        {
            var list = new List<Test>
            {
                new Test(){Id="1",Name="张三"},
                new Test(){Id="2",Name="王二"}
            };
            return Json(new { total = list.Count(), rows = list });

        }


    }

    public class Test
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}