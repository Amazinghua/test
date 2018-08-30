using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ajax_Newtest
{
    public partial class AJaxStyle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["action"];
            string VerifyCodeValue = Request.Params["VerifyCode"];
            if (action == "comparison")
               
            {
               
                
                switch (VerifyCodeValue)
                {

                    case "1":
                        Response.Write(abc());//转到这个方法
                        Response.End();
                        break;
                    case "2":
                        Response.Write(def());
                        Response.End();
                        break;

                    default:
                        break;
                }
            }
        }
        public string abc()
        {
            return "Amazinghua!" ;
        }
        public string def()
        {
            return "emmmmm";
        }
    }
}