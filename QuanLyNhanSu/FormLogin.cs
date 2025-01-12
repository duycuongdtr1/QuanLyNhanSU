using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.Resources
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; 

        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Chuỗi kết nối
            string connStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open();

                    string query = @"
                        SELECT Role, ISNULL(NS_ID, 0) AS NS_ID 
                        FROM Users 
                        WHERE Username = @username AND Password = @password
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string role = dr["Role"].ToString();
                                int nsID = dr["NS_ID"] != DBNull.Value ? Convert.ToInt32(dr["NS_ID"]) : 0;

                                MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (role == "Admin")
                                {
                                    FormMenu menuForm = new FormMenu();
                                    menuForm.UserRole = role;       
                                    menuForm.Username = username;
                                    menuForm.Show();
                                }
                                else if (role == "User")
                                {
                                    FormUser userForm = new FormUser();
                                    userForm.UserRole = role;       
                                    userForm.Username = username;
                                    userForm.NhanSuID = nsID;       
                                    userForm.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Không xác định quyền truy cập!",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                // Ẩn FormLogin
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
