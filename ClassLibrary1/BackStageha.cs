using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;


namespace GetMealTicketDB
{
    public class BackStage
    {
     
        /// <summary>
        /// 检查是否存在相关人员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool checkid (string name, string password)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = "select count(*) from Table_test where name = '" + name +  "' and pssword= '" + password + "'";
                DataTable dt = doh.GetDataTable();
                doh.Dispose();
                string r = dt.Rows[0][0].ToString();
                if(r =="0")
                {
                    rst = true;
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
        /// 将注册的用户更新到表中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool insertToTabel(string name,string password,string id)
        {
            bool rst = false;
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = " set rowcount 1 update Table_test set name = '" + name + "' where (name is null or name = '') and password = '" + password + "' and id = '" + id + "'";
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
        /// 将相关信息返回给前台
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable getToMsg(string name,string password,string id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDbOperHandler doh = new SqlDbOperHandler();
                doh.Reset();
                doh.SqlCmd = " select * from Table_test where name = '" + name +"' and id = '" + id + "'";
                dt = doh.GetDataTable();
                doh.Dispose();
                return dt;
            }
            catch (Exception e)
            {
                return dt;
            }
        }
    }
}

