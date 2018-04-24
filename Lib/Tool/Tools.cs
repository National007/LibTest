using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web;
using System.Text.RegularExpressions;

namespace Lib.Tool
{
    public class Tools
    {
        public static string IpAddress
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }

        public static string localPath
        {
            get { return HttpContext.Current.Request.Url.LocalPath; }
        }
        public static string GUID
        {
            get { return Guid.NewGuid().ToString("n").ToUpper(); }
        }

        public static string NewID
        {
            get { return DateTime.Now.ToString("yyyyMMddHHmmssff") + (new Random().Next(10000000, 99999999)) + (new Random().Next(10000000, 99999999)); }
        }

        /// <summary>
        /// MD5密码比较
        /// </summary>
        /// <param name="cpass">数据库密码</param>
        /// <param name="npass">表单填写密码</param>
        /// <returns></returns>
        public static bool ComparePassword(string cpass, string npass)
        {
            return ComparePassword(cpass, npass, Tools._salt);
        }

        /// <summary>
        /// MD5密码比较
        /// </summary>
        /// <param name="cpass">数据库密码</param>
        /// <param name="npass">表单填写密码</param>
        /// <param name="salt">MD5加颜值</param>
        /// <returns></returns>
        public static bool ComparePassword(string cpass, string npass, string salt)
        {
            return string.Compare(cpass, Tools.MakePassword(npass, salt), true) == 0;
        }

        public static string _salt
        {
            get { return string.IsNullOrEmpty(WebConfigurationManager.AppSettings["md5Salt"]) ? "Lib" : WebConfigurationManager.AppSettings["md5Salt"]; }
        }

        public static string MakePassword(string password)
        {
            return MakePassword(password, Tools._salt);
        }

        public static string MakePassword(string password, string _salt)
        {
            return Md5Sign(password, _salt, "UTF-8");
        }

        public static string Md5Sign(string parameter, string key, string _input_charset)
        {
            StringBuilder sb = new StringBuilder(32);
            string s = parameter + key;
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bt = md.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            int num = 0;
            for (int i = 0; i < bt.Length; i = num + 1)
            {
                sb.Append(bt[i].ToString("x").PadLeft(2, '0'));
                num = i;
            }
            return sb.ToString();
        }
        public static string GetDateText(string str)
        {
            DateTime dt2 = Convert.ToDateTime(str);
            return Tools.GetDateText(dt2);
        }
        public static string GetDateText(DateTime dt)
        {
            //string[] week = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string result = string.Empty;
            DateTime dtNow = DateTime.Now;
            TimeSpan ts = dtNow - dt;

            if (ts.TotalMinutes < 0)
            {
                return "扯卵蛋";
            }

            bool flag = ts.TotalMinutes <= 1.0;

            if (flag)
                result = "刚刚";
            else
            {
                bool flag2 = ts.TotalHours <= 1.0;
                if (flag2)
                    result = ts.TotalMinutes.ToString("f0") + "分钟前";
                else
                {
                    int tsDay = Convert.ToInt32(dtNow.Day - dt.Day);
                    int dow = Convert.ToInt32(dtNow.DayOfWeek.ToString("d")) == 0 ? 7 : Convert.ToInt32(dtNow.DayOfWeek.ToString("d"));
                    DateTime startWeek = dtNow.AddDays(1 - dow);   //本周周一的时间
                    DateTime endWeek = startWeek.AddDays(6);   //本周周日
                    int i1 = Convert.ToInt32(dt.Day - startWeek.Day);
                    if (dt.Year == dtNow.Year && dt.Month == dtNow.Month)
                    {
                        if (tsDay <= 0)
                            return result = "今天 " + dt.ToString("HH:mm");
                        if (0 < tsDay && tsDay <= 1)
                            return result = "昨天 " + dt.ToString("HH:mm");
                        if (1 < tsDay && tsDay <= 2)
                            return result = "前天 " + dt.ToString("HH:mm");
                        if (i1 >= 0)
                            return result = dt.ToString("dddd") + " " + dt.ToString("HH:mm");
                        if (i1 < 0)
                            return result = string.Format("{0:m} {1:t}", dt, dt);
                    }
                    else
                    {
                        DateTime smileDT = new DateTime(DateTime.Now.Year, 1, 1);   //本年第一天
                        TimeSpan ts2 = smileDT - dt;
                        if (ts2.TotalSeconds > 0)
                        {
                            result = string.Format("{0:f}", dt);
                        }

                        else
                        {
                            result = string.Format("{0:m} {1:t}", dt, dt);
                        }


                    }
                }
            }

            return result;
        }

