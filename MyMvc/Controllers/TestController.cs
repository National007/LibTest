using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Controllers
{
    public class TestController : Controller
    {

        public ActionResult ToastrDemo()
        {
            return View();
        }

        public ActionResult GooFlowDemo()
        {
            return View();
        }

        public ActionResult LodopDemo()
        {
            return View();
        }

        public ActionResult ElementUI()
        {
            return View();
        }


        // GET: Test
        public ActionResult Index()
        {
            var str = string.Empty;
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                //using (StreamReader sr = new StreamReader(Server.MapPath("/Log/2018-03-22.log"),Encoding.GetEncoding("GB2312")))
                //{
                //    string line;
                //    var i = 0;
                //    // 从文件读取并显示行，直到文件的末尾 
                //    while ((line = sr.ReadLine()) != null)
                //    {
                //        //i++;
                //        //if (i > 2)   //只读两行文本
                //        //{
                //        //    break;
                //        //}

                //        str += line+"<br />";
                //    }
                //}
                using (StreamReader sr = new StreamReader(Server.MapPath("/Log/2018-03-22.log"), Encoding.GetEncoding("GB2312")))
                {
                    str = sr.ReadToEnd().Replace("\r\n", "<br/>");  //读取文本所有内容
                }

                ViewBag.ClientIP = Net.GetWebClientIp();
                ViewBag.LanIP = Net.GetLanIp();
                ViewBag.ServerIP = Net.GetWebRemoteIp();

                var data = HttpMethods.HttpGet("http://localhost:55216/Com/Disease/GetList");
                ViewData["data"] = data;

                var ComData = HttpMethods.HttpPost("http://localhost:55216/Com/Disease/Del", "DiseaseId=17");

                //string[] line = System.IO.File.ReadAllLines(Server.MapPath("/Log/2018-03-22.log"),Encoding.GetEncoding("GB2312"));
                //str = line[1];
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            ViewData["str"] = str;
            return View();
        }

        public JsonResult Send()
        {
            SendEmail();
            return Json(new { status = true, message = "发送成功" }, JsonRequestBehavior.DenyGet);
        }

        public void SendEmail()
        {
            MailMessage msg = new MailMessage();

            List<string> strList = new List<string>() { "1912824749@qq.com", "3443929091@qq.com", "1591025134@qq.com" };

            //msg.To.Add("3443929091@qq.com");//收件人地址  

            foreach (var item in strList)
            {
                msg.To.Add(item);
            }

            msg.From = new MailAddress("1591025134@qq.com", "詹宝华");//发件人邮箱，名称  
                                                                   // msg.CC.Add("1912824749@qq.com");  //抄送人

            msg.Subject = "This is a test email from QQ";//邮件标题  
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8  

            msg.Body = "this is body<br/>https://blog.csdn.net/qingheshijiyuan/article/details/50327795";//邮件内容  
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8  

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.qq.com";//SMTP服务器地址  
            client.Port = 587;//SMTP端口，QQ邮箱填写587  

            client.EnableSsl = true;//启用SSL加密  

            client.Credentials = new NetworkCredential("1591025134@qq.com", "csvfgixaifqyfeia");//发件人邮箱账号，授权码 （POP3/SMTP服务 账户设置里面）

            client.Send(msg);//发送邮件  

        }

    }
}