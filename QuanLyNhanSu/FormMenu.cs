using QuanLyNhanSu.Resources;
using System;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FormMenu : Form
    {
        public string UserRole { get; set; } 
        public string Username { get; set; } 

        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            lblChaoMung.Text = $"Chào mừng, {Username}! Vai trò: {UserRole}.";
        }

        private void btnQLNhanSu_Click(object sender, EventArgs e)
        {
            FormQuanLyNhanSu frmNhanSu = new FormQuanLyNhanSu();
            frmNhanSu.UserRole = this.UserRole;
            frmNhanSu.Show();
           
        }

        private void btnQLLuong_Click(object sender, EventArgs e)
        {
            FormQuanLyLuong fLuong = new FormQuanLyLuong();
            fLuong.ShowDialog();
        }

        private void btnQLChamCong_Click(object sender, EventArgs e)
        {
            FormChamCong frm = new FormChamCong();
            frm.ShowDialog();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Báo cáo", "Thông báo");
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
        }
    }
}
