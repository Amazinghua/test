using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ajax_Newtest
{
    public partial class Ajax_demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["action"];
            string VerifyCodeValue = Request.Params["VerifyCode"];
            string NameIdValue = Request.Params[" NameIdValue"];
            string PassWordValue = Request.Params["PassWordValue"];
            if (action == "comparison")
            {
                string Msg = "true";
                //对session中存储的验证码对比
                if (HttpContext.Current.Session["dt_session_code"] == null || VerifyCodeValue.ToLower() != HttpContext.Current.Session["dt_session_code"].ToString().ToLower())
                {
                    Msg = "false";//验证码输入不正确
                }
                Response.Write(Msg);
                Response.End();
            }

        }
    }
}