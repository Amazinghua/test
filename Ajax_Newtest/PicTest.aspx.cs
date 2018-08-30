using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Ajax_Newtest
{
    public partial class PicTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        string SQLString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(SQLString))
                {
                    string FullName = FileUpload1.PostedFile.FileName;//获取图片物理地址
                    FileInfo fi = new FileInfo(FullName);
                    string name = fi.Name;//获取图片名称
                    string type = fi.Extension;//获取图片类型

                    if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".png")
                    {
                        string SavePath = Server.MapPath("~\\excel");//图片保存到文件夹下
                        this.FileUpload1.PostedFile.SaveAs(SavePath + "\\" + name);//保存路径
                        this.Image1.Visible = true;
                        this.Image1.ImageUrl = "~\\excel" + "\\" + name;//界面显示图片
                        string urimage = this.Image1.ImageUrl;
                        string sql = "insert into image1(ImageName,ImageType,ImagePath) values('" + name + "','" + type + "','" + urimage + "')";
                        SqlCommand cmd = new SqlCommand(sql, sqlcon);
                        sqlcon.Open();
                        cmd.ExecuteNonQuery();
                        this.label1.Text = "上传成功";
                    }
                    else
                    {
                        this.label1.Text = "请选择正确的格式图片";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}