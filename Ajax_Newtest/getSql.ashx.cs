using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using GetMealTicketDB;
using DBUtility;

namespace Ajax_Newtest
{
    /// <summary>
    /// getSql 的摘要说明
    /// </summary>
    public class getSql : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            string type = "";
            try
            {
                string poststr = getpost();
                if (!string.IsNullOrEmpty(poststr))
                {
                    JObject jobj = JObject.Parse(poststr);
                  //  type = jobject(jobj, "type");
                    string namevalue = jobject(jobj, "namevalue");
                    string passwordvalue = jobject(jobj, "passwordvalue");
                    string idvalue = jobject(jobj, "idvalue");
                    switch (idvalue)
                    {
                        case "0":
                            bool isUpdate = Update(namevalue);
                            if (isUpdate)
                            {
                                DataTable dt = select();
                                result = "{\"result\":\"000\",\"foodid_dt\":" + DataTableToJson(dt) + "}";
                            } else
                            {
                                result = "{\"result\":\"555\"}";
                            }
                            break;
                        case "1":
                            DataTable dt2 = select();
                            result = "{\"result\":\"111\",\"foodid_dt\":" + DataTableToJson(dt2) + "}";
                            break;
                        default:
                            bool isInsert = Added(namevalue, passwordvalue, idvalue);
                            if (isInsert)
                            {
                             
                               

                                DataTable dt3 = select();
                                result = "{\"result\":\"222\",\"foodid_dt\":" + DataTableToJson(dt3) + "}";
                               
                            } else
                            {
                                result = "{\"result\":\"333\"}";
                            }
                          
                            break;
                    }
                }   
            }
            catch (Exception e)
            {
                result = "{\"result\":\"222\"}";
            }
            context.Response.Write(result);
            context.Response.End();
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public static DataTable select()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select * from Table_test";
                dt = doh.GetDataTable();

                doh.Dispose();
                return dt;
            }
            catch (Exception e)
            {
                return dt;
            }
        }
        public static bool Added(string name,string Password,string id)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "insert into Table_test (name,Password,id) values ('" + name + "'," + Password + ",'" + id + "')";
                doh.ExecuteSqlNonQuery();
                doh.Dispose();
                rst = true;
                return rst;
            }
            catch (Exception e)
            {
                return rst;
            }
        }
        public static bool Update(string name)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "UPDATE Table_test set name ='" + name + "' where (Password ='2342') ";
                doh.ExecuteSqlNonQuery();
                doh.Dispose();
                rst = true;
                return rst;
            }
            catch (Exception e)
            {
                return rst;
            }
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
    }
}