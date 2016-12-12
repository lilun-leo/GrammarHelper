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
        #region 获取文件路并自动创建目录
        /// <summary>
        /// 根据文件目录、编号、文件名生成文件路径，并且创建文件存放目录
        /// 格式为:/directory/code/filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directory"></param>
        /// <param name="code"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFiePathAndCreateDirectoryByCode<T>(string directory, T code, string fileName)
        {
            if (directory == null)
            {
                throw new ArgumentNullException("FileSugar.GetCreatePath.directory");
            }
            directory = directory.TrimEnd('/');
            string path = new StringBuilder("{0}//{1}//{2}").AppendFormat(directory, code, fileName).ToString();
            directory = Path.GetDirectoryName(path);
            if (!IsExistDirectory(directory))
            {
                CreateDirectory(directory);
            }
            return path;

        }
        /// <summary>
        /// 根据文件目录、日期、文件名生成文件路径，并且创建文件存放目录
        /// 格式为:/directory/2015/01/01/filename
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directory"></param>
        /// <param name="code"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFiePathAndCreateDirectoryByDate<T>(string directory, string fileName)
        {
            if (directory == null)
            {
                throw new ArgumentNullException("FileSugar.GetCreatePath.directory");
            }
            directory = directory.TrimEnd('/');
            string path = new StringBuilder("{0}//{1}//{2}//{3}//{4}").AppendFormat(directory, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, fileName).ToString();
            directory = Path.GetDirectoryName(path);
            if (!IsExistDirectory(directory))
            {
                CreateDirectory(directory);
            }
            return path;

        }
        #endregion

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




        //----------检测------------------
        #region 检测指定目录是否存在
        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>        
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        #endregion



        //----------创建------------------
        #region 创建一个目录
        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion
    }
}
