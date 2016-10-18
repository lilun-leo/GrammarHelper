using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrammarHelper;

namespace text.Form
{
    public partial class RequeryToModeltext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
              
                //设置允许 value中 包含逗号的写法
                RequestToModel.SetIsUnvalidatedFrom = key =>
                {
                    var formArray = Request.Form.GetValues(key).Select(it => it == null ? it : it.Replace(",", RequestToModel.COMMAS)).ToArray();
                    string reval = string.Join(",", formArray);
                    return reval;
                };

                //表单提交获取单个对象
                // var category = RequestToModel.GetSingleForm<RTModel>();
                //表单提交获取多个对象
                //只获取表单中name= form12.xxx的数据
                List<RTModel> categoryLists = RequestToModel.GetListByForm<RTModel>("form12.");
                //获取所有的数据 属性需要保持一致
                List<RTModel> categoryLists2 = RequestToModel.GetListByForm<RTModel>();

                var list = RequestToModel.GetformToModel<RTModel>();

            }
        }
    }
    public class RTModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}