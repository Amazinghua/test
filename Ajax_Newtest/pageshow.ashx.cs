using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using DBUtility;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Ajax_Newtest
{
    /// <summary>
    /// pageshow 的摘要说明
    /// </summary>
    public class   pageshow : IHttpHandler
    {
        private int PageIndex = 0;//当前页码
        private int PageSize = 3;//每页几条记录
        private int TotalPage = 1;//总分页数
        private int TotalRecord = 0;//总记录
        private string OrderType = " desc";//排序方式 默认正序
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            string resultStr = string.Empty;
            string type = "";
            try
            {
                string poststr = getpost();
                if (!string.IsNullOrEmpty(poststr))
                {
                    JObject jobj = JObject.Parse(poststr);
                    type = jobject(jobj, "type");
                    switch (type)
                    {
                        case "list":
                            GetParams();
                            DataTable ds = PageData("Table_2", "*", "name", OrderType, "", PageSize, PageIndex, out TotalPage, out TotalRecord);
                           
                            resultStr = GetDivPager("", ds);
                            
                            //resultStr = resultStr.Replace("\"", "\\\"")/*.Replace("\r", "").Replace("\n", "").Replace("\\", "\\\\")*/;
                       
                            result = "{\"result\":\"888\",\"table\":" + DataTableToJson(ds) + ",\"page\":" + resultStr + "}";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, "C:\\Users\\Administrator\\Desktop\\log.txt");

            }
            context.Response.Write(result);
            context.Response.Write(resultStr);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string NoHtml(string html)
        {
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");
            return StrNohtml;
        }
        /// <summary>
        /// 这个也是转换为json不过是用系统的
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DataTableToJson(DataTable dt)
        {
            string result = "";
            result = JsonConvert.SerializeObject(dt, new DataTableConverter());
            //new DataTableConverter()一般是固定的
            return result;
        }
        /// <summary>     
        /// dataTable转换成Json格式     
        /// </summary>     
        /// <param name="dt"></param>     
        /// <returns></returns>     
        public static string tableToJson(DataTable dt, string tablename)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("\"");
            jsonBuilder.Append(tablename);
            jsonBuilder.Append("\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jsonBuilder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(dt.Columns[j].ColumnName.ToLower());
                        jsonBuilder.Append("\":\"");
                        if (dt.Columns[j].DataType == typeof(DateTime))
                        {
                            String v = dt.Rows[i][j].ToString().Trim();
                            if (v != "")
                            {
                                DateTime d = DateTime.Now;
                                DateTime.TryParse(v, out d);
                                v = d.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            jsonBuilder.Append(v);
                        }
                        else
                        {
                            String v = dt.Rows[i][j].ToString().Trim();
                            v = v.Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "").Replace("\\", "\\\\");
                            jsonBuilder.Append(v);
                        }
                        jsonBuilder.Append("\",");
                    }

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("},");
                }
            }
            else
            {
                jsonBuilder.Append(",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);

            jsonBuilder.Append("]");
            //jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <returns></returns>
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
        public static void WriteLog(Exception ex, string LogAddress = "")
        {
            //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
            if (LogAddress == "")
            {
                LogAddress = Environment.CurrentDirectory + '\\' +
                    DateTime.Now.Year + '-' +
                    DateTime.Now.Month + '-' +
                    DateTime.Now.Day + "_Log.log";
            }
            //把异常信息输出到文件
            StreamWriter fs = new StreamWriter(LogAddress, true);
            fs.WriteLine("当前时间：" + DateTime.Now.ToString());
            fs.WriteLine("异常信息：" + ex.Message);
            fs.WriteLine("异常对象：" + ex.Source);
            fs.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
            fs.WriteLine("触发方法：" + ex.TargetSite);
            fs.WriteLine();
            fs.Close();
        }
        private void GetParams()
        {
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Request["page"]))
            {
                PageIndex = Convert.ToInt32(System.Web.HttpContext.Current.Request["Page"]);
            }
            else
            {
                PageIndex = 1;
            }
        }
        #region 获得分页字符
        public string GetDivPager(string queryString, DataTable ds)
        {
            StringBuilder sp = new StringBuilder();
            int TotalCount = TotalRecord;
            int rowCount = TotalPage;
            if (ds != null)
            {
                //sp.AppendFormat(" <p>总记录：<span id=\"sum\">{0}</span>", TotalCount);
                //sp.AppendFormat("  页码：<em><b id=\"current\">{0}</b>/<span id=\"count\">{1}</span></em> ", PageIndex, rowCount);
                //sp.AppendFormat("  每页：<span id=\"eachPage\">{0}</span></p> ", PageSize);

                sp.AppendFormat("  <a  href='{0}'>首页</a> ", "?page=1" + queryString);
                if (PageIndex > 1)
                {
                    sp.AppendFormat("  <a href='{0}'>< 上一页 </a>", "?page=" + (PageIndex - 1) + queryString);
                }
                int temp = 0;
                int loopc = rowCount > 10 ? 10 : rowCount;
                for (int i = 0; i < loopc; i++)
                {
                    temp = i + 1;
                    if (PageIndex > 10) { temp = (PageIndex - 10) + i + 1; }
                    sp.AppendFormat("  <a class=\"{0}\" href='{1}'>{2}</a>", PageIndex == temp ? "active" : "", "?page=" + temp + queryString, temp);
                }
                if (PageIndex != rowCount)
                {
                    sp.AppendFormat("  <a href='{0}'>下一页 ></a>", "?page=" + (PageIndex + 1) + queryString);
                }
                sp.AppendFormat("  <a href='{0}'>尾页</a>", "?page=" + rowCount + queryString);

            }
            else
            {
                ds = null;
            }
            return sp.ToString();
        }
        #endregion
        #region 获取分页的数据
        /// <summary>
        /// 获取分页的数据
        /// </summary>
        /// <param name="TblName">数据表名</param>
        /// <param name="Fields">要读取的字段</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="OrderType">排序方式</param>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="PageSize">每页显示多少条数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="TotalPage">返回值，共有多少页</param>
        /// <param name="TotalRecord">返回值，总有多少条记录</param>
        /// <returns></returns>
        public static DataTable PageData(string TblName, string Fields, string OrderField, string OrderType, string SqlWhere, int PageSize, int pageIndex, out int TotalPage, out int TotalRecord)
        {

            SqlConnection conn = new SqlConnection("Data Source=AMAZING华;Initial Catalog=DataStudents;Integrated Security=True;");
            SqlCommand comm = new SqlCommand("GetDateList", conn);

            comm.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 100)).Value = TblName;
            comm.Parameters.Add(new SqlParameter("@Fields", SqlDbType.NVarChar, 1000)).Value = Fields;
            comm.Parameters.Add(new SqlParameter("@OrderField", SqlDbType.NVarChar, 1000)).Value = OrderField;
            comm.Parameters.Add(new SqlParameter("@OrderType", SqlDbType.NVarChar, 1000)).Value = OrderType;
            comm.Parameters.Add(new SqlParameter("@sqlWhere", SqlDbType.NVarChar, 1000)).Value = SqlWhere;
            comm.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = PageSize;
            comm.Parameters.Add(new SqlParameter("@pageIndex", SqlDbType.Int)).Value = pageIndex;
            comm.Parameters.Add(new SqlParameter("@TotalPage", SqlDbType.Int));

            comm.Parameters["@TotalPage"].Direction = ParameterDirection.Output;//获得out出来的参数值

            comm.Parameters.Add(new SqlParameter("@totalRecord", SqlDbType.Int));
            comm.Parameters["@totalRecord"].Direction = ParameterDirection.Output;

            comm.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comm);
            DataTable ds = new DataTable();
            dataAdapter.Fill(ds);

            TotalPage = (int)comm.Parameters["@TotalPage"].Value;
            TotalRecord = (int)comm.Parameters["@totalRecord"].Value;

            conn.Close();
            conn.Dispose();
            comm.Dispose();

            return ds;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    #endregion
}

