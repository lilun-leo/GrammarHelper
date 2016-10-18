using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrammarHelper
{
    /// <summary>
    /// ** 描述：表单帮助类
    /// ** 作者：lilun
    /// </summary>
    public static class RequestToModel
    {

        /// <summary>
        /// 提交表单通过反射获取单个像
        /// 注意：表单控件name必包含对应类中的第一个字段，否则将报错
        /// </summary>
        public static T GetSingleForm<T>() where T : new()
        {
            T t = SetList<T>(null, 0).Single();
            return t;
        }

        /// <summary>
        /// 提交表单通过反射获取单个像
        /// 注意：表单控件name必包含对应类中的第一个字段，否则将报错
        /// <param name="appstr">控件前缀,比如 name="form1.sex" appstr可以设为form1</param>
        /// </summary>
        public static T GetSingleForm<T>(string appstr) where T : new()
        {
            T t = SetList<T>(appstr, 0).Single();
            return t;
        }

        /// <summary>
        /// 提交表单通过反射获取多个对像
        /// 注意：表单控件name必包含对应类中的第一个字段，否则将报错
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> GetListByForm<T>() where T : new()
        {
            List<T> t = SetList<T>(null, 0);
            return t;
        }
        /// <summary>
        /// 提交表单通过反射获取多个对像
        /// 注意：表单控件name必包含对应类中的第一个字段，否则将报错
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="appstr">控件前缀,比如 name="form1.sex" appstr可以设为form1</param>
        /// <returns></returns>
        public static List<T> GetListByForm<T>(string appstr) where T : new()
        {
            List<T> t = SetList<T>(appstr, 0);
            return t;
        }
        /// <summary>
        /// 需要允许特殊字符使用外部函数获取Request，为NULL使用默认用Requst[]获取参数
        /// </summary>  
        public static Func<string, string> SetIsUnvalidatedFrom = null;
        //替换特殊字符
        public static string COMMAS = "$^douhao^$";
        private static string RequestPars(string key)
        {
            if (SetIsUnvalidatedFrom == null)
                return System.Web.HttpContext.Current.Request[key];
            else
                return SetIsUnvalidatedFrom(key);

        }
        public static  List<T> GetformToModel<T>() where T :new()
        {
            List<T> t = SetList<T>(null, 0);
            return t;
        }
        private static List<T> SetList<T>(string appendstr, int index) where T : new()
        {
            List<T> t = new List<T>();
            try
            {
                //通过反射获取实体对象里面的全部属性
                var properties = new T().GetType().GetProperties();
                //此处取得index=0所以获取表单中的name属性类必须包含实体中的一个字段 
                var pars = RequestPars(appendstr + properties[index].Name);
                if (pars == null || pars == "")
                {
                    return null;
                }
                var subNum = pars.Split(',').Length;
                for (int i = 0; i < subNum; i++)
                {
                    //实例化对象 遍历属性数组来获取request请求中的数据
                    var model = new T();
                    foreach (var p in properties)
                    {
                        string pval = RequestPars(appendstr + p.Name + "");
                        if (!string.IsNullOrEmpty(pval))
                        {
                            pval = pval.Split(',')[i];
                            if (pval != null)
                            {
                                pval = pval.Replace(COMMAS, ",");
                            }
                            string pptypeName = p.PropertyType.Name;
                            p.SetValue(model, Convert.ChangeType(pval, p.PropertyType), null);
                        }
                    }
                    t.Add(model);
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }


            return t;
        }
    }
}
