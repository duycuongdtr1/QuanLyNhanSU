using QuanLyNhanSu.Resources;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyNhanSu
{
    public partial class FormUser : Form
    {
        public string UserRole { get; set; }
        public string Username { get; set; }
        public int NhanSuID { get; set; } // ID trong bảng NhanSu
        public string HoTen { get; private set; }
        public string Email { get; private set; }
        public string NgaySinh { get; private set; }
        public string DiaChi { get; private set; }
        public string Phone { get; private set; }

        public FormUser()
        {
            InitializeComponent();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            // Lấy họ tên nhân sự
            string hoTen = LayHoTenNhanSu(NhanSuID);
            lblHelloUser.Text = $"Xin chào, {Username}!\nHôm nay là {DateTime.Now.ToShortDateString()}";
        }

        private string LayHoTenNhanSu(int nsID)
        {
            if (nsID <= 0) return "Unknown";

            string hoTen = "";
            string connStr = ConfigurationManager.ConnectionStrings["conStr"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Lỗi: Không đọc được chuỗi kết nối 'conStr' trong App.config");
                return hoTen;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT HoTen FROM NhanSu WHERE ID=@id", con);
                    cmd.Parameters.AddWithValue("@id", nsID);
                    object rs = cmd.ExecuteScalar();
                    if (rs != null)
                        hoTen = rs.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy họ tên nhân sự: " + ex.Message);
            }

            return hoTen;
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            ChamCong_CheckIn();
        }

        private void ChamCong_CheckIn()
        {
            // 1. Xác định ID nhân viên
            int nsID = this.NhanSuID;
            if (nsID <= 0)
            {
                MessageBox.Show("Không xác định được ID nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Lấy thời gian hiện tại
            DateTime now = DateTime.Now;
            DateTime datePart = now.Date;         // chỉ ngày
            TimeSpan timePart = now.TimeOfDay;    // chỉ giờ

            // 3. Xác định ca làm
            string ca = XacDinhCaLam(now);

            // 4. Xác định có trễ không
            bool diTre = KiemTraDiTre(now, ca);

            // 5. Lấy connection string từ App.config
            string connStr = ConfigurationManager.ConnectionStrings["conStr"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Lỗi: Không đọc được chuỗi kết nối 'conStr' trong App.config", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 6. Kiểm tra xem nhân viên đã chấm công hôm nay chưa
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    // Kiểm tra nếu đã chấm công hôm nay
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ChamCong WHERE ID=@id AND NgayChamCong=@ngay", con);
                    checkCmd.Parameters.AddWithValue("@id", nsID);
                    checkCmd.Parameters.AddWithValue("@ngay", datePart);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bạn đã chấm công hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 7. Insert vào bảng ChamCong
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO ChamCong(ID, NgayChamCong, GioChamCong, CaLam, IsLate)
                        VALUES(@id, @ngay, @gio, @ca, @isLate)
                    ", con);

                    cmd.Parameters.AddWithValue("@id", nsID);
                    cmd.Parameters.AddWithValue("@ngay", datePart);
                    cmd.Parameters.AddWithValue("@gio", timePart);
                    cmd.Parameters.AddWithValue("@ca", ca);
                    cmd.Parameters.AddWithValue("@isLate", diTre ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }

                // 8. Thông báo
                string thongBao = diTre
                    ? $"Bạn đã check-in (CA {ca}) - BẠN ĐI TRỄ!"
                    : $"Bạn đã check-in (CA {ca}) - ĐÚNG GIỜ.";
                MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string XacDinhCaLam(DateTime dt)
        {
            int hour = dt.Hour;
            if (hour >= 6 && hour < 14)
                return "Sáng";
            else if (hour >= 14 && hour < 22)
                return "Chiều";
            else
                return "Tối";
        }

        private bool KiemTraDiTre(DateTime dt, string caLam)
        {
            TimeSpan gioBatDau;
            DateTime ngaySoSanh = dt.Date; // Ngày so sánh ban đầu

            switch (caLam)
            {
                case "Sáng":
                    gioBatDau = new TimeSpan(6, 0, 0);
                    break;
                case "Chiều":
                    gioBatDau = new TimeSpan(14, 0, 0);
                    break;
                case "Tối":
                    gioBatDau = new TimeSpan(22, 0, 0);
                    // Nếu giờ hiện tại < 06:00 sáng, nghĩa là thuộc ca tối của ngày hôm trước
                    if (dt.TimeOfDay < new TimeSpan(6, 0, 0))
                    {
                        // Trừ 1 ngày để so sánh với ca tối của ngày hôm trước
                        ngaySoSanh = ngaySoSanh.AddDays(-1);
                    }
                    break;
                default:
                    gioBatDau = new TimeSpan(22, 0, 0);
                    break;
            }

            // Tạo DateTime kết hợp ngày so sánh và giờ bắt đầu
            DateTime diemBatDauCa = ngaySoSanh.Add(gioBatDau);
            // Cộng thêm 15 phút để xác định thời hạn trễ
            DateTime diemGiaHan = diemBatDauCa.AddMinutes(15);

            // So sánh thời điểm check-in với thời hạn trễ
            return dt > diemGiaHan;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnXemThongTin_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
    using (SqlConnection con = new SqlConnection(connStr))
    {
        con.Open();
        string query = "SELECT HoTen, Email, NgaySinh, DiaChi, Phone FROM NhanSu WHERE ID = @id";
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@id", NhanSuID);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    // Lấy thông tin từ database
                    string hoTen = dr["HoTen"].ToString();
                    string email = dr["Email"].ToString();
                    string ngaySinh = Convert.ToDateTime(dr["NgaySinh"]).ToShortDateString();
                    string diaChi = dr["DiaChi"].ToString();
                    string phone = dr["Phone"].ToString();

                    // Hiển thị thông tin
                    MessageBox.Show($"Thông tin cá nhân:\n\n"
                        + $"ID: {NhanSuID}\n"
                        + $"Tên: {hoTen}\n"
                        + $"Email: {email}\n"
                        + $"Ngày Sinh: {ngaySinh}\n"
                        + $"Địa Chỉ: {diaChi}\n"
                        + $"Số Điện Thoại: {phone}",
                        "Thông tin cá nhân", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin cá nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
        }
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau doiMatKhauForm = new FormDoiMatKhau();
            doiMatKhauForm.Username = this.Username; // Truyền thông tin username
            doiMatKhauForm.ShowDialog();
        }

        private void LoadUserInfo()
        {
            string connStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string query = "SELECT HoTen, Email , NgaySinh , DiaChi ,Phone FROM Users WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", Username);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) 
                        {
                            HoTen = dr["HoTen"].ToString();
                            Email = dr["Email"].ToString();
                            NgaySinh = dr["NgaySinh "].ToString();
                            DiaChi = dr["DiaChi"].ToString();
                            Phone = dr["Phone"].ToString();


                        }
                    }
                }
            }
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
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
