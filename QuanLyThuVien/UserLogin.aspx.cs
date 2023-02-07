using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace QuanLyThuVien
{
    public partial class UserLogin : System.Web.UI.Page
    {
        private readonly string _strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string memberID = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(memberID) || string.IsNullOrWhiteSpace(password))
            {
                Response.Write("<script>alert('Tên tài khoản hoặc mật khẩu không được để trống.')</script>");
                return;
            }
            try
            {

                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("select * from member_master_table where member_id=@memberID AND password=@password", con))
                    {
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Response.Write("<script>alert('" + dr.GetValue(8).ToString() + "');</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Thông tin không hợp lệ');</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return;
            }
        }
    }
}