using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ajax_Newtest
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Log4Helper.Fatal(this.GetType(), "Fatal");
            Log4Helper.Error(this.GetType(), "Error", new Exception("error"));
            Log4Helper.Warn(this.GetType(), "Warn");
            Log4Helper.Info(this.GetType(), "Info");
            Log4Helper.Debug(this.GetType(), "Debug");
            try
            {
                WebRequest request = WebRequest.Create("http://www.joybirds.cn/JMGS/list.aspx?type=guid");
                WebResponse response = request.GetResponse();
                StreamReader readers = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string a = readers.ReadToEnd();
                using (StreamWriter sw = new StreamWriter(@"d:\1.txt"))//将获取的内容写入文本

                {

                    sw.Write(a);

                }
                readers.Close();
                readers.Dispose();
                response.Close();
                //            string URL = "https://www.baidu.com/";
                //            CookieContainer cookie = new CookieContainer();
                //            ArrayList list = GetHtmlData(URL, "", cookie, "utf-8");
                //            string conent = list[1].ToString();
                //            var html = @"div class='1'>
                //<h2>啦啦啦</h2>
                //<ul>
                //<li><a href=""http://"" target=""_blank"">问题</a>";
                //            //MatchCollection mac_title = new RegExp("<body>(.|\s|\r|\n|\f)*</body>").;
                //            //MatchCollection mac = new Regex("<ul class=\"doc_list\">([^^]*?)</ul>").Matches(conent);
                //            Regex reg = new Regex(@"(?<=<body>)(.*?)(?=</body>)", RegexOptions.IgnoreCase);
                //            var pattern = @"<h2>(?<title>.*?)</h2>.*(\r\n.*)*<a\shref=""(?<url>.*?)""";
                //            var match = System.Text.RegularExpressions.Regex.Match(html, pattern);
                //            if (match.Success)
                //            {
                //                var title = match.Groups[1].Value;
                //                var url = match.Groups[2].Value;
                //string text = "One car red car blue car";
                //string pat = @"(\w+)\s+(car)";
                //// Compile the regular expression.
                //Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                //// Match the regular expression pattern against a text string.
                //Match m = r.Match(text);
                //int matchCount = 0;
                //while (m.Success)
                //{
                //    Response.Write("Match" + (++matchCount) + "<br>");
                //    for (int i = 1; i <= 2; i++)
                //    {
                //        Group g = m.Groups[i];
                //        Response.Write("Group" + i + "='" + g + "'" + "<br>");
                //        CaptureCollection cc = g.Captures;
                //        for (int j = 0; j < cc.Count; j++)
                //        {
                //            Capture c = cc[j];
                //            Response.Write("Capture" + j + "='" + c + "', Position=" + c.Index + "<br>");
                //        }
                //    }
                //    m = m.NextMatch();
                //}
            }
            catch (Exception ex)
            {
                tb.Text = ex.Message;
            }

        }
        public static ArrayList GetHtmlData(string postUrl, string Referer, CookieContainer cookie, string code)
        {
            return GetHtmlData(postUrl, Referer, cookie, Encoding.GetEncoding(code));
        }
        /// <summary>
        /// 通过get方式请求页面，传递一个实例化的cookieContainer
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static ArrayList GetHtmlData(string postUrl, string Referer, CookieContainer cookie, Encoding encoding)
        {

            HttpWebRequest request;
            HttpWebResponse response;
            ArrayList list = new ArrayList();
            request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0";
            //HttpWebRequest request;
            //HttpWebResponse response;
            //ArrayList list = new ArrayList();
            //request = WebRequest.Create(postUrl) as HttpWebRequest;
            //request.Method = "GET";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8;";
            //request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            //request.KeepAlive = true;
            //request.Headers.Add("Host", "lib.wyu.edu.cn");
            //request.Proxy = new WebProxy("lib.wyu.edu.cn");

            if (Referer != "")
                request.Referer = Referer;
            request.CookieContainer = cookie;

            try
            {
                //获取服务器返回的资源
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {

                        cookie.Add(response.Cookies);
                        try
                        {
                            System.Web.HttpContext.Current.Session["CookieContainer"] = cookie;
                        }
                        catch { }
                        //保存Cookies
                        list.Add(cookie);
                        list.Add(reader.ReadToEnd());
                        list.Add(Guid.NewGuid().ToString());//图片名
                    }
                }
            }
            catch (WebException ex)
            {
                list.Clear();
                list.Add("发生异常/n/r");
                WebResponse wr = ex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add("5");
                list.Add("发生异常：" + ex.Message);
            }
            return list;
        }
    }
}