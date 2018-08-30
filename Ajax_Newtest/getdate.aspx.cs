using DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ajax_Newtest
{
    public partial class getdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Getdate();
            }
        }
        private void Getdate()
        {
            string type = "";
            string id = "";
            string name = "";
            string image = "";
            string typename = "";
            string postdata = GetPost();
            if (!string.IsNullOrEmpty(postdata))
            {
                JObject json = new JObject();
                try
                {
                    json = JObject.Parse(postdata);
                }
                catch
                {

                }
                //获取前台请求的数据
                type = GetJObject(json, "type");
                id = GetJObject(json, "id");
                name = GetJObject(json, "name");
                image = GetJObject(json, "image");
                typename = GetJObject(json, "id");
            }
            string returnstr = "";
            try
            {
                switch (type.ToLower())
                {
                    case "test":
                        #region
                        returnstr = "{\"code\":\"0\",\"text\":\"成功\",\"hellow\":\" 成功\"" + "}";
                        #endregion
                        break;

                    case "checknot":
                        #region
                        bool isExist = Checkid(name, id);//检查是否存在相关信息
                        if (isExist == true)
                        {
                            DataTable dt1 = GetToMsg(name, id);
                            returnstr = "{\"code\":\"1\",\"date\":" + DataTableToJson(dt1) + "}";//返回成功时的完整的表
                        }
                        else
                        {
                            returnstr = "{\"code\":\"2\"}";//返回不成功
                        }
                        #endregion
                        break;
                    case "sh":
                        if (typename == "image1")
                        {
                            string image1 = SaveFile1(id);
                            Picfirst(image1, id);
                        } else if(typename == "image2")
                        {
                            string image2 = SaveFile2(id);
                            PicSec(image2, id);
                        } else if (typename == "image3")
                        {
                            string image3 = SaveFile3(id);
                            PicThird(image3, id);
                        }
                        //returnstr = "{\"code\":\"3\",\"date\":" + DataTableToJson(dt2) + "}";
                        break;
                    case "image1":
                        string basePath = "./NewFolder1";
                        string names;
                        basePath = System.Web.HttpContext.Current.Server.MapPath(basePath);
                        HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                        if (!System.IO.Directory.Exists(basePath))
                        {
                            System.IO.Directory.CreateDirectory(basePath);
                        }
                        var suffix = files[0].ContentType.Split('/');
                        var _suffix = suffix[1];
                        var _temp = System.Web.HttpContext.Current.Request["name"];
                        //如果不修改文件名，则创建随机文件名  
                        if (!string.IsNullOrEmpty(_temp))
                        {
                            name = _temp;
                        }
                        else
                        {
                            Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                            name = rand.Next() + "." + _suffix;
                        }
                        var full = basePath + name;
                        files[0].SaveAs(full);
                        break;
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                returnstr = "{\"result\":\"error\",\"msg\":\"" + result + "\",\"date\":\"\"" + "}";
            }
            Response.Write(returnstr);
            Response.End();
        }
        string GetPost()
        {
            string g = "";
            if (HttpContext.Current.Request.InputStream != null)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream, System.Text.Encoding.UTF8);
                g = reader.ReadToEnd();
                // Voting.Common.WebCommon.Log("\n---------------------------\n" + g + "\n----------------------------------\n");
                //g = g.ToLower();
            }
            return g;
        }
        string GetJObject(JObject json, string key)
        {
            string v = "";
            if (json[key] != null)
                v = json[key].ToString().Trim();
            return v;
        }
        public string DataTableToJson(DataTable dt)
        {
            string result = "";
            result = JsonConvert.SerializeObject(dt, new DataTableConverter());
            return result;
        }
        /// <summary>
        /// 检查用户输入信息是否匹配
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Checkid(string name, string id)
        {
            bool rst = true;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select count (*) from Table_test where name = '" + name + "' and id= '" + id + "'";
                DataTable dt = doh.GetDataTable();
                string r = dt.Rows[0][0].ToString();
                if (r == "0")
                {
                    rst = false;
                    return rst;
                }
                return rst;
            }
            catch (Exception e)
            {
                rst = false;
                return rst;
            }
        }
        /// <summary>
        /// 将相关信息返回给前台
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetToMsg(string name, string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = " select * from Table_test where name = '" + name + "' and id = '" + id + "'";
                dt = doh.GetDataTable();
                doh.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public  void Picfirst(string pic,string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "UPDATE Table_test set image1 ='" + pic + "' where (id ='" + id + "') ";
                dt = doh.GetDataTable();
                doh.Dispose();
            }
            catch (Exception ex)
            {
                string result = ex.Message;
            }
        }
        public void PicSec(string pic, string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "UPDATE Table_test set image2 ='" + pic + "' where (id ='" + id + "') ";
                dt = doh.GetDataTable();
                doh.Dispose();
            }
            catch (Exception ex)
            {
                string result = ex.Message;
            }
        }
        public void PicThird(string pic, string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "UPDATE Table_test set image3 ='" + pic + "' where (id ='" + id + "') ";
                dt = doh.GetDataTable();
                doh.Dispose();
            }
            catch (Exception ex)
            {
                string result = ex.Message;
            }
        }
        public DataTable image1(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = " select * from Table_test where id = '" + id  + "'";
                dt = doh.GetDataTable();
                doh.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        public string SaveFile1(string id)
        {
            string basePath = "./imagedown/" + id + "/";
            string name;
            basePath = System.Web.HttpContext.Current.Server.MapPath(basePath);
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            var _temp = System.Web.HttpContext.Current.Request["name"];
            _temp = "经营许可证正面" + id;
            name = _temp;
            var full = basePath + name;
            files[0].SaveAs(full);
            return full;
        }
        public string SaveFile2(string id)
        {
            string basePath = "./imagedown/" + id + "/";
            string name;
            basePath = System.Web.HttpContext.Current.Server.MapPath(basePath);
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            var _temp = System.Web.HttpContext.Current.Request["name"];
            _temp = "经营场所正面" + id;
            name = _temp;
            var full = basePath + name;
            files[0].SaveAs(full);
            return full;
        }
        public string SaveFile3(string id)
        {
            string basePath = "./imagedown/" + id + "/";
            string name;
            basePath = System.Web.HttpContext.Current.Server.MapPath(basePath);
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            var _temp = System.Web.HttpContext.Current.Request["name"];
            _temp = "工作场所" + id;
            name = _temp;
            var full = basePath + name;
            files[0].SaveAs(full);
            return full;
        }
    }
}