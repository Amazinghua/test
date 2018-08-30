using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax_Newtest.fxxf
{
    public class common
    {
        public static string QueryString(string par)
        {
            string v = "";
            if (HttpContext.Current.Request.QueryString[par] != null)
            {
                v = HttpContext.Current.Request.QueryString[par].Trim();
            }
            return v;
        }
    }
}