using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrammarHelper;

namespace text.Extenions
{
    public partial class IEnumerableExtensions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            List<User> list = new List<User>();
            list.Add(new User() { name="a1",age="1"} );
            list.Add(new User() { name = "a2", age = "2" });
            list.Add(new User() { name = "a3", age = "3" });
            list.Add(new User() { name = "a4", age = "4" });
            var item= new List<User>();
            User u = new User();

           
            list.TryForEach(i => item.Add(new User
            {
                name = i.name,
                age = i.age
            }));
         

        }

        
    }
    public class userinfo
    {
        public Dictionary<User, int> info { get; set; }
    }
    public class User {
        public string name{ get; set; }
        public string age { get; set; }
    }
}