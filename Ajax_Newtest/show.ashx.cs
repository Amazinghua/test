using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

//namespace Ajax_Newtest
//{
//    /// <summary>
//    /// show 的摘要说明
//    /// </summary>
//    public class show : IHttpHandler
//    {

//        public void ProcessRequest(HttpContext context)
//        {
//            context.Response.ContentType = "text/plain";
//            string result = "";
//            string type = "";
//            try
//            {
//                string poststr = getpost();
//                if (!string.IsNullOrEmpty(poststr))
//                {
//                    JObject jobj = JObject.Parse(poststr);
//                    type = jobject(jobj, "type");
//                    switch (type)
//                    {
//                        case "getdata":
//                            String path = @"C:\Users\Administrator\Desktop\文件上传\文件上传\Solution1\WebApplication1\bootstrap-fileinput-master\examples\txt";

//                            break;
//                    }
//                }
//            }
//            catch (Exception e)
//            {

//            }
//            context.Response.Write(result);
//            context.Response.Write("fuck");
//            context.Response.End();
//        }
//        //context.Response.ContentType = "text/plain";
//        private void GetZipfile()
//        {
//            DirectoryInfo theFolder = new DirectoryInfo("C:\\Users\\Administrator\\Desktop\\文件上传\\文件上传\\Solution1\\WebApplication1\bootstrap-fileinput-master\\examples\\txt\\");  // 给出你的目录文件位置 

//            FileInfo[] fileInfo = theFolder.GetFiles(); // 获得当前的文件夹内的所有文件数组

//            foreach (FileInfo NextFile in fileInfo)   //遍历文件
//            {
//                if (NextFile.Extension == ".zip")  // 得到你想要的格式
//                {
//                    this.ListBox1.Items.Add(NextFile.Name); // 用于测试输出
//                }
//            }
//        }
//        public string getpost()
//        {
//            string g = "";
//            if (HttpContext.Current.Request.InputStream != null)
//            {
//                System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream, System.Text.Encoding.UTF8);
//                g = sr.ReadToEnd();
//            }
//            return g;
//        }
//        /// <summary>
//        /// 从jobject中获取相应的数据 
//        /// </summary>
//        /// <param name="jobj">jobject对象</param>
//        /// <param name="key">要获取的值</param>
//        /// <returns></returns>
//        public string jobject(JObject jobj, string key)
//        {
//            string hh = "";
//            if (jobj[key] != null)
//            {
//                hh = jobj[key].ToString().Trim();
//            }
//            return hh;
//        }

//        public bool IsReusable
//        {
//            get
//            {
//                return false;
//            }
//        }
//    }
//}