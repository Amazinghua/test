using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ajax_Newtest.fxxf
{
    public partial class details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public string GetId
        {
            get
            {
                return Ajax_Newtest.fxxf.common.QueryString("id");
            }
        }
    }
}