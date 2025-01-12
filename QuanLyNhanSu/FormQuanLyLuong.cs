using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FormQuanLyLuong : Form
    {
        public FormQuanLyLuong()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadLuong();
        }

        private void LoadLuong()
        {
            string connStr = ConfigurationManager.ConnectionStrings["conStr"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Lỗi: Không đọc được chuỗi kết nối 'conStr' trong App.config",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Thêm cột “Luong” tính toán
                dt.Columns.Add("Luong", typeof(decimal));

                // Duyệt từng hàng để tính lương
                foreach (DataRow row in dt.Rows)
                {
                    int soNgayLam = row["SoNgayLam"] != DBNull.Value ? Convert.ToInt32(row["SoNgayLam"]) : 0;
                    int soNgayDiTre = row["SoNgayDiTre"] != DBNull.Value ? Convert.ToInt32(row["SoNgayDiTre"]) : 0;

                    // Tính lương dựa trên công thức
                    decimal luongCoBan = soNgayLam * 8 * 30000m;
                    decimal phatDiTre = soNgayDiTre * 10000m;
                    decimal tongLuong = luongCoBan - phatDiTre;

                    row["Luong"] = tongLuong;
                }

                dgvLuong.DataSource = dt;

                // Tùy chỉnh header
                if (dgvLuong.Columns.Count > 0)
                {
                    dgvLuong.Columns["ID"].HeaderText = "Mã NS";
                    dgvLuong.Columns["HoTen"].HeaderText = "Họ Tên";
                    dgvLuong.Columns["SoNgayLam"].HeaderText = "Số ngày làm";
                    dgvLuong.Columns["SoNgayDiTre"].HeaderText = "Số ngày đi trễ";
                    dgvLuong.Columns["Luong"].HeaderText = "Lương (VNĐ)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lương: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportLuongToExcel();
        }

        private void ExportLuongToExcel()
        {
            

            if (dgvLuong.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file (*.csv)|*.csv";
            sfd.FileName = "BangLuong.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Ghi dữ liệu ra file CSV
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // Ghi dòng header
                        for (int i = 0; i < dgvLuong.Columns.Count; i++)
                        {
                            sw.Write(dgvLuong.Columns[i].HeaderText);
                            if (i < dgvLuong.Columns.Count - 1) sw.Write(",");
                        }
                        sw.WriteLine();

                        // Ghi từng dòng
                        foreach (DataGridViewRow row in dgvLuong.Rows)
                        {
                            for (int i = 0; i < dgvLuong.Columns.Count; i++)
                            {
                                sw.Write(row.Cells[i].Value?.ToString());
                                if (i < dgvLuong.Columns.Count - 1) sw.Write(",");
                            }
                            sw.WriteLine();
                        }
                    }

                    MessageBox.Show("Xuất CSV thành công! Bạn có thể mở bằng Excel.", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message);
                }
            }
        }
    }
}
