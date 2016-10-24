using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace GrammarHelper
{

    /// <summary>
    /// DataTable扩展方法
    /// **作者：lilun
    /// </summary>
    public static class DataTableExtensions
    {
        #region DataTable 转 json
        /// <summary>
        /// 将table转换成 json
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToJson(this DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        #endregion

        #region DataTable 转 XML
        /// <summary>
        /// 将table转换成 XML
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToXml(this DataTable dt)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.AppendLine("<XmlTable>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strXml.AppendLine("<rows>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strXml.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j] + "</" + dt.Columns[j].ColumnName + ">");
                }
                strXml.AppendLine("</rows>");
            }
            strXml.AppendLine("</XmlTable>");
            return strXml.ToString();
        }
        #endregion

        #region DataTable 转 IList 行中是用Hashtable对象存
        /// <summary>
        /// DataTable 转 IList 行中使用Hashtable对象存
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>行中是用Hashtable对象存</returns>
        public static IList<Hashtable> DataTableToArrayList(this DataTable dt)
        {
            if (dt == null) return new List<Hashtable>();
            IList<Hashtable> datas = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable ht = dr.DataRowToHashTable();
                //Hashtable ht = DataRowToHashTable(dr);
                datas.Add(ht);
            }
            return datas;
        }
        #endregion

        #region DataRow  转  HashTable
        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashTable(this DataRow dr)
        {
            Hashtable htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);
            return htReturn;
        }
        #endregion

        #region Hashtable根据key过滤表的内容
        /// <summary>
        /// Hashtable根据key过滤表的内容
        /// </summary>
        /// <param name="dt">数据库表</param>
        /// <param name="keyField">键</param>
        /// <param name="valFiled">值</param>
        /// <returns></returns>
        public static Hashtable DataTableToHashtableByKeyValue(DataTable dt, string keyField, string valFiled)
        {
            Hashtable ht = new Hashtable();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string key = dr[keyField].ToString();
                    ht[key] = dr[valFiled];
                }
            }
            return ht;
        }
        #endregion

        #region DataTable 分页
        /// <summary>
        /// Datatable 分页  PageIndex从1开始 传0返回table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PageIndex">当前页从1开始</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static DataTable DataTableGetPaged(this DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            //复制表结构 设置开始和结算位置
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;
            //限制开始位置必须小于表的行数  结束位置大于行数时为表的最后一行
            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        #endregion

        #region 过滤掉DataTable中重复的数据
        /// <summary>
        /// 返回执行去重复后的DataTable
        /// </summary>
        /// <param name="SourceTable">源数据表</param>
        /// <param name="FieldNames">过滤字段</param>
        /// <returns></returns>
        public static DataTable DataTableSelectDistinct(this DataTable SourceTable, string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(",", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!DataTableFieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(DatatablRowClone(row, newTable.NewRow(), FieldNames));

                    DataTableSetLastValues(lastValues, row, FieldNames);
                }
            }
            return newTable;
        }
        /// <summary>
        /// 复制DataRow中的到一个新的DataRow中
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <param name="newRow"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        private static DataRow DatatablRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];
            return newRow;
        }
        private static void DataTableSetLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        private static bool DataTableFieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }
            return areEqual;
        }
        #endregion

        #region DataTable根据条件过滤表的内容
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable DataTableFilter(this DataTable dt, string condition)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (condition.Trim() == "")
                {
                    return dt;
                }
                else
                {
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    DataRow[] dr = dt.Select(condition);
                    for (int i = 0; i < dr.Length; i++)
                    {
                        newdt.ImportRow((DataRow)dr[i]);
                    }
                    return newdt;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region DataTable 转成 HTML
        /// <summary>
        ///  DataTable 转成 HTML（table）
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="columnWidth">宽度</param>
        /// <returns></returns>
        public static string DataTableToHtml(this DataTable dt, int columnWidth)
        {
            StringBuilder strTable = new StringBuilder();
            if (dt == null) return "<b stype=\"color:red\">没有找到您要的相关数据！</b>";
            strTable.AppendLine("<table id=\"table1\" class=\"grid\" singleselect=\"true\">");
            strTable.AppendLine("<thead><tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                int cw = columnWidth;
                //if (dc.ColumnName == "姓名" || dc.ColumnName == "目标值" || dc.ColumnName == "完成值" || dc.ColumnName == "完成比例")
                //    cw = 80;

                strTable.AppendLine("<td style=\"width: " + cw.ToString() + "px; text-align: center;\">" + dc.ColumnName + "</td> ");
            }
            strTable.AppendLine("</tr></thead><tbody>");

            foreach (DataRow rw in dt.Rows)
            {
                strTable.AppendLine("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    int cw = columnWidth;
                    //if (dc.ColumnName == "姓名" || dc.ColumnName == "目标值" || dc.ColumnName == "完成值" || dc.ColumnName == "完成比例")
                    //    cw = 80;
                    strTable.AppendLine("<td style=\"width: " + cw.ToString() + "px;\">" + rw[dc.ColumnName].ToString() + "</td> ");
                }
                strTable.AppendLine("</tr>");
            }
            strTable.AppendLine("</tbody></table>");
            return strTable.ToString();
        }
        /// <summary>
        /// DataTable 转成 HTML  默认宽度120
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToHtml(DataTable dt)
        {
            return DataTableToHtml(dt, 120);

        }
        #endregion
        
        #region DataTable 转成SVC,供图片使用
        /// <summary>
        /// 将datatable内容，转成图片svc使用的内容
        /// </summary>
        /// <param name="dt">table</param>
        /// <param name="dType">数据分类</param>
        /// <param name="xAxis">x</param>
        /// <param name="datacolumn">数据列</param>
        /// <returns></returns>
        public static List<string> DataTableToSvc(DataTable dt, string dType, string xAxis, string datacolumn)
        {
            string categories_Comment = "", categories_Data = "";
            DataView dv = dt.DefaultView;
            DataTable rows_Type = dt.DefaultView.ToTable(true, new string[] { dType });
            DataTable rows_Question = dt.DefaultView.ToTable(true, new string[] { xAxis });
            foreach (DataRow rw in rows_Type.Rows)
            {
                categories_Data += "{name:'" + rw[dType].ToString() + "',data:[";
                foreach (DataRow dr in rows_Question.Rows)
                {
                    if (categories_Comment.Contains(dr[xAxis].ToString() + "'") == false)
                        categories_Comment += "'" + dr[xAxis].ToString() + "',";

                    DataRow[] row = dt.Select("" + dType + "='" + rw[dType] + "' and " + xAxis + "='" + dr[xAxis] + "'");
                    if (row.Length == 0)
                        categories_Data += "0,";
                    else categories_Data += row[0][datacolumn].ToString() + ",";
                }
                if (categories_Data.EndsWith(","))
                    categories_Data = categories_Data.Remove(categories_Data.Length - 1) + "]},";
            }

            if (categories_Data.EndsWith(","))
                categories_Data = categories_Data.Remove(categories_Data.Length - 1);
            if (categories_Comment.EndsWith(","))
                categories_Comment = categories_Comment.Remove(categories_Comment.Length - 1);

            List<string> lists = new List<string>();
            lists.Add(categories_Comment);
            lists.Add(categories_Data);
            return lists;
        }

        #endregion

        #region 排序表的视图
        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static DataTable DataTableSort(this DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    tmp += sorts[i] + ",";
                }
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }
        #endregion



       
    }
}
