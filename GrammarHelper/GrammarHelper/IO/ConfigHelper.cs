using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GrammarHelper
{

    /// <summary>
    /// ** 描述：配置文件读取类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获得Web.config ConnectionString 里的配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        /// <summary>
        /// 获得Web.config ConnectionString 里的配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        /// <summary>
        /// 将配置信息转化为字符串
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static string GetAppString(string key, string defaultValue = null)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(keyValue))
            {
                return keyValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为整型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static int GetAppInt(string key, int defaultValue = 0)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            int tempValue;
            if (int.TryParse(keyValue, out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为布尔型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static bool GetAppBool(string key, bool defaultValue = false)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            bool tempValue;
            if (bool.TryParse(keyValue, out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }
    }
}
