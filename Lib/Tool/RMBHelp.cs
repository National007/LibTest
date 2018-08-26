﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Tool
{
   public class RMBHelp
    {
        public static string CmycurD(decimal num)
        {
            string text = "零壹贰叁肆伍陆柒捌玖";
            string text2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string text3 = "";
            string str = "";
            int num2 = 0;
            num = System.Math.Round(System.Math.Abs(num), 2);
            string text4 = ((long)(num * 100m)).ToString();
            int length = text4.Length;
            string result;
            if (length > 15)
            {
                result = "溢出";
            }
            else
            {
                text2 = text2.Substring(15 - length);
                for (int i = 0; i < length; i++)
                {
                    string text5 = text4.Substring(i, 1);
                    int startIndex = System.Convert.ToInt32(text5);
                    string str2;
                    if (i != length - 3 && i != length - 7 && i != length - 11 && i != length - 15)
                    {
                        if (text5 == "0")
                        {
                            str2 = "";
                            str = "";
                            num2++;
                        }
                        else if (text5 != "0" && num2 != 0)
                        {
                            str2 = "零" + text.Substring(startIndex, 1);
                            str = text2.Substring(i, 1);
                            num2 = 0;
                        }
                        else
                        {
                            str2 = text.Substring(startIndex, 1);
                            str = text2.Substring(i, 1);
                            num2 = 0;
                        }
                    }
                    else if (text5 != "0" && num2 != 0)
                    {
                        str2 = "零" + text.Substring(startIndex, 1);
                        str = text2.Substring(i, 1);
                        num2 = 0;
                    }
                    else if (text5 != "0" && num2 == 0)
                    {
                        str2 = text.Substring(startIndex, 1);
                        str = text2.Substring(i, 1);
                        num2 = 0;
                    }
                    else if (text5 == "0" && num2 >= 3)
                    {
                        str2 = "";
                        str = "";
                        num2++;
                    }
                    else if (length >= 11)
                    {
                        str2 = "";
                        num2++;
                    }
                    else
                    {
                        str2 = "";
                        str = text2.Substring(i, 1);
                        num2++;
                    }
                    if (i == length - 11 || i == length - 3)
                    {
                        str = text2.Substring(i, 1);
                    }
                    text3 = text3 + str2 + str;
                    if (i == length - 1 && text5 == "0")
                    {
                        text3 += '整';
                    }
                }
                if (num == 0m)
                {
                    text3 = "零元整";
                }
                result = text3;
            }
            return result;
        }

        public static string CmycurD(string numstr)
        {
            string result;
            try
            {
                decimal num = System.Convert.ToDecimal(numstr);
                result = RMBHelp.CmycurD(num);
            }
            catch
            {
                result = "非数字形式！";
            }
            return result;
        }
    }
}
