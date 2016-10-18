using GrammarHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class EnumSugarExtenions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var attr = MyType.b.GetName();
            var value = MyType.b.GetValue<int>();
           
        }
        public enum MyType
        {

         aaaa = 0,

          b = 1
        }
    }
}