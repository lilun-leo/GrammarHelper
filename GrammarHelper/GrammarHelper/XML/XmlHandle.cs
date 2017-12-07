using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Tool.Xml
{
    /// <summary>
    /// 序列化xml 多属性参考 http://www.cnblogs.com/bdstjk/archive/2012/01/19/2519861.html
    /// </summary>
    public class XmlHandle
    {
        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xml) where T : new()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化异常：" + ex.Message,ex);
            }
        }
        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(Stream xml) where T : new()
        {
            try
            {
                Type t = typeof(T);
                XmlSerializer xmls = new XmlSerializer(t);
                var data = xmls.Deserialize(xml);
                return (T)data;

            }
            catch (Exception ex)
            {
                throw new Exception("反序列化异常："+ex.Message,ex);
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string XmlSerializer<T>(T entity)
        {
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(entity.GetType()).Serialize((TextWriter)writer, entity);
                return writer.ToString();
            }
        }
    }
}
