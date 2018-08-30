using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text;
using System.Web;
using DBUtility;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Spider;
using System.Net;
using System.Threading.Tasks;

using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Ajax_Newtest
{
    /// <summary>
    /// Getdata2 的摘要说明
    /// </summary>
    public class Getdata2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            string type = "";
            string total = "";
            int pageSize = 2;
            try
            {
                string poststr = getpost();
                if (!string.IsNullOrEmpty(poststr))
                {
                    JObject jobj = JObject.Parse(poststr);
                    type = jobject(jobj, "type");
                    string namevalue = jobject(jobj, "name");
                    string contendvalue = jobject(jobj, "content");
                    string pageNow = jobject(jobj, "pageNow");
                    string id = jobject(jobj, "id");
                    switch (type)
                    {
                        case "liuyan":

                            break;
                        case "all":
                            DataTable dt = gettoMsg(pageNow);
                            total = dt.Rows.Count.ToString();
                            result = "{\"code\":\"0\",\"total\" :\"" + total + "\",\"data\":" + DataTableToJson(dt) + "}";
                            break;
                        case "search":
                            string bstructure = " id|name|creditCode|regStatus|regDate";
                            string sql = ArrayKeyWord(contendvalue, bstructure);
                            DataTable dtMsg = ShowMsg(sql);
                            total = dtMsg.Rows.Count.ToString();
                            result = "{\"code\":\"0\",\"total\" :\"" + total + "\",\"data\":" + DataTableToJson(dtMsg) + "}";
                            break;
                        case "byId":
                            DataTable dt_id = SearchById(id);
                            total = dt_id.Rows.Count.ToString();
                            result = "{\"code\":\"0\",\"total\" :\"" + total + "\",\"data\":" + DataTableToJson(dt_id) + "}";
                            break;
                        case "dele":
                            string[] RegisterCodes = new string[5] { "9", "8", "4", "6", "3" };
                            var flag = true;
                            var registerCodesStr = string.Empty;
                            foreach (var registerCode in RegisterCodes)
                            {
                                if (string.IsNullOrEmpty(registerCode))
                                    continue;

                                var workoreder = registerCode;
                                if (!flag || workoreder == null)
                                {
                                    registerCodesStr += registerCode + " ; ";
                                    flag = false;
                                    continue;
                                }
                                if (int.Parse(registerCode) > 5)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                }
                                if (flag == false)
                                { registerCodesStr += registerCode; continue; }


                            }
                            if (!string.IsNullOrEmpty(registerCodesStr))
                            {
                                Console.WriteLine("不成功的有:" + registerCodesStr);
                            }
                            break;
                        case "getname":
                            string urls = "http://localhost:51112/example_zip/" + "test1.zip";
                            var webclient = new WebClient();
                            webclient.DownloadData(urls);
                            var t = webclient.ResponseHeaders;

                            break;
                        case "testQR":
                            string strCode = "王天华";
                            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
                            QRCode qrcode = new QRCode(qrCodeData);

                            // qrcode.GetGraphic 方法可参考最下发“补充说明”
                            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
                            MemoryStream ms = new MemoryStream();
                            qrCodeImage.Save(ms, ImageFormat.Jpeg);
                            qrCodeImage.Save("C:\\Users\\Administrator\\Desktop\\test.jpg");
                            break;
                        case "zip":
                            string name = "test1";

                            var paths = DownLoad(name, "1");
                            string path_name = Path.GetFileName(paths);
                            //byte[] bytes = GetFileDate(paths);
                            //string filebody = System.Text.Encoding.UTF8.GetString(bytes);
                            byte[] bytes = File.ReadAllBytes(paths);
                            //var str = Convert.ToBase64String(File.ReadAllBytes(paths));


                            //unZipfile(paths,unzip); 
                            string unziped = "D:\\Documents\\Visual Studio 2017\\Projects\\Ajax_Newtest\\Ajax_Newtest\\unzipped\\" + name;
                            //DirectoryInfo di = new DirectoryInfo("D:\\Documents\\Visual Studio 2017\\Projects\\Ajax_Newtest\\Ajax_Newtest\\unzipped\\" + name);
                            //FindFile(di);
                            break;
                        case "gai":
                            string fileDir = HttpContext.Current.Request.PhysicalApplicationPath;
                            string savePath = fileDir + "LoadZip\\";
                            string zipName = "test2.zip";
                            string rootMark = savePath + zipName;
                            Crc32 crc = new Crc32();
                            ZipOutputStream outPutStream = new ZipOutputStream(File.Create(rootMark));
                            outPutStream.SetLevel(9);
                            FileStream fileStream = File.OpenRead("C:\\Users\\Administrator\\Desktop\\test.xlsx");
                            byte[] buffer = new byte[fileStream.Length];
                            fileStream.Read(buffer, 0, buffer.Length);
                            ZipEntry entry = new ZipEntry("C:\\Users\\Administrator\\Desktop\\test.xlsx".Replace(savePath, string.Empty));
                            entry.DateTime = DateTime.Now;
                            entry.Size = fileStream.Length;
                            crc.Reset();
                            crc.Update(buffer);
                            entry.Crc = crc.Value;
                            outPutStream.PutNextEntry(entry);
                            outPutStream.Write(buffer, 0, buffer.Length);
                            fileStream.Close();
                            outPutStream.Finish();
                            outPutStream.Close();
                            break;
                        case "check":
                            var downLoadUrl = "http://www.e-irobot.com/api/web_all_source";
                            HttpHelper http = new HttpHelper();
                            HttpItem item = new HttpItem()
                            {
                                URL = downLoadUrl,
                                Method = "POST",
                                UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.62 Safari/537.36",
                                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                                ContentType = "application/x-www-form-urlencoded",
                                Referer = "http://120.237.31.12/",
                                Allowautoredirect = false,
                                ResultType = ResultType.Byte,

                            };
                            HttpResult results = http.GetHtml(item);

                            var dirPath = AppDomain.CurrentDomain.BaseDirectory + @"DownLoad\";
                            if (!Directory.Exists(dirPath))
                                Directory.CreateDirectory(dirPath);
                            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                            var r1 = new Random().Next(9);
                            var r2 = new Random().Next(9);
                            var r3 = new Random().Next(9);
                            var r4 = new Random().Next(9);
                            var r = r1 + r2 + r3 + r4;
                            string filePath;
                            //只能使用xls格式的excel文件
                            filePath = dirPath + epoch + r + ".txt";

                            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                            fs.Write(results.ResultByte, 0, results.ResultByte.Length);
                            fs.Flush();
                            fs.Close();
                            break;
                        case "list":

                            string dataSource = "phone=15015051037";

                            var url = "https://www.e-irobot.com/api/get_phone";


                            // 使用ASCII码字符

                            byte[] datalogins = Encoding.ASCII.GetBytes(dataSource);
                            HttpWebRequest res = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                            res.Method = "Post";

                            res.ContentType = "application/x-www-form-urlencoded";
                            res.ContentLength = datalogins.Length;
                            Stream newstream = res.GetRequestStream();
                            newstream.Write(datalogins, 0, datalogins.Length);
                            newstream.Close();

                            //接收相应  
                            HttpWebResponse resp = (HttpWebResponse)res.GetResponse();
                            Stream respStream = resp.GetResponseStream();
                            StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                            string resultsss = reader.ReadToEnd();
                            reader.Close();
                            res.Abort();
                            ////发送信息  
                            //WebRequest req = WebRequest.Create(url);
                            //req.Method = "post";
                            //req.ContentType = "application/x-www-form-urlencoded";
                            //req.CookieContainer = cookies;//设置cookies
                            ////Accept - Language:zh - CN,zh; q = 0.8  
                            //req.Headers.Add("Accept-Language", "zh - CN,zh; q = 0.8");
                            ////指定客户端代理的方式  

                            ////req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2560.0 Safari/537.36");  
                            ////添加Post参数  
                            //Stream reqStream = req.GetRequestStream();
                            //reqStream.Write(data, 0, data.Length);
                            //reqStream.Close();
                            ////接收相应  
                            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                            //Stream respStream = resp.GetResponseStream();
                            //StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                            //string resultsss = reader.ReadToEnd();
                            //reader.Close();
                            break;
                        case "excel":
                            try
                            {
                                SqlDbOperHandler doh = new SqlDbOperHandler();
                                doh.Reset();
                                doh.SqlCmd = "select from Table_yan";
                                DataTable excelTab = doh.GetDataTable();
                                ExportExcel(excelTab);

                            }
                            catch (Exception ex)
                            {
                                WriteLog(ex, "C:\\Users\\Amazinghua\\Desktop\\log.txt");
                            }

                            break;
                        case "datatable"://excel读入内存表，批量插入数据库
                            string path = System.Web.HttpContext.Current.Server.MapPath("~/excel/");
                            string path_zip = path + "gs_0805.xls";//目标文件
                            //DataTable fakeTable = ReadExcelToDataTable(path_zip);
                            //BatchInsertBySqlBulkCopy(fakeTable, "gs_0805");
                            FileInfo fi = new FileInfo(path_zip);
                            string test = (fi.Length / 1024).ToString();
                            if(int.Parse(test) <= 2)
                            {

                            }
                            else
                            {

                            }
                            break;
                        case "gstest":

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, "C:\\Users\\Amazinghua\\Desktop\\log.txt");

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
        public string unZipfile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();

                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }

        protected byte[] GetFileDate(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public static void FindFile(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                Console.WriteLine("文件：" + fis[i].FullName);
            }
            DirectoryInfo[] dis = di.GetDirectories();
            for (int j = 0; j < dis.Length; j++)
            {
                Console.WriteLine("目录：" + dis[j].FullName);
                FindFile(dis[j]);
            }
        }
        public static string DownLoad(string name, string true_name)
        {
            string filepath;
            try
            {
                var downLoadUrl = "http://localhost:51112/example_zip/" + name + ".zip";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(downLoadUrl);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                string path = System.Web.HttpContext.Current.Server.MapPath("~/zip/");
                Stream stream = new FileStream(path + true_name + ".zip", FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                filepath = path + true_name + ".zip";
                return filepath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// 批量插入SqlBulkCopy
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName">表名</param>
        public static void BatchInsertBySqlBulkCopy(DataTable dt, string tableName)
        {
            using (SqlBulkCopy sbc = new SqlBulkCopy(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                sbc.BatchSize = dt.Rows.Count;
                sbc.BulkCopyTimeout = 60;
                sbc.DestinationTableName = tableName;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sbc.ColumnMappings.Add(dt.Columns[i].ColumnName, i);
                }
                //全部写入数据库
                sbc.WriteToServer(dt);
            }
        }
        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            NPOI.SS.UserModel.ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                NPOI.SS.UserModel.IWorkbook workbook = NPOI.SS.UserModel.WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    NPOI.SS.UserModel.IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            NPOI.SS.UserModel.ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual bool Upload_attach(string registerCode, string submitFlag, string token)
        {

            string path = System.Web.HttpContext.Current.Server.MapPath("~/zip/");
            string path_zip = path + registerCode + ".zip";//目标文件
            if (File.Exists(path_zip))
            {
                string path_name = Path.GetFileName(path_zip);
                //byte[] bytes = GetFileDate(path_zip);
                //string filebody = System.Text.Encoding.UTF8.GetString(bytes);
                var registerCodeEn = System.Web.HttpUtility.UrlEncode(registerCode, Encoding.UTF8);
                var filenameEn = System.Web.HttpUtility.UrlEncode(path_name, Encoding.UTF8);
                var submitFlagEn = System.Web.HttpUtility.UrlEncode(submitFlag, Encoding.UTF8);
                var tokenEn = System.Web.HttpUtility.UrlEncode(token, Encoding.UTF8);
                byte[] bytess = File.ReadAllBytes(path_zip);
                string filebody = Convert.ToBase64String(bytess);
                string url = $"http://{registerCodeEn}/App/InterfacesApply/Service.ashx";
                try
                {
                    LitJson.JsonData jo = new LitJson.JsonData
                    {
                        ["act"] = "NoticyCom",
                        ["method"] = "File",
                        ["sourcekey"] = registerCodeEn,
                        ["filename"] = path_name,
                        ["filebody"] = Convert.ToBase64String(File.ReadAllBytes(path_zip)),
                        ["submitflag"] = submitFlagEn,
                        ["token"] = tokenEn
                    };
                    string value = jo.ToJson();
                    byte[] requestBytes = Encoding.UTF8.GetBytes(value);
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencode";
                    req.ContentLength = requestBytes.Length;

                    Stream requestStream = req.GetRequestStream();
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();

                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    string backstr = sr.ReadToEnd();
                    sr.Close();
                    sr = null;
                    res.Close();
                    res = null;

                    var jObject = JObject.Parse(backstr);
                    var stateCode = jObject["stateCode"].ToString().Trim();
                    //Write(registerCode + " " + DateTime.Now.ToString("yyyyMMddHHmmss"), backstr);
                    if (stateCode != "0")
                    {
                        //添加异常记录
                        return false;
                    }
                    //using (HttpClient httpClient = new HttpClient())
                    //{
                    //    Task<String> response =
                    //        httpClient.GetStringAsync(
                    //            $"http://{public_test}/App/InterfacesApply/Service.ashx?act=NoticyCom&method=File&sourcekey={registerCodeEn}&filename={filenameEn}&filebody={filebody}&submitflag={submitFlagEn}&token={token}");
                    //    var jObject = JObject.Parse(response.Result);
                    //    var stateCode = jObject["stateCode"].ToString().Trim();
                    //    if(stateCode != "0")
                    //    {
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        return true;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    //FileLog.Write(ex);
                    return false;
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        /// <summary>
        /// 调用上传附件接口
        /// </summary>
        /// <param name="registerCode">互联网编号</param>
        /// <returns>true代表成功,false代表失败</returns>
        //public virtual bool File_attach(string registerCode,string sourcekey,string filename,string filebody,string submitflag)
        //{
        //    var registerCodeEn = System.Web.HttpUtility.UrlEncode(registerCode, Encoding.UTF8);
        //    string url = $"http://12345.jiangmen.gov.c/App/InterfacesApply/Service.ashx";
        //    try
        //    {

        //    }

        //}
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
        public static DataTable gettoMsg(string pageNow)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select top 2* from Enter_test as AA where id not in (select top " + (int.Parse(pageNow) - 1) * 2 + " id from Enter_test)";
                dt = doh.GetDataTable();
                doh.Dispose();
                return dt;
            }
            catch (Exception e)
            {
                return dt;
            }

        }

        public static DataTable SearchById(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select * from Enter_test where id ='" + id + "'";
                dt = doh.GetDataTable();
                doh.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
        public static DataTable ShowMsg(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (sql == "")
                {
                    sql = "1=1";
                }
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select * from Enter_test where " + sql;

                string a = "select * from Enter_test where" + sql;
                dt = doh.GetDataTable();
                doh.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return dt;
        }
        public static string ArrayKeyWord(string keywords, string dbstruture)
        {
            string condition = "";
            string[] keywordList = dbstruture.Split('|');
            try
            {
                if (keywordList.Length > 1)
                {
                    for (int i = 0; i < keywordList.Length; i++)
                    {
                        condition += keywordList[i] + " like '%" + keywords + "%' or ";

                    }
                    condition = condition.Substring(0, condition.Length - 3);
                    condition = condition.Replace("[", "[[]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return condition;
        }
        public static bool Added(string contend, string name, string time)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "insert into Table_yan (contend,name,time) values ('" + contend + "','" + name + "','" + time + "')";
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
        public void ExportExcel(DataTable dt)
        {
            try
            {
                //创建一个工作簿
                IWorkbook workbook = new HSSFWorkbook();

                //创建一个 sheet 表
                ISheet sheet = workbook.CreateSheet(dt.TableName);

                //创建一行
                IRow rowH = sheet.CreateRow(0);

                //创建一个单元格
                ICell cell = null;

                //创建单元格样式
                ICellStyle cellStyle = workbook.CreateCellStyle();

                //创建格式
                IDataFormat dataFormat = workbook.CreateDataFormat();

                //设置为文本格式，也可以为 text，即 dataFormat.GetFormat("text");
                cellStyle.DataFormat = dataFormat.GetFormat("@");

                //设置列名
                foreach (DataColumn col in dt.Columns)
                {
                    //创建单元格并设置单元格内容
                    rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);

                    //设置单元格格式
                    rowH.Cells[col.Ordinal].CellStyle = cellStyle;
                }

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //跳过第一行，第一行为列名
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                        cell.CellStyle = cellStyle;
                    }
                }

                //设置导出文件路径
                string path = HttpContext.Current.Server.MapPath("Export/");

                //设置新建文件路径及名称
                string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);

                //创建一个 IO 流
                MemoryStream ms = new MemoryStream();

                //写入到流
                workbook.Write(ms);

                //转换为字节数组
                byte[] bytes = ms.ToArray();

                file.Write(bytes, 0, bytes.Length);
                file.Flush();

                //还可以调用下面的方法，把流输出到浏览器下载
                OutputClient(bytes);

                //释放资源
                bytes = null;

                ms.Close();
                ms.Dispose();

                file.Close();
                file.Dispose();

                workbook.Close();
                sheet = null;
                workbook = null;
            }
            catch (Exception ex)
            {

            }
        }
        public void OutputClient(byte[] bytes)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Buffer = true;

            response.Clear();
            response.ClearHeaders();
            response.ClearContent();

            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));

            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");

            response.BinaryWrite(bytes);
            response.Flush();

            response.Close();
        }

        public static DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select * from Table_yan";
                dt = doh.GetDataTable();

                doh.Dispose();
                return dt;
            }
            catch (Exception e)
            {
                return dt;
            }
        }


        public static bool Update(string name, string password)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "UPDATE Table_2 set password ='" + password + "' where (name ='" + name + "') ";
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


        public static bool Deleteing(string name)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "DELETE FROM Table_2 WHERE name = '" + name + "' ";
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
            string inj_str = "'| and | exec | insert | select | delete | update | count |*|%| chr | mid | master | truncate | char | declare |;| or |+|/";
            string inj_strA = inj_str.ToUpper();
            string[] injStra = inj_str.Split('|');
            string[] injStrA = inj_strA.Split('|');
            foreach (var inj in injStra)
            {
                hh = hh.Replace(inj, "");
            }
            foreach (var inj in injStrA)
            {
                hh = hh.Replace(inj, "");
            }
            return hh;
        }
    }
}