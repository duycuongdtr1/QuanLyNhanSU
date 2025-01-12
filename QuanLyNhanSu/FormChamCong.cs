using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FormChamCong : Form
    {
        public FormChamCong()
        {
            InitializeComponent();
        }

        private void FormChamCong_Load(object sender, EventArgs e)
        {
            // Khi mở form, tải danh sách chấm công
            LoadChamCong();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadChamCong();
        }

        private void LoadChamCong()
        {
            string connStr = ConfigurationManager.ConnectionStrings["conStr"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Lỗi: Không đọc được chuỗi kết nối 'conStr' trong App.config", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = @"
                SELECT N.ID, N.HoTen, 
                       COUNT(*) AS SoNgayLam,
                       SUM(CASE WHEN C.IsLate = 1 THEN 1 ELSE 0 END) AS SoNgayDiTre
                FROM ChamCong C
                JOIN NhanSu N ON C.ID = N.ID
                GROUP BY N.ID, N.HoTen
            ";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvChamCong.DataSource = dt;

            // Tùy chỉnh header
            if (dgvChamCong.Columns.Count > 0)
            {
                dgvChamCong.Columns["ID"].HeaderText = "Mã NS";
                dgvChamCong.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvChamCong.Columns["SoNgayLam"].HeaderText = "Số ngày làm";
                dgvChamCong.Columns["SoNgayDiTre"].HeaderText = "Số ngày đi trễ";
            }
        }

        private void btnFilterLate_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["conStr"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Lỗi: Không đọc được chuỗi kết nối 'conStr' trong App.config", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = @"
                SELECT N.ID, N.HoTen, 
                       COUNT(*) AS SoNgayLam,
                       SUM(CASE WHEN C.IsLate = 1 THEN 1 ELSE 0 END) AS SoNgayDiTre
                FROM ChamCong C
                JOIN NhanSu N ON C.ID = N.ID
                GROUP BY N.ID, N.HoTen
                HAVING SUM(CASE WHEN C.IsLate = 1 THEN 1 ELSE 0 END) > 0
            ";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu chấm công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvChamCong.DataSource = dt;

            // Tùy chỉnh header
            if (dgvChamCong.Columns.Count > 0)
            {
                dgvChamCong.Columns["ID"].HeaderText = "Mã NS";
                dgvChamCong.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvChamCong.Columns["SoNgayLam"].HeaderText = "Số ngày làm";
                dgvChamCong.Columns["SoNgayDiTre"].HeaderText = "Số ngày đi trễ";
            }
        }

    }
}