        #region  //返回手机号码格式  例如:123****78901
        /// <summary>
        /// 返回手机号码格式  例如:123****78901
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns></returns>
        public static string FormatMobile(string phoneNo)
        {
            Regex re = new Regex("(\\d{3})(\\d{4})(\\d{4})", RegexOptions.None);
            phoneNo = re.Replace(phoneNo, "$1****$3");
            return phoneNo;
        }
        #endregion

        #region  //返回银行卡格式  例如:**** **** **** 1201
        /// <summary>
        /// //返回银行卡格式  例如:**** **** **** 1201
        /// </summary>
        /// <param name="bankNo">银行卡号</param>
        /// <returns></returns>
        public static string FormatBank(string bankNo)
        {
            Regex re = new Regex("(\\d{12})(\\d{4})", RegexOptions.None);
            bankNo = re.Replace(bankNo, "**** **** **** $2");
            return bankNo;
        }
        #endregion

        #region  //返回身份证号码格式  例如:362330********1212
        /// <summary>
        ///  返回身份证号码格式  例如:362330********1212
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns></returns>
        public static string FormatIdCard(string idCard)
        {
            Regex re = new Regex("(\\d{6})(\\d{8})(\\d{4})", RegexOptions.None);
            idCard = re.Replace(idCard, "$1********$3");
            return idCard;
        }
        #endregion

