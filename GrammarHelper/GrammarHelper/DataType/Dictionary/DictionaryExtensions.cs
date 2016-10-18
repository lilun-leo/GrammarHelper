using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrammarHelper.DataType.Dictionary
{

    /// <summary>
    /// 数据字典
    /// </summary>
    public static   class DictionaryExtensions
    {
        /// <summary>
        /// 获取数据字典的值
        /// </summary>
        /// <param name="thisObj">数据字典</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetTryValue(this Dictionary<string, string> thisObj, string key)
        {
            if (thisObj == null) return null;
            if (!thisObj.ContainsKey(key)) return null;
            return thisObj[key] + "";
        }
    }
}
