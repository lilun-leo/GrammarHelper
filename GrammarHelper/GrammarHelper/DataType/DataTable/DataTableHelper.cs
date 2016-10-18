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
