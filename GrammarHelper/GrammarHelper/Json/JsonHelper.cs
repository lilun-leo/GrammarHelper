using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace GrammarHelper
{
    public class JsonHelper
    {
      
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool HasNullIgnore)
        {
            if (HasNullIgnore)
                return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!String.IsNullOrWhiteSpace(strJson))
                return JsonConvert.DeserializeObject<T>(strJson);
            return null;
        }
        #region ListToJson
        /// <summary>
        /// list 转换json格式
        /// </summary>
        /// <param name="jsonName">类名</param>
        /// <param name="objlist">list集合</param>
        /// <returns></returns>
        public static string ListToJson<T>(List<T> objlist, string jsonName)
        {
            string result = "{";
            //如果没有给定类的名称， 指定一个
            if (jsonName.Equals(string.Empty))
            {
                object o = objlist[0];
                jsonName = o.GetType().ToString();
            }
            result += "\"" + jsonName + "\":[";
            //处理第一行前面不加","号
            bool firstline = true;
            foreach (object oo in objlist)
            {
                if (!firstline)
                {
                    result = result + "," + ToJson(oo);
                }
                else
                {
                    result = result + ToJson(oo) + "";
                    firstline = false;
                }
            }
            return result + "]}";
        }
        #endregion
        /// <summary>
        /// 数组转Json
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string ArrayToJson(string[] strs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strs.Length; i++)
            {
                sb.AppendFormat("'{0}':'{1}',", i + 1, strs[i]);
            }
            if (sb.Length > 0)
                return "{" + sb.ToString().TrimEnd(',') + "}";
            return "";
        }


    
        #region Property
        /// <summary>
        /// 数据状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 回传URL
        /// </summary>
        public string ReUrl { get; set; }
        /// <summary>
        /// 数据包
        /// </summary>
        public object Data { get; set; }
        #endregion

    }
}
