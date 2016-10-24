using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GrammarHelper
{
    /// <summary>
    /// DataTable语法类
    /// **作者：lilun
    /// </summary>
    public static class DataTableHelper
    {

        /// <summary>
        /// List 转换成Datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public delegate object[] CreateRowDelegate<T>(T t);
        static public DataTable ToDataTable<T>(this IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            foreach (T c in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)c.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(c, null) == null ? DBNull.Value : pi.GetValue(c, null);
                }
                dtReturn.Rows.Add(dr);
            }

            return (dtReturn);

        }
        /// <summary>
        /// 创建简单的dataTable
        /// </summary>
        /// <param name="columnNameArray"></param>
        /// <returns></returns>
        public static DataTable CreateEasyTable(params string[] columnNameArray)
        {
            DataTable dt = new DataTable();
            if (columnNameArray.Length > 0)
                foreach (var it in columnNameArray)
                {
                    dt.Columns.Add(new DataColumn(it));
                }
            return dt;
        }
        /// <summary>
        /// 添加DataRow
        /// </summary>
        /// <param name="columnNameArray"></param>
        /// <returns></returns>
        public static DataTable AddRow(this DataTable dt, params Object[] rowValues)
        {
            var row = dt.NewRow();
            int i = 0;
            if (rowValues.Length > 0)
            {
                foreach (var it in rowValues)
                {
                    row[i] = it;
                    ++i;
                }
            }
            dt.Rows.Add(row);
            return dt;
        }
        #region 检查DataTable 是否有数据行
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;

            return false;
        }
        #endregion
    }
}
