using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
public partial class Default3 : System.Web.UI.Page
{
    public int noticeNum = 1;

    public string showMsg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = "";
        string poststr = getpost();

        JObject jobj = JObject.Parse(poststr);
        type = jobject(jobj, "type");
        switch (type)
        {
            case "show":

                string cmdText = "select * from Table_2";

                string connectionString = "Data Source=AMAZING华;Initial Catalog=DataStudents;Integrated Security=True;";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    showMsg += noticeNum.ToString() + ":" + dr["name"].ToString() + ":" +
                        dr["password"].ToString() + dr["sex"].ToString() + " <br /> ";

                    noticeNum++;

                }
                noticeNum--;
                conn.Close();
                break;
        }
        Response.Write(showMsg);
        Response.End();
    }
    public string getpost()
    {
        string g = "";
        if (HttpContext.Current.Request.InputStream != null)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream, System.Text.Encoding.UTF8);
            g = sr.ReadToEnd();
        }
        return g;
    }
    /// <summary>
    /// 从jobject中获取相应的数据 
    /// </summary>
    /// <param name="jobj">jobject对象</param>
    /// <param name="key">要获取的值</param>
    /// <returns></returns>
    public string jobject(JObject jobj, string key)
    {
        string hh = "";
        if (jobj[key] != null)
        {
            hh = jobj[key].ToString().Trim();
        }
        return hh;
    }
}
    