        #region   //将传入的字符串中间部分字符替换成特殊字符
        /// <summary>
        /// 将传入的字符串中间部分字符替换成特殊字符
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度</param>
        /// <param name="endLen">尾保留长度</param>
        /// <param name="replaceChar">特殊字符</param>
        /// <returns>被特殊字符替换的字符串</returns>
        public static string ReplaceWithSpecialChar(string value, int startLen = 4, int endLen = 4, char specialChar = '*')
        {
            try
            {
                int lenth = value.Length - startLen - endLen;
                string replaceStr = value.Substring(startLen, lenth);
                string specialStr = string.Empty;
                for (int i = 0; i < replaceStr.Length; i++)
                {
                    specialStr += specialChar;
                }
                value = value.Replace(replaceStr, specialStr);
            }
            catch (Exception)
            {
                throw;
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 姓氏全拼名字取首字母
        /// </summary>
        /// <param name="strText">中文字符串</param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                {
                    myStr += StrConvertToPinyin(strText.Substring(i, 1));
                }
                else
                {
                    myStr += getSpell(strText.Substring(i, 1));
                }

            }
            return myStr.ToLower();
        }

        #region //取汉字的首字母
        /// <summary>
        /// //取汉字的首字母
        /// </summary>
        /// <param name="cnChar">汉字字符</param>
        /// <returns></returns>
        public static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
                return cnChar;
        }
        #endregion

        #region //定义拼音区编码数组
        private static int[] getValue = new int[]
            {
        -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
        -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
        -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
        -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
        -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
        -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
        -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
        -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
        -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
        -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
        -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
        -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
        -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
        -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
        -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
        -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
        -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
        -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
        -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
        -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
        -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
        -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
        -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
        -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
        -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
        -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
        -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
        -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
        -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
        -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
        -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
        -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
        -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
            };
        #endregion

        #region //定义拼音数组
        private static string[] getName = new string[]
            {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
           };
        #endregion

        #region 汉字转换成全拼的拼音
        /// <summary>汉字转换成全拼的拼音</summary>
        /// <param name="Chstr">汉字字符串</param>
        /// <returns>转换后的拼音字符串</returns> 
        public static string StrConvertToPinyin(string Chstr)
        {
            Regex reg = new Regex("^[\u4e00-\u9fa5]$");//验证是否输入汉字
            byte[] arr = new byte[2];
            string pystr = "";
            int asc = 0, M1 = 0, M2 = 0;
            char[] mChar = Chstr.ToCharArray();//获取汉字对应的字符数组
            for (int j = 0; j < mChar.Length; j++)
            {
                //如果输入的是汉字
                if (reg.IsMatch(mChar[j].ToString()))
                {
                    arr = System.Text.Encoding.Default.GetBytes(mChar[j].ToString());
                    M1 = (short)(arr[0]);
                    M2 = (short)(arr[1]);
                    asc = M1 * 256 + M2 - 65536;
                    if (asc > 0 && asc < 160)
                    {
                        pystr += mChar[j];
                    }
                    else
                    {
                        switch (asc)
                        {
                            case -9254:
                                pystr += "Zhen"; break;
                            case -8985:
                                pystr += "Qian"; break;
                            case -5463:
                                pystr += "Jia"; break;
                            case -8274:
                                pystr += "Ge"; break;
                            case -5448:
                                pystr += "Ga"; break;
                            case -5447:
                                pystr += "La"; break;
                            case -4649:
                                pystr += "Chen"; break;
                            case -5436:
                                pystr += "Mao"; break;
                            case -5213:
                                pystr += "Mao"; break;
                            case -3597:
                                pystr += "Die"; break;
                            case -5659:
                                pystr += "Tian"; break;
                            default:
                                for (int i = (getValue.Length - 1); i >= 0; i--)
                                {
                                    if (getValue[i] <= asc) //判断汉字的拼音区编码是否在指定范围内
                                    {
                                        pystr += getName[i];//如果不超出范围则获取对应的拼音
                                        break;
                                    }
                                }
                                break;
                        }
                    }
                }
                else    //如果不是汉字
                {
                    pystr += mChar[j].ToString();//如果不是汉字则返回
                }
            }
            return pystr;//返回获取到的汉字拼音
        }
        #endregion


        /// <summary>
        /// C# DateTime转换为JavaScript时间戳
        /// </summary>
        public static long jsTimestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970,1,1));
                long timestamp = (long) (DateTime.Now - startTime).TotalMilliseconds;   //相差毫秒数
                return timestamp;
            }
        }

        /// <summary>
        /// JavaScript时间戳转换为C# DateTime
        /// </summary>
        /// <param name="jsTimestamp"></param>
        /// <returns></returns>
        public static DateTime jsTimeStampConvertDateTime(long jsTimestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970,1,1));
            DateTime dt = startTime.AddMilliseconds(jsTimestamp);
            return dt;
        }

        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        public static long unixTimestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                long timestamp = (long)(DateTime.Now - startTime).TotalSeconds;   //相差秒数
                return timestamp;
            }
        }

        /// <summary>
        /// Unix时间戳转换为C# DateTime
        /// </summary>
        /// <param name="jsTimestamp"></param>
        /// <returns></returns>
        public static DateTime unixTimeStampConvertDateTime(long unixTimestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dt = startTime.AddSeconds(unixTimestamp);
            return dt;
        }

        /// <summary>
        /// 根据年份、月份 获取当前月份共有多少天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>返回当月天数</returns>
        public static int GetDays(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                    {
                        return 29;
                    }
                    return 28;
                default:
                    return 0;
            }
        }

        public static int ToInt(string text)
        {
            if (text.Length <= 0)
            {
                return 0;
            }
            int rel = 0;
            if (int.TryParse(text, out rel))
                return rel;

            return 0;
        }

    }
}
