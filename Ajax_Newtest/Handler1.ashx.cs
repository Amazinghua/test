using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax_Newtest
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            string name = context.Request.Params["name"].ToString().Trim();
            if ("china".Equals(name))
            {
                context.Response.Write("1");//1标志login success  
            }
            else
            {
                context.Response.Write("0");//0标志login fail  
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}