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

                    string cmdText = $"SELECT * FROM member_master_tbl WHERE member_id = '{memberID}' AND password = '{password}'";
                    SqlCommand cmd = new SqlCommand(cmdText, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        // Login successful

                        Response.Write("<script>alert('Đăng nhập thành công!')</script>");
                        Session["username"] = memberID;
                        Response.Redirect("HomePage.aspx");
                    }
                    else
                    {
                        // Login failed
                        Response.Write("<script>alert('Tên tài khoản hoặc mật khẩu không đúng.')</script>");
                        return;
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