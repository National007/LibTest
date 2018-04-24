using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.StringHelper
{
   public class StringHelps
    {
        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
        public static string bSubstring(string s, int length, string fh)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
            int n = 0;  //  表示当前的字节数
            int i = 0;  //  要截取的字节数
            for (; i < bytes.GetLength(0) && n < length; i++)
            {

                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                if (i % 2 == 0)
                {
                    n++;      //  在UCS2第一个字节时n加1
                }
                else
                {

                    //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                    if (bytes[i] > 0)
                    {
                        n++;
                    }
                }
            }


            //  如果i为奇数时，处理成偶数

            if (i % 2 == 1)
            {
                //  该UCS2字符是汉字时，去掉这个截一半的汉字

                if (bytes[i] > 0)
                    i = i - 1;

                //  该UCS2字符是字母或数字，则保留该字符
                else
                    i = i + 1;
            }
            string str = System.Text.Encoding.Unicode.GetString(bytes, 0, i);
            if (!string.IsNullOrEmpty(fh))
            {
                if (s.Length > str.Length)
                {
                    return str + fh;
                }
            }
            return str;

        }

        public static string ReplaceRed(string strtitle, string redkey)
        {
            if (redkey == "" || redkey == null)
            {
                return strtitle;
            }
            else
                strtitle = strtitle.Replace(redkey, "<span style=\"color:#f33\">" + redkey + "</span>");
            return strtitle;
        }


    }
}
