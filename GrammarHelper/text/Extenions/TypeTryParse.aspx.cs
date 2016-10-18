using GrammarHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class TypeTryParse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var int1 = "2".ConvertToInt();//转换为int失败返回0
            var int2 = "2x".ConvertToInt();
            var int3 = "2".ConvertToInt(1);//转换为int失败返回1
            var int4 = "2x".ConvertToInt(1);


            var d1 = "2".ConvertToDouble();
            var d2 = "2x".ConvertToDouble();
            var d3 = "2".ConvertToDouble(1);
            var d4 = "2x".ConvertToDouble(1);

            string a = null;
            var s1 = a.ConvertToString();
            var s3 = a.ConvertToString("1");


            var d11 = "2".ConvertToDecimal();
            var d22 = "2x".ConvertToDecimal();
            var d33 = "2".ConvertToDecimal(1);
            var d44 = "2x".ConvertToDecimal(1);


            var de1 = "2013-1-1".ConvertToDate();
            var de2 = "x2013-1-1".ConvertToDate();
            var de3 = "x2013-1-1".ConvertToDate(DateTime.Now);


            //json和model转换
            var json = new { id = 1 }.ModelToJson();
            var model = "{id:1}".JsonToModel<user>();


            //list和dataTable转换
            user m = new user();
            m.id = 111;
            m.name = "aa";
            var dt = new List<user>() { m }.ConvertToDataTable();
            var list = dt.ConvertToList<user>();
            //list 转hashtable
            var clist = dt.DataTableToArrayList().ConvertIListToList();
            var ilist = dt.DataTableToArrayList();
            foreach (var item in ilist)
            {
                foreach (DictionaryEntry obj in item)
                {
                    var k = obj.Key;
                    var v = obj.Value;
                }
            }
        }
        public class user
        {
            public int id { get; set; }
            public string name { get; set; }

        }
    }
}