using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrammarHelper;
using System.Data;
namespace text.Extenions
{
    public partial class DataTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //创建table 添加行
            var dt = DataTableHelper.CreateEasyTable("id").AddRow(1);
            dt.AddRow(2);
            dt.AddRow(3);
            dt.AddRow(4);
            dt.AddRow(5);
            dt.AddRow(6);
            dt.AddRow(7);
            dt.AddRow(8);
            dt.AddRow(9);
            dt.AddRow(10);
            dt.AddRow(11);
            dt.AddRow(10);
            //指定字段去重
            string[] pare = { "id" };
            var a = dt.DataTableSelectDistinct(pare);
            //根据条件筛选
            var aa = dt.DataTableFilter("id>1");
            //转换List<hashtable>
            var x = dt.DataTableToArrayList();

            //DataTable转html
            var html = dt.DataTableToHtml(200);
            //对datatable分页
            var table = dt.DataTableGetPaged(0, 5);
            //对表中某一行转换成HashTable
            var HashTable = dt.Rows[0].DataRowToHashTable();

            //字符串截取and后追加字符
            string str = "abcd";
            string aaaaaa= str.ToCutString(3,"eff");
            //对heml操作
            string htmlcode = "<br>aaaaaa< p > bbbb </ p > ";
            string newhtmlcode = htmlcode.ToHtmlEncode();//转码
            string newhtmlcode2 = htmlcode.ToHtmlDecode();//解码
            //对url操作
            string url = "http://localhost:4099/Extenions/DataTable.aspx";
            string newurl = url.ToUrlEncode();
            string newurl2 = url.ToUrlDecode();
            //将text字符串转换html
            string text = "\r\n11cc\r<p>\taaa\r ";
            var ccc= text.ToHtmlByText();
            //追加字符串
            string stringa = "abc";
            var stringApp = stringa.AppendString("edf", ":");
            //string stringa = null;
            // var stringApp = stringa.AppendString("edf",":",false);  结果 edf
            
            //list 直接转换为Datatable
            List<a> list = new List<a>();
            System.Data.DataTable dt3 = DataTableHelper.ToDataTable(list, rec => new object[] { list });

        }
        public class a
        {
            public float id { get; set; }
        }
    }
}