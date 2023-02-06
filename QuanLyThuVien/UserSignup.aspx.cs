using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyThuVien
{
    public partial class UserSignup : System.Web.UI.Page
    {
        private readonly string _strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // sign up button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                Response.Write($"<script>alert('Tên tài khoản đã tồn tại hoặc trống. Vui lòng thử tên khác!')</script>");
            }
            else
            {
                signUpNewMember();
            }

        }

        /// <summary>
        /// Check member exists
        /// </summary>
        /// <returns></returns>
        bool checkMemberExists()
        {
            using (SqlConnection con = new SqlConnection(_strcon))
            {
                if (string.IsNullOrWhiteSpace(TextBox8.Text.Trim()))
                { 
                    return true; 
                }
                try
                {
                    con.Open();

                    string cmdText = $"SELECT * FROM member_master_tbl WHERE member_id = '{TextBox8.Text.Trim()}'";
                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            return dt.Rows.Count >= 1;
                        }
                    }


                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);

                    return false;
                }
            }
        }

        void signUpNewMember()
        {
            using (SqlConnection con = new SqlConnection(_strcon))
            {
                try
                {
                    con.Open();

                    string cmdText = "EXEC spInsertMember @member_id, @full_name, @dob, @contact_no, @email, @province, @district, @pin_code, @full_address, @password, @account_status";
                    SqlCommand cmd = new SqlCommand(cmdText, con);
                    cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@province", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@district", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@pin_code", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@account_status", "pending");

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Đăng ký thành công!');</script>");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }
            }
        }
    }
}