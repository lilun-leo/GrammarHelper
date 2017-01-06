using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrammarHelper;

namespace text.IO
{
    public partial class ValidateCode : System.Web.UI.Page
    {
      
     
        protected void Page_Load(object sender, EventArgs e)
        {
            string Code;
            var ms= ValidateHelper.CreateValidateGraphic(out Code, 6,200,50,10);
            //将生成的图片发回客户端
            //RequeryToModeltext页面中引用
            Response.ClearContent(); //需要输出图象信息 要修改HTTP头
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
           
        }
    
    }
}