namespace QuanLyNhaTro
{
    partial class FormKhachThue
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtHoTen = new TextBox();
            grpGioiTinh = new GroupBox();
            rdoNu = new RadioButton();
            rdoNam = new RadioButton();
            dtpNgaySinh = new DateTimePicker();
            txtSDT = new TextBox();
            cboPhong = new ComboBox();
            cboTinh = new ComboBox();
            cboPhuongXa = new ComboBox();
            chkTamTru = new CheckBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnGayLoiThu = new Button();
            txtTimKiem = new TextBox();
            label8 = new Label();
            cboLocPhong = new ComboBox();
            cboLocTinh = new ComboBox();
            btnTim = new Button();
            btnBoLoc = new Button();
            dgvKhachThue = new DataGridView();
            label9 = new Label();
            lblStatus = new Label();
            grpGioiTinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachThue).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 30);
            label1.Name = "label1";
            label1.Size = new Size(150, 20);
            label1.TabIndex = 0;
            label1.Text = "Thông tin khách thuê:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 74);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 1;
            label2.Text = "Họ tên:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 162);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 2;
            label3.Text = "Ngày sinh:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(38, 210);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 3;
            label4.Text = "Số điện thoại";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(38, 299);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 4;
            label5.Text = "Tỉnh/Thành";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(38, 253);
            label6.Name = "label6";
            label6.Size = new Size(51, 20);
            label6.TabIndex = 5;
            label6.Text = "Phòng";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(38, 338);
            label7.Name = "label7";
            label7.Size = new Size(83, 20);
            label7.TabIndex = 6;
            label7.Text = "Phường/Xã";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(248, 67);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(174, 27);
            txtHoTen.TabIndex = 7;
            // 
            // grpGioiTinh
            // 
            grpGioiTinh.Controls.Add(rdoNu);
            grpGioiTinh.Controls.Add(rdoNam);
            grpGioiTinh.Location = new Point(248, 109);
            grpGioiTinh.Name = "grpGioiTinh";
            grpGioiTinh.Size = new Size(174, 42);
            grpGioiTinh.TabIndex = 8;
            grpGioiTinh.TabStop = false;
            // 
            // rdoNu
            // 
            rdoNu.AutoSize = true;
            rdoNu.Location = new Point(118, 16);
            rdoNu.Name = "rdoNu";
            rdoNu.Size = new Size(50, 24);
            rdoNu.TabIndex = 1;
            rdoNu.TabStop = true;
            rdoNu.Text = "Nữ";
            rdoNu.UseVisualStyleBackColor = true;
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(10, 16);
            rdoNam.Name = "rdoNam";
            rdoNam.Size = new Size(62, 24);
            rdoNam.TabIndex = 0;
            rdoNam.TabStop = true;
            rdoNam.Text = "Nam";
            rdoNam.UseVisualStyleBackColor = true;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(248, 162);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(174, 27);
            dtpNgaySinh.TabIndex = 9;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(248, 210);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(174, 27);
            txtSDT.TabIndex = 10;
            // 
            // cboPhong
            // 
            cboPhong.FormattingEnabled = true;
            cboPhong.Location = new Point(248, 253);
            cboPhong.Name = "cboPhong";
            cboPhong.Size = new Size(174, 28);
            cboPhong.TabIndex = 11;
            // 
            // cboTinh
            // 
            cboTinh.FormattingEnabled = true;
            cboTinh.Location = new Point(248, 299);
            cboTinh.Name = "cboTinh";
            cboTinh.Size = new Size(174, 28);
            cboTinh.TabIndex = 12;
            cboTinh.SelectedIndexChanged += CboTinh_SelectedIndexChanged;
            // 
            // cboPhuongXa
            // 
            cboPhuongXa.FormattingEnabled = true;
            cboPhuongXa.Location = new Point(248, 338);
            cboPhuongXa.Name = "cboPhuongXa";
            cboPhuongXa.Size = new Size(174, 28);
            cboPhuongXa.TabIndex = 13;
            // 
            // chkTamTru
            // 
            chkTamTru.AutoSize = true;
            chkTamTru.Location = new Point(38, 389);
            chkTamTru.Name = "chkTamTru";
            chkTamTru.Size = new Size(155, 24);
            chkTamTru.TabIndex = 14;
            chkTamTru.Text = "Đã đăng kí tạm trú";
            chkTamTru.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(37, 440);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 15;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(137, 440);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 16;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(237, 440);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 17;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(337, 440);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 18;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnGayLoiThu
            // 
            btnGayLoiThu.Location = new Point(248, 389);
            btnGayLoiThu.Name = "btnGayLoiThu";
            btnGayLoiThu.Size = new Size(94, 29);
            btnGayLoiThu.TabIndex = 19;
            btnGayLoiThu.Text = "Gây lỗi";
            btnGayLoiThu.UseVisualStyleBackColor = true;
            btnGayLoiThu.Visible = false;
            btnGayLoiThu.Click += btnGayLoiThu_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(501, 30);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(192, 27);
            txtTimKiem.TabIndex = 20;
            txtTimKiem.PlaceholderText = "Tìm theo tên hoặc SĐT";
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(699, 37);
            label8.Name = "label8";
            label8.Size = new Size(55, 20);
            label8.TabIndex = 21;
            label8.Text = "phòng:";
            // 
            // cboLocPhong
            // 
            cboLocPhong.FormattingEnabled = true;
            cboLocPhong.Location = new Point(760, 34);
            cboLocPhong.Name = "cboLocPhong";
            cboLocPhong.Size = new Size(151, 28);
            cboLocPhong.TabIndex = 22;
            cboLocPhong.SelectedIndexChanged += cboLocPhong_SelectedIndexChanged;
            // 
            // cboLocTinh
            // 
            cboLocTinh.FormattingEnabled = true;
            cboLocTinh.Location = new Point(917, 34);
            cboLocTinh.Name = "cboLocTinh";
            cboLocTinh.Size = new Size(151, 28);
            cboLocTinh.TabIndex = 23;
            cboLocTinh.SelectedIndexChanged += cboLocTinh_SelectedIndexChanged;
            // 
            // btnTim
            // 
            btnTim.Location = new Point(760, 70);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(94, 29);
            btnTim.TabIndex = 24;
            btnTim.Text = "Tìm kiếm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            // 
            // btnBoLoc
            // 
            btnBoLoc.Location = new Point(874, 70);
            btnBoLoc.Name = "btnBoLoc";
            btnBoLoc.Size = new Size(94, 29);
            btnBoLoc.TabIndex = 25;
            btnBoLoc.Text = "Bỏ lọc";
            btnBoLoc.UseVisualStyleBackColor = true;
            btnBoLoc.Click += btnBoLoc_Click;
            // 
            // dgvKhachThue
            // 
            dgvKhachThue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachThue.Location = new Point(481, 109);
            dgvKhachThue.Name = "dgvKhachThue";
            dgvKhachThue.RowHeadersWidth = 51;
            dgvKhachThue.Size = new Size(587, 360);
            dgvKhachThue.TabIndex = 26;
            dgvKhachThue.SelectionChanged += dgvKhachThue_SelectionChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(38, 504);
            label9.Name = "label9";
            label9.Size = new Size(0, 20);
            label9.TabIndex = 27;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(38, 516);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 28;
            // 
            // FormKhachThue
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 638);
            Controls.Add(lblStatus);
            Controls.Add(label9);
            Controls.Add(dgvKhachThue);
            Controls.Add(btnBoLoc);
            Controls.Add(btnTim);
            Controls.Add(cboLocTinh);
            Controls.Add(cboLocPhong);
            Controls.Add(label8);
            Controls.Add(txtTimKiem);
            Controls.Add(btnGayLoiThu);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(chkTamTru);
            Controls.Add(cboPhuongXa);
            Controls.Add(cboTinh);
            Controls.Add(cboPhong);
            Controls.Add(txtSDT);
            Controls.Add(dtpNgaySinh);
            Controls.Add(grpGioiTinh);
            Controls.Add(txtHoTen);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormKhachThue";
            Text = "Form1";
            Load += FormKhachThue_Load;
            KeyDown += FormKhachThue_KeyDown;
            grpGioiTinh.ResumeLayout(false);
            grpGioiTinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachThue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtHoTen;
        private GroupBox grpGioiTinh;
        private RadioButton rdoNam;
        private RadioButton rdoNu;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtSDT;
        private ComboBox cboPhong;
        private ComboBox cboTinh;
        private ComboBox cboPhuongXa;
        private CheckBox chkTamTru;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnGayLoiThu;
        private TextBox txtTimKiem;
        private Label label8;
        private ComboBox cboLocPhong;
        private ComboBox cboLocTinh;
        private Button btnTim;
        private Button btnBoLoc;
        private DataGridView dgvKhachThue;
        private Label label9;
        private Label lblStatus;
    }
}
