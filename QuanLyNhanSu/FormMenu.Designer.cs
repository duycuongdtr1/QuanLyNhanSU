namespace QuanLyNhanSu
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;

        // Dọn dẹp tài nguyên
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.lblChaoMung = new System.Windows.Forms.Label();
            this.grbChucNang = new System.Windows.Forms.GroupBox();
            this.btnQLNhanSu = new System.Windows.Forms.Button();
            this.btnQLLuong = new System.Windows.Forms.Button();
            this.btnQLChamCong = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btn_dangxuat = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.grbChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChaoMung
            // 
            this.lblChaoMung.AutoSize = true;
            this.lblChaoMung.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblChaoMung.Location = new System.Drawing.Point(12, 9);
            this.lblChaoMung.Name = "lblChaoMung";
            this.lblChaoMung.Size = new System.Drawing.Size(55, 25);
            this.lblChaoMung.TabIndex = 0;
            this.lblChaoMung.Text = "hello";
            // 
            // grbChucNang
            // 
            this.grbChucNang.Controls.Add(this.pictureBox1);
            this.grbChucNang.Controls.Add(this.btnQLNhanSu);
            this.grbChucNang.Controls.Add(this.btnQLLuong);
            this.grbChucNang.Controls.Add(this.btnQLChamCong);
            this.grbChucNang.Controls.Add(this.btnBaoCao);
            this.grbChucNang.Controls.Add(this.btn_dangxuat);
            this.grbChucNang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grbChucNang.Location = new System.Drawing.Point(16, 41);
            this.grbChucNang.Name = "grbChucNang";
            this.grbChucNang.Size = new System.Drawing.Size(572, 648);
            this.grbChucNang.TabIndex = 1;
            this.grbChucNang.TabStop = false;
            this.grbChucNang.Text = "Chức năng ";
            // 
            // btnQLNhanSu
            // 
            this.btnQLNhanSu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQLNhanSu.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnQLNhanSu.ImageKey = "administrator.png";
            this.btnQLNhanSu.ImageList = this.imageList1;
            this.btnQLNhanSu.Location = new System.Drawing.Point(30, 59);
            this.btnQLNhanSu.Name = "btnQLNhanSu";
            this.btnQLNhanSu.Size = new System.Drawing.Size(300, 85);
            this.btnQLNhanSu.TabIndex = 1;
            this.btnQLNhanSu.Text = "Quản lý Nhân sự";
            this.btnQLNhanSu.UseVisualStyleBackColor = true;
            this.btnQLNhanSu.Click += new System.EventHandler(this.btnQLNhanSu_Click);
            // 
            // btnQLLuong
            // 
            this.btnQLLuong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQLLuong.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQLLuong.ImageKey = "salary.png";
            this.btnQLLuong.ImageList = this.imageList1;
            this.btnQLLuong.Location = new System.Drawing.Point(30, 171);
            this.btnQLLuong.Name = "btnQLLuong";
            this.btnQLLuong.Size = new System.Drawing.Size(300, 85);
            this.btnQLLuong.TabIndex = 2;
            this.btnQLLuong.Text = "Quản lý Lương";
            this.btnQLLuong.UseVisualStyleBackColor = true;
            this.btnQLLuong.Click += new System.EventHandler(this.btnQLLuong_Click);
            // 
            // btnQLChamCong
            // 
            this.btnQLChamCong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQLChamCong.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQLChamCong.ImageKey = "user (1).png";
            this.btnQLChamCong.ImageList = this.imageList1;
            this.btnQLChamCong.Location = new System.Drawing.Point(30, 293);
            this.btnQLChamCong.Name = "btnQLChamCong";
            this.btnQLChamCong.Size = new System.Drawing.Size(300, 85);
            this.btnQLChamCong.TabIndex = 3;
            this.btnQLChamCong.Text = "Quản lý Chấm công";
            this.btnQLChamCong.UseVisualStyleBackColor = true;
            this.btnQLChamCong.Click += new System.EventHandler(this.btnQLChamCong_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBaoCao.Location = new System.Drawing.Point(30, 414);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(300, 85);
            this.btnBaoCao.TabIndex = 4;
            this.btnBaoCao.Text = "Báo cáo";
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btn_dangxuat
            // 
            this.btn_dangxuat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_dangxuat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_dangxuat.ImageKey = "logout.png";
            this.btn_dangxuat.ImageList = this.imageList1;
            this.btn_dangxuat.Location = new System.Drawing.Point(30, 536);
            this.btn_dangxuat.Name = "btn_dangxuat";
            this.btn_dangxuat.Size = new System.Drawing.Size(300, 85);
            this.btn_dangxuat.TabIndex = 5;
            this.btn_dangxuat.Text = "Đăng xuất";
            this.btn_dangxuat.UseVisualStyleBackColor = true;
            this.btn_dangxuat.Click += new System.EventHandler(this.btn_dangxuat_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::QuanLyNhanSu.Properties.Resources.wave;
            this.pictureBox2.Location = new System.Drawing.Point(389, -1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QuanLyNhanSu.Properties.Resources.form;
            this.pictureBox1.Location = new System.Drawing.Point(373, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(203, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "administrator.png");
            this.imageList1.Images.SetKeyName(1, "salary.png");
            this.imageList1.Images.SetKeyName(2, "user (1).png");
            this.imageList1.Images.SetKeyName(3, "logout.png");
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(627, 701);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.grbChucNang);
            this.Controls.Add(this.lblChaoMung);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMenu - Quản Lý Nhân Sự";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.grbChucNang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChaoMung;
        private System.Windows.Forms.GroupBox grbChucNang;
        private System.Windows.Forms.Button btnQLNhanSu;
        private System.Windows.Forms.Button btnQLLuong;
        private System.Windows.Forms.Button btnQLChamCong;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btn_dangxuat;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ImageList imageList1;
    }
}
