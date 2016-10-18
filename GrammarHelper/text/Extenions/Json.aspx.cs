using GrammarHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class Json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //  List<user> list = new List<user>();
            //  user u = new user() { id="1",name="22"};
            //var aaaa=  JsonHelper.ToJson(u);
            //  list.Add(u);

            //var a1 = JsonHelper.ListToJson(list,"");

            // string[] a = { "aa","bb","cc"};
            //var aa= JsonHelper.ArrayToJson(a);
        

        }
        public class user {

           public string id { get; set; }
            public string name { get; set; }
        }
    
    }


}