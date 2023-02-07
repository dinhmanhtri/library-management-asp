using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyThuVien
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        private readonly string _strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Response.Write("<script>alert('Tên tài khoản hoặc mật khẩu không được để trống.')</script>");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand($"select * from admin_tbl where username='{username}' AND password='{password}'", con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Response.Write("<script>alert('Đăng nhập thành công');</script>");
                                    
                                    Session["username"] = dr.GetValue(0).ToString();
                                    Session["fullname"] = dr.GetValue(2).ToString();
                                    Session["role"] = "admin";
                                    //Session["status"] = dr.GetValue(10).ToString();
                                }
                                Response.Redirect("homepage.aspx");
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
            }
        }
    }
}