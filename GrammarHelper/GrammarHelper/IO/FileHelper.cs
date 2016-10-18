using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GrammarHelper
{
    /// <summary>
    /// ** 描述：文件公共类
    /// ** 修改时间：-
    /// ** 修改人：lilun
    /// </summary>
    public class FileHelper
    {


        #region 将文件读取到字符串中
        public static string FileToString(string filePath)
        {
            return FileToString(filePath,Encoding.UTF8);
        }
        public static string FileToString(string filePath, Encoding encoding)
        {
            //创建读取器  文件 编码格式
            StreamReader reader = new StreamReader(filePath,encoding);
            try
            {
                //一次性读取所有内容并返回
                return reader.ReadToEnd(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
           
        }
        #endregion
    }
}
