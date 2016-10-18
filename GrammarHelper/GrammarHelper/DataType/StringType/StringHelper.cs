using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GrammarHelper
{
    /// <summary>
    /// **描述:string操作类
    /// **作者 lilun
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 获取格式化字符串,等同于string.Format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string ToFormat(this string value, params object[] args)
        {
           
            return string.Format(value, args);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="cutLength">开始截取的位置</param>
        /// <param name="appendString">截取后要追加的字符串</param>
        /// <returns></returns>
        public static string ToCutString(this string value,int cutLength,string appendString =null)
        {
            string str = "";
            //截取长度大于值得长度返回字符串
            if (cutLength >= value.Length)
            {
                return value;
            }
            //最后字符实际要追加长度  
            int nRealLen = cutLength * 2;
            if (appendString != null)
            {
                nRealLen= nRealLen- appendString.Length;
            }
            Encoding encoding = Encoding.GetEncoding("gb2312");
            for (int i = value.Length; i >= 0; i--)
            {
                str = value.Substring(0, i);
                if (encoding.GetBytes(str).Length > nRealLen)
                    continue;
                else
                    break;
            }
            str += appendString;
            return str;
        }

        /// <summary>
        /// 对 HTML 编码的字符串进行解码，并返回已解码的字符串。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHtmlDecode(this string value)
        {
            return System.Web.HttpContext.Current.Server.HtmlDecode(value);
        }
        /// <summary>
        ///   对字符串进行 HTML 编码并返回已编码的字符串。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHtmlEncode(this string value)
        {
            return System.Web.HttpContext.Current.Server.HtmlEncode(value);
        }

        /// <summary>
        /// 对字符串进行 URL 解码并返回已解码的字符串。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlDecode(this string value)
        {
            return System.Web.HttpContext.Current.Server.UrlDecode(value);
        }
        /// <summary>
        ///  对字符串进行 URL 编码，并返回已编码的字符串。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToUrlEncode(this string value)
        {
            return System.Web.HttpContext.Current.Server.UrlEncode(value);
        }


        /// <summary>
        /// 将文本转换成html 例如:\r\n11cc\r<p>\taaa\r 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHtmlByText(this string value)
        {
            value = value.Replace("\r\n", "\r");
            value = value.Replace("\n", "\r");
            value = value.Replace("\r", "<br>\r\n");
            value = value.Replace("\t", " ");
            return value;
        }
        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="appendString"></param>
        /// <param name="symbol">两个字符串之间的符号</param>
        /// <param name="valueIsNullAppendSymbol">当VALUE是NULL时默认 symbol是追加的 ，设成false则不会添加到拼接当中</param>
        /// <returns></returns>
        public static string AppendString(this string value, string appendString, string symbol = null, bool valueIsNullAppendSymbol = true)
        {

            if (valueIsNullAppendSymbol)
            {
                return value + symbol + appendString;
            }
            else
            {
                return value + (string.IsNullOrEmpty(value) ? "" : symbol) + appendString;
            }

        }
    }
}
