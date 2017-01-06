using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace GrammarHelper
{
    /// <summary>
    /// ** 描述：文件公共类
    /// ** 修改时间：-
    /// ** 修改人：lilun
    /// </summary>
    public class FileHelper
    {

        //----------------变量
        #region 字段定义
        /// <summary>
        /// 同步标识
        /// </summary>
        private static Object sync = new object();
        #endregion
        //----------------
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
            return FileToString(filePath, Encoding.UTF8);
        }
        public static string FileToString(string filePath, Encoding encoding)
        {
            //创建读取器  文件 编码格式
            StreamReader reader = new StreamReader(filePath, encoding);
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

        #region 获取当前绝对路径

        public static string GetMapPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }

        #endregion

        #region 获取指定目录中的文件列表
        /// <summary>
        /// 获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 从文件绝对路径中获取目录路径
        /// <summary>
        /// 从文件绝对路径中获取目录路径
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetDirectoryFromFilePath(string filePath)
        {
            //实例化文件
            FileInfo file = new FileInfo(filePath);

            //获取目录信息
            DirectoryInfo directory = file.Directory;

            //返回目录路径
            return directory.FullName;
        }
        #endregion
        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,需要重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetDirectories(string directoryPath)
        {
            try
            {
                return Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

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

        #region 检测指定文件是否存在
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

        #region 检测指定目录是否为空
        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }

                //判断是否存在文件夹
                string[] directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 检测指定目录中是否存在指定的文件
        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。 Log?.xml   搜索logx。xml 文件 </param>        
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //获取文件目录路径
                    string directoryPath = GetDirectoryFromFilePath(filePath);

                    //如果文件的目录不存在，则创建目录
                    CreateDirectory(directoryPath);
                    lock (sync)
                    {
                        //创建文件                    
                        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                        {}
                    }
                }
            }
            catch
            {
            }
        }


        #endregion


    }
}
