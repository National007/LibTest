using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Tool;
using Lib.StringHelper;
using Newtonsoft.Json.Linq;
using Lib.Http;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Web;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace TestProject
{
    public class Moudel {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

    }

    public class TreeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public IEnumerable<TreeItem> ChildItems { get; set; }
    }


    class Program
    {
        // 获取网页的HTML内容，根据网页的charset自动判断Encoding  
        public static string GetHtml(string url)
        {
            return GetHtml(url, null);
        }

        // 获取网页的HTML内容，指定Encoding  
        private static string GetHtml(string url, Encoding encoding)
        {
            byte[] buf = new WebClient().DownloadData(url);
            if (encoding != null) return encoding.GetString(buf);
            string html = Encoding.UTF8.GetString(buf);
            encoding = GetEncoding(html);
            if (encoding == null || encoding == Encoding.UTF8) return html;
            return encoding.GetString(buf);
        }

        // 根据网页的HTML内容提取网页的Encoding  
        private static Encoding GetEncoding(string html)
        {
            string pattern = @"(?i)\bcharset=(?<charset>[-a-zA-Z_0-9]+)";
            string charset = Regex.Match(html, pattern).Groups["charset"].Value;
            try
            {
                return Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        // 根据网页的HTML内容提取网页的Title  
        private static string GetTitle(string html)
        {
            string pattern = @"(?si)<title(?:\s+(?:""[^""]*""|'[^']*'|[^""'>])*)?>(?<title>.*?)</title>";
            return Regex.Match(html, pattern).Groups["title"].Value.Trim();
        }

        // 打印网页的Encoding和Title  
        private static void PrintEncodingAndTitle(string url)
        {
            string html = GetHtml(url);
            Console.WriteLine("[{0}] [{1}]", GetEncoding(html), GetTitle(html));
        }
        // 打印网页的Title  
        private static void PrintTitle(string url)
        {
            string html = GetHtml(url);
            Console.WriteLine("{0}", GetTitle(html));
        }
        static void Main(string[] args)
        {
            //Console.WriteLine(StringHelps.bSubstring("尼玛123456事多了付款时间的爽爽的就离开看似简单浪费空间上来看看事多了九分裤世纪东方可视对讲开了房间",20,"……"));
            //Console.WriteLine(Tools.StrConvertToPinyin("詹宝华"));
            //Console.WriteLine(GetlocationByMobile("18720052965"));
            //Console.WriteLine(StringHelps.ReplaceRed("全是爱","爱"));
            //Console.WriteLine(Tools.GetDays(2016,2));

            //DateTime dnow = DateTime.Now;
            //int days = DateTime.DaysInMonth(1, 2);  //根据年月获取月份的天数
            //int day = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(1,2);
            //Console.WriteLine(days);
            //var str = "2014-14-21";
            ////if (!IsNumeric(str))
            ////    Console.WriteLine("非数字");
            ////else
            ////    Console.WriteLine("数字");
            //Console.WriteLine(Validator.IsDateTime(str));


            //ReturnInt("sd");
            // ReturnDecimal("sd");

            //string str = "1,2,1,3";
            //var arr = str.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).Distinct();
            //foreach (var item in arr)
            //{
            //    Console.WriteLine(item);
            //}

            //string str = "03";
            //if ("03,05,07".Contains(str))
            //    Console.WriteLine("true");
            //else
            //    Console.WriteLine("false");

            //DateTime dtNow = DateTime.Now;
            //int days = DateTime.DaysInMonth(2016,2);
            //Console.WriteLine(days);

            //Console.Write(getTimeSpan("17:30"));
            //Console.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm"));

            //Console.Write(Tools.unixTimestamp + "\n" + Tools.jsTimeStampConvertDateTime(Tools.unixTimestamp));
            //var str = "2017-06";

            //Console.WriteLine(Convert.ToInt32(str.Split('-')[1]));

            //int[] intArr = new int[] {1,2,3,4,5,6,7,8,9,10,11,12 };
            //if (!intArr.Any(p => p == 2))
            //{
            //    Console.WriteLine("true");
            //}
            //else
            //{
            //    Console.WriteLine("false");
            //}

            //var sdt = Convert.ToDateTime("2017-03");
            //var edt = Convert.ToDateTime("2018-02");
            //int diffMonths = (edt.Year * 12 + edt.Month) - (sdt.Year * 12 + sdt.Month)+1;
            //Console.WriteLine(diffMonths);
            //var outDt = new DateTime();
            //for (int i = 0; i < diffMonths; i++)
            //{
            //   outDt=sdt.AddMonths(i);

            //    Console.WriteLine(outDt.ToString("yyyy-MM"));
            //}

            //foreach (var value in Enum.GetValues(typeof(TypeCategory)))
            //{
            //    object[] objAttrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
            //    if (objAttrs != null &&
            //        objAttrs.Length > 0)
            //    {
            //        DescriptionAttribute descAttr = objAttrs[0] as DescriptionAttribute;
            //        Console.WriteLine(string.Format("[{0}]", descAttr.Description));

            //        Console.WriteLine(string.Format("{0}={1}", value.ToString(), Convert.ToInt32(value)));
            //    }
            //    //Console.WriteLine(string.Format("{0}={1}", value.ToString(), Convert.ToInt32(value)));
            //}
            //PrintEncodingAndTitle("https://www.baidu.com/");

            //PrintTitle("https://www.baidu.com/");

            //string[] arr1 = new[] { "1", "2", "3", "4", "5" };
            //string[] arr2 = new[] { "1", "3", "5","4","5","2"};
            //var sameArr = arr1.Intersect(arr2).ToArray();
            ////找出相同元素(即交集)
            //foreach (var item in sameArr)
            //{
            //    Console.Write(item + "\t");
            //}
            //var diffArr = arr1.Where(c => !arr2.Contains(c)).ToArray();
            //var difflenth = diffArr.Length;
            ////找出不同的元素(即交集的补集)


            //DateTime sdt = DateTime.Parse("2017-03").AddMonths(-1);
            //DateTime sdt = DateTime.Parse("2017-03");
            //DateTime edt = DateTime.Parse("2018-02");
            ////while(sdt<edt){
            ////    sdt = sdt.AddMonths(1);
            ////    Console.WriteLine(sdt.ToString("yyyy-MM"));
            ////}

            //for (DateTime dt = sdt; dt <= edt; dt = dt.AddMonths(1))
            //{
            //    Console.WriteLine(dt.ToString("yyyy-MM"));
            //}
            //Console.Write("sd".strLength());

            //List<Maori> list = new List<Maori>()
            //{
            //    new Maori(){Type=1,Store="0006",Counter="1",Month="2"},
            //    new Maori(){Type=0,Store="0006",Counter="1",Month="2"},
            //    new Maori(){Type=1,Store="0006",Counter="3",Month="2"},
            //    new Maori(){Type=0,Store="0006",Counter="2",Month="2"},
            //    //new Maori(){Type=1,Store="0006",Counter="1",Month="2"},
            //    //new Maori(){Type=1,Store="0006",Counter="1",Month="2"},
            //    new Maori(){Type=1,Store="0007",Counter="4",Month="2"},
            //    new Maori(){Type=0,Store="0007",Counter="1",Month="2"}
            //};
            //IEnumerable<Maori> ie=list;
            //IEnumerable<Maori> ie = list.DistinctBy<Maori, string>(m => m.Type + "|" + m.Store );
            //foreach (var item in ie)
            //{
            //    Console.WriteLine(item.Type + "\t" + item.Store + "\t" + item.Counter + "\t" + item.Month);
            //}
            //int imax = Convert.ToInt32(list.Where(m => m.Store == "0007").Select(s => s.Counter).Max());

            //Console.WriteLine(imax.ToString("00000"));
            //var s = list.GroupBy(m => new { m.Type, m.Store }).Select(item => 
            //{
            //    var n=new Maori();
            //    n.Type = item.FirstOrDefault().Type;
            //    n.Store = item.FirstOrDefault().Store;
            //    n.Counter = string.Join(",", item.Select(k => k.Counter).ToArray());
            //    n.Month = Convert.ToString(item.Select(k => Convert.ToInt32(k.Month)).Sum());
            //    return n;
            //});

            //foreach (var item in s)
            //{
            //     Console.WriteLine(item.Type + "\t" + item.Store + "\t" + item.Counter + "\t" + item.Month);
            //}

            //var str = "'抚州万达合汇','临川华润合汇'";
            //Console.WriteLine(str.Replace("'",""));

            //var str = "临川华润合汇,宜春青龙高科合汇";
            //var arr = str.Split(',');
            //var ss = string.Empty;
            //foreach (var item in arr)
            //{
            //    ss += "'" + item + "',";
            //}
            //Console.WriteLine(ss.TrimEnd(','));

            var arr1 = new string[] { "采购", "商品" };
            var arr2 = new string[] { "采购", "采购", "采购", "采购" };
            //var q = from a in arr1
            //        join b in arr2.Distinct()
            //        on a equals b
            //        select a;
            //if (q.Count() == arr1.Count())
            //{
            //    Console.Write("true");
            //}
            //else
            //{
            //    Console.Write("false,{0}",arr1.Where(m=>!q.Contains(m)).ToArray().FirstOrDefault());
            //}
            //if (arr1.Count() == arr2.Distinct().Count())
            //    Console.WriteLine("true");
            //else
            //    Console.WriteLine("false,{0}", arr1.Where(m => !arr2.Contains(m)).ToArray().FirstOrDefault());

            //string str = "2017-08-26";
            //Console.WriteLine(Convert.ToDateTime(str).AddMonths(1));

            //int str = 2;
            //Console.Write(str.ToString("00"));
            //Console.WriteLine(CaculateWeekDay(2017,2,28));


            //Console.Write(flag);
            //flag = true;
            //if (!flag)
            //{
            //    Console.Write(1);
            //}
            //else
            //{
            //    Console.Write(2);
            //}

            //var TestList = new List<test>()
            //{
            //    new test{FlowNo="201711180001",Name="测试1"},
            //    new test{FlowNo="201711180002",Name="测试2"}
            //};
            //Console.WriteLine(TestList.Select(s=>s.FlowNo).Max());

            //string str = "A,B";
            //string strB = "A,C";
            //var s=str.Split(',').Intersect(strB.Split(','));
            //if (s!=null)
            //{
            //    Console.WriteLine(true);
            //}
            //else
            //{
            //    Console.WriteLine(false);
            //}

            //string str = "9.16-9.27,10.1-10.20,1.1-12.1";
            //var strA = str.Split(',');
            //var DA = "";
            //foreach (var item in strA)
            //{
            //    var strB = item.Split('-');
            //    var DB = "";
            //    foreach (var itemB in strB)
            //    {
            //        try
            //        {
            //            DB += Convert.ToDateTime(itemB).ToString("MM.dd") + "-";
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message+""+itemB);
            //        }
                    
                   
            //    }
            //    DA += DB.TrimEnd('-') + ",";
            //}


            //string a = "9.1";
            //string b = Convert.ToDateTime(a).ToString("MM.dd");
            //Console.WriteLine(DA.TrimEnd(','));

            //string str = "sdsss";
            //Console.WriteLine(Tools.ToInt(str));

            //Console.WriteLine(Math.Round(123.455, MidpointRounding.AwayFromZero));


            //var strArr = new[] {1,6,3,6,8,10 };
            //bubble_sort(strArr,false);

            //foreach (var item in strArr)
            //{
            //    Console.Write(item+"\t");
            //}
            //Console.WriteLine();
            //foreach (var item in strArr.OrderByDescending(x=>x))
            //{
            //    Console.Write(item + "\t");
            //}

            var v = "MR201712150001";
            //Console.WriteLine("MR" + Convert.ToInt64(Convert.ToInt64(v.Substring(2)) - 1));

            //var listA = new List<test>
            //{
            //    new test{FlowNo="01",Name="111"},
            //    new test{FlowNo="02",Name="222"},
            //    new test{FlowNo="03",Name="333"}
            //};

            //var listB = new List<test>() { };

            //var listC = new List<test>
            //{
            //    new test{FlowNo="01",Name="111"},
            //    new test{FlowNo="02",Name="222"},
            //    new test{FlowNo="03",Name="333"}
            //};
            ////var diffArr = arr1.Where(c => !arr2.Contains(c)).ToArray();
            //var v=0;
            //var s = 0;
            //foreach (var item in listA)
            //{
            //    if (listB.Any(m => m.FlowNo == item.FlowNo && m.Name == item.Name))
            //    {
            //        v = 1;
            //    }
            //    else
            //    {
            //        v = 2;
            //    }

            //    if (listC.Any(m => m.FlowNo == item.FlowNo && m.Name == item.Name))
            //    {
            //        s = 1;
            //    }
            //    else
            //    {
            //        s = 2;
            //    }
            //}
            //Console.WriteLine(v+"\n"+s);

            //var list = new List<test>
            //{
            //    new test{Name="老王"},
            //    new test{Name="宋喆"}
            //};
            //list.Select(s =>
            //{
            //    s.FlowNo = "1";
            //    return s;
            //}).ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.FlowNo);
            //}
            var list = GetMacByIPConfig();
            //Console.WriteLine(list.FirstOrDefault());
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //string JsonStr = "{\'Name\':\'Elaine00\',\'Hobby\':\'eat eat\'}";
            //A a = JsonConvert.DeserializeObject<A>(JsonStr);
            //Console.WriteLine(a.Name);
            //A a = new A();
            //a.Name = "Elain00";
            //a.Hobby = "eat eat";

            //string jsonStr = JsonConvert.SerializeObject(a);
            //Console.WriteLine(jsonStr);

            //int[] arr = new int[] { 3, 4, 1, 0, 8, 5, 10 };
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    for (int j = i; j < arr.Length; j++)
            //    {
            //        //if (arr[i] > arr[j]) {
            //        //    int temp = 0;
            //        //    temp = arr[i];
            //        //    arr[i] = arr[j];
            //        //    arr[j] = temp;
            //        //}
            //        if (arr[i] < arr[j])
            //        {
            //            int temp = 0;
            //            temp = arr[i];
            //            arr[i] = arr[j];
            //            arr[j] = temp;
            //        }
            //    }
            //}

            //foreach (var item in arr)
            //{
            //    Console.Write(item+"\t");
            //}
            //string a = "a";
            //string b = "啊";
            //Console.WriteLine(a.Length+"\n"+b.Length);
            //Console.WriteLine(System.Text.Encoding.Default.GetBytes(a).Length+"\n"+Encoding.Default.GetBytes(b).Length);

            //Console.WriteLine(sizeof(int));

            // Console.WriteLine(RMBHelp.CmycurD("1234567890121"));

            //var MoudelList = new List<Moudel>()
            //{
            //    new Moudel(){Id=1,Name="菜单管理",ParentId=0 },
            //    new Moudel(){Id=2,Name="系统设置",ParentId=0 },
            //    new Moudel(){Id=3,Name="日志管理",ParentId=0 },
            //    new Moudel(){Id=4,Name="菜单列表",ParentId=1 },
            //    new Moudel(){Id=5,Name="菜单权限",ParentId=1 },
            //    new Moudel(){Id=6,Name="用户列表",ParentId=2 },
            //    new Moudel(){Id=7,Name="日志追寻",ParentId=3 },
            //    new Moudel(){Id=8,Name="日志跟踪",ParentId=3 },
            //    new Moudel(){Id=9,Name="数据字典",ParentId=2 }
            //};

            ////MoudelList.ForEach(a => Console.WriteLine($"{a.Id}\t{a.Name}\t{a.ParentId}"));
            //var treeItem = GetAll(MoudelList);
            //Console.WriteLine(Tostring(treeItem));
            WcfService1.Service1 service = new WcfService1.Service1();
            var l = service.GetAll();
            

            Console.ReadKey();
        }

        public static List<TreeItem> GetAll(List<Moudel> list)
        {
            var result = list.Where(m=>m.ParentId==0).Select(s=>
            {
                TreeItem tree = new TreeItem();
                tree.Id = s.Id;
                tree.Name = s.Name;
                tree.ParentId = s.ParentId;
                tree.ChildItems = list.Where(p => p.ParentId == s.Id).Select(o =>
                {
                    TreeItem childtree = new TreeItem();
                    childtree.Id = o.Id;
                    childtree.Name = o.Name;
                    childtree.ParentId = o.ParentId;
                    return childtree;
                }).ToList();
                return tree;
            }).ToList();

            return result;
        }

        public static string Tostring(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 获取客户端MAC地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacByIPConfig()
        {
          List<string> macs =new List<string>();
          ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
          startInfo.UseShellExecute = false;
          startInfo.RedirectStandardInput = true;
          startInfo.RedirectStandardOutput = true;
          startInfo.RedirectStandardError = true;
          startInfo.CreateNoWindow = true;
          Process p = Process.Start(startInfo);
          //截取输出流
          StreamReader reader = p.StandardOutput;
          string line = reader.ReadLine();

          while (!reader.EndOfStream)
          {
            if (!string.IsNullOrEmpty(line))
            {
              line = line.Trim();

              //if (line.StartsWith("Physical Address")||line.StartsWith("物理地址"))
              //{
              //  macs.Add(line);
              //}
              //if (line.StartsWith("IPv4"))
              //{
              //    macs.Add(line);
              //}
            }

            line = reader.ReadLine();
          }

          //等待程序执行完退出进程
          p.WaitForExit();
          p.Close();
          reader.Close();
 
          return macs;
        }

        static void bubble_sort(int[] unsorted, bool Desc=true)
        {
            for (int i = 0; i < unsorted.Length; i++)
            {
                for (int j = i + 1; j < unsorted.Length; j++)
                {
                    if (Desc)
                    {
                        if (unsorted[j] > unsorted[i])
                        {
                            int temp = unsorted[i];
                            unsorted[i] = unsorted[j];
                            unsorted[j] = temp;
                        }
                    }
                    else
                    {
                        if (unsorted[i] > unsorted[j])
                        {
                            var temp = unsorted[i];
                            unsorted[i] = unsorted[j];
                            unsorted[j] = temp;
                        }
                    }
                   

                }
            }
        }


        public class test
        {
            public string FlowNo { get; set; }
            public string Name { get; set; }
        }

        public class A
        {
            public string Name { get; set; }
            public string Hobby { get; set; }
        }

        public static bool flag { get; set; }

        /// <summary>
        /// 基姆拉尔森计算公式计算日期
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns>星期几</returns>

        public static string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1 || m == 2)
            {
                m += 12;
                y--;         //把一月和二月看成是上一年的十三月和十四月，例：如果是2004-1-10则换算成：2003-13-10来代入公式计算。
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            string weekstr = "";
            switch (week)
            {
                case 0: weekstr = "星期一"; break;
                case 1: weekstr = "星期二"; break;
                case 2: weekstr = "星期三"; break;
                case 3: weekstr = "星期四"; break;
                case 4: weekstr = "星期五"; break;
                case 5: weekstr = "星期六"; break;
                case 6: weekstr = "星期日"; break;
            }
            return weekstr;
        }

        public static bool getTimeSpan(string timeStr)
        {
            //判断当前时间是否在工作时间段内
            string _strWorkingDayAM = "08:30";//工作时间上午08:30
            string _strWorkingDayPM = "17:30";
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            //string time1 = "2017-2-17 8:10:00";
            DateTime t1 = Convert.ToDateTime(timeStr);

            TimeSpan dspNow = t1.TimeOfDay;
            if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
            {
                return true;
            }
            return false;
        }

        public static void ReturnInt(string str)
        {
            try
            {
                if (IsNumeric(str))
                {
                    Console.WriteLine("数字类型");
                }
                else
                {
                    Console.WriteLine("非数字类型");
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("终止。。。");
            }
        }

        public static void ReturnDecimal(string str)
        {
            if (IsNumeric(str))
            {
                Console.WriteLine("数字类型");
                return;
            }
            else
            {
                Console.WriteLine("非数字类型");
            }
            Console.WriteLine("滴滴");
        }


        /// <summary>
        /// 验证字符串是否为数字类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            decimal n;
            if (decimal.TryParse(value, out n))
                return true;
            else
                return false;
        }

        #region  根据手机号码获取地址
        public static string GetlocationByMobile(string mobile)
        {
            string res = string.Empty;
            string url = "http://apis.haoservice.com/mobile?phone=" + mobile + "&key=8b28a1b71b92403fbe1515c90d4751c0";
            JObject jo = Get.GetResult<JObject>(url);
            //res = HttpGet(url);
            //JObject jo = (JObject)JsonConvert.DeserializeObject(res);
            res = jo["result"]["province"].ToString() + jo["result"]["city"].ToString();
            return res;
        }
        #endregion

    }
    // 扩展IEnumerable<T>
    public static class ExternEnumerable
    {
        //两个泛型直接的数据排重
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
    public class Maori
    {
        public int Type { get; set; }
        public string Store { get; set; }
        public string Counter { get; set; }
        public string Month { get; set; }
    }

    public static class test
    {
       
        public static int strLength(this String str)
        {
            return str.Length;
        }
    }
}
