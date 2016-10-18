using GrammarHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class HtmlElement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/file/test.htm");
            //获取HTML代码
            string mailBody = FileHelper.FileToString(filePath);

            XHtmlElement xh = new XHtmlElement(mailBody);
            

            //获取body的子集a标签并且class="icon"
            var link = xh.Descendants("body").ChildDescendants("a").Where(c => c.Attributes.Any(a => a.Key == "class" && a.Value == "icon")).ToList();

            //获取带href的a元素
            var links = xh.Descendants("a").Where(c => c.Attributes.Any(a => a.Key == "href")).ToList();
            foreach (var r in links)
            {
                Response.Write(r.Attributes.Single(c => c.Key == "href").Value); //出输href
            }

            //获取第一个img
            var img = xh.Descendants("img");

            //获取最近的第一个p元素以及与他同一级的其它p元素
            var ps = xh.Descendants("p");
        }
    }
}