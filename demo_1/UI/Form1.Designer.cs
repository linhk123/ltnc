using demo_1.DAL.Entity;

namespace demo_1
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            tabPage6 = new TabPage();
            panel6 = new Panel();
            dgvSachSapHet = new DataGridView();
            dgvTopSach = new DataGridView();
            panel5 = new Panel();
            lblTongSachBan = new Label();
            lblTongSoHD = new Label();
            lblTongDoanhThu = new Label();
            panel4 = new Panel();
            btnThốngKê = new Button();
            dtpNgayThongKe = new DateTimePicker();
            tabPage5 = new TabPage();
            dgvLichSuHoaDon = new DataGridView();
            tabPage3 = new TabPage();
            gio_hang = new DataGridView();
            panel3 = new Panel();
            btn_ = new Button();
            tong = new Label();
            So_lương = new NumericUpDown();
            label16 = new Label();
            label14 = new Label();
            txtSach = new ComboBox();
            btnHD_Xoa = new Button();
            BtnHD_Sua = new Button();
            btnHD_Them = new Button();
            dateTimePicker1 = new DateTimePicker();
            label7 = new Label();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            btnType_Them = new Button();
            btnType_Sua = new Button();
            btnType_Xoa = new Button();
            textBox3 = new TextBox();
            label6 = new Label();
            LoaddataType = new DataGridView();
            tabPage1 = new TabPage();
            btnTimKiem = new Button();
            txtTimKiem = new TextBox();
            panel1 = new Panel();
            cboLoaiSach = new ComboBox();
            txtGiaBan = new NumericUpDown();
            btnXoa = new Button();
            nudSoLuong = new NumericUpDown();
            txtTenSach = new TextBox();
            txtTacGia = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnSua = new Button();
            btnThem = new Button();
            LoaddataSach = new DataGridView();
            tabControl1 = new TabControl();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tabPage6.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSachSapHet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopSach).BeginInit();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichSuHoaDon).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gio_hang).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)So_lương).BeginInit();
            tabPage2.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoaddataType).BeginInit();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoaddataSach).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // tabPage6
            // 
            tabPage6.AccessibleName = "tab6";
            tabPage6.Controls.Add(panel6);
            tabPage6.Controls.Add(panel5);
            tabPage6.Controls.Add(panel4);
            tabPage6.Location = new Point(4, 29);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(1060, 535);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Thống kê";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(dgvSachSapHet);
            panel6.Controls.Add(dgvTopSach);
            panel6.Location = new Point(31, 281);
            panel6.Name = "panel6";
            panel6.Size = new Size(995, 223);
            panel6.TabIndex = 3;
            // 
            // dgvSachSapHet
            // 
            dgvSachSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSachSapHet.Location = new Point(550, 22);
            dgvSachSapHet.Name = "dgvSachSapHet";
            dgvSachSapHet.RowHeadersWidth = 51;
            dgvSachSapHet.Size = new Size(407, 174);
            dgvSachSapHet.TabIndex = 1;
            dgvSachSapHet.CellContentClick += dgvSachSapHet_CellContentClick;
            // 
            // dgvTopSach
            // 
            dgvTopSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTopSach.Location = new Point(38, 22);
            dgvTopSach.Name = "dgvTopSach";
            dgvTopSach.RowHeadersWidth = 51;
            dgvTopSach.Size = new Size(407, 174);
            dgvTopSach.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(lblTongSachBan);
            panel5.Controls.Add(lblTongSoHD);
            panel5.Controls.Add(lblTongDoanhThu);
            panel5.Location = new Point(31, 138);
            panel5.Name = "panel5";
            panel5.Size = new Size(995, 114);
            panel5.TabIndex = 1;
            // 
            // lblTongSachBan
            // 
            lblTongSachBan.AutoSize = true;
            lblTongSachBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongSachBan.Location = new Point(27, 84);
            lblTongSachBan.Name = "lblTongSachBan";
            lblTongSachBan.Size = new Size(155, 20);
            lblTongSachBan.TabIndex = 2;
            lblTongSachBan.Text = "Tổng số sách đã bán:";
            // 
            // lblTongSoHD
            // 
            lblTongSoHD.AutoSize = true;
            lblTongSoHD.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongSoHD.Location = new Point(27, 48);
            lblTongSoHD.Name = "lblTongSoHD";
            lblTongSoHD.Size = new Size(130, 20);
            lblTongSoHD.TabIndex = 1;
            lblTongSoHD.Text = "Tổng số hóa đơn:";
            // 
            // lblTongDoanhThu
            // 
            lblTongDoanhThu.AutoSize = true;
            lblTongDoanhThu.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongDoanhThu.Location = new Point(27, 14);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(125, 20);
            lblTongDoanhThu.TabIndex = 0;
            lblTongDoanhThu.Text = "Tổng doanh thu:";
            // 
            // panel4
            // 
            panel4.Controls.Add(btnThốngKê);
            panel4.Controls.Add(dtpNgayThongKe);
            panel4.Location = new Point(31, 33);
            panel4.Name = "panel4";
            panel4.Size = new Size(995, 80);
            panel4.TabIndex = 0;
            panel4.Paint += panel4_Paint;
            // 
            // btnThốngKê
            // 
            btnThốngKê.BackColor = Color.LightGray;
            btnThốngKê.Location = new Point(809, 24);
            btnThốngKê.Name = "btnThốngKê";
            btnThốngKê.Size = new Size(148, 29);
            btnThốngKê.TabIndex = 1;
            btnThốngKê.Text = "Thống Kê";
            btnThốngKê.UseVisualStyleBackColor = false;
            btnThốngKê.Click += btnThốngKê_Click;
            // 
            // dtpNgayThongKe
            // 
            dtpNgayThongKe.Location = new Point(27, 26);
            dtpNgayThongKe.Name = "dtpNgayThongKe";
            dtpNgayThongKe.Size = new Size(334, 27);
            dtpNgayThongKe.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.AccessibleName = "tab5";
            tabPage5.Controls.Add(dgvLichSuHoaDon);
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1060, 535);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Lịch sử hóa đơn";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvLichSuHoaDon
            // 
            dgvLichSuHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichSuHoaDon.Location = new Point(53, 47);
            dgvLichSuHoaDon.Name = "dgvLichSuHoaDon";
            dgvLichSuHoaDon.RowHeadersWidth = 51;
            dgvLichSuHoaDon.Size = new Size(950, 432);
            dgvLichSuHoaDon.TabIndex = 0;
            dgvLichSuHoaDon.CellContentClick += dgvPhieuNhap_CellContentClick;
            // 
            // tabPage3
            // 
            tabPage3.AccessibleName = "tab3";
            tabPage3.Controls.Add(gio_hang);
            tabPage3.Controls.Add(panel3);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1060, 535);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Hóa đơn";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // gio_hang
            // 
            gio_hang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gio_hang.Location = new Point(24, 26);
            gio_hang.Name = "gio_hang";
            gio_hang.RowHeadersWidth = 51;
            gio_hang.Size = new Size(1005, 261);
            gio_hang.TabIndex = 1;
            gio_hang.CellContentClick += gio_hang_CellContentClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(btn_);
            panel3.Controls.Add(tong);
            panel3.Controls.Add(So_lương);
            panel3.Controls.Add(label16);
            panel3.Controls.Add(label14);
            panel3.Controls.Add(txtSach);
            panel3.Controls.Add(btnHD_Xoa);
            panel3.Controls.Add(BtnHD_Sua);
            panel3.Controls.Add(btnHD_Them);
            panel3.Controls.Add(dateTimePicker1);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(24, 312);
            panel3.Name = "panel3";
            panel3.Size = new Size(1005, 190);
            panel3.TabIndex = 0;
            // 
            // btn_
            // 
            btn_.BackColor = Color.LightGray;
            btn_.Location = new Point(567, 119);
            btn_.Name = "btn_";
            btn_.Size = new Size(94, 45);
            btn_.TabIndex = 17;
            btn_.Text = "Lưu";
            btn_.UseVisualStyleBackColor = false;
            btn_.Click += btn__Click;
            // 
            // tong
            // 
            tong.AutoSize = true;
            tong.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tong.Location = new Point(684, 119);
            tong.Name = "tong";
            tong.Size = new Size(81, 31);
            tong.TabIndex = 16;
            tong.Text = "Tổng: ";
            tong.Click += label17_Click;
            // 
            // So_lương
            // 
            So_lương.Location = new Point(828, 41);
            So_lương.Name = "So_lương";
            So_lương.Size = new Size(71, 27);
            So_lương.TabIndex = 14;
            So_lương.ValueChanged += So_lương_ValueChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(484, 48);
            label16.Name = "label16";
            label16.Size = new Size(65, 20);
            label16.TabIndex = 13;
            label16.Text = "Tên sách";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(750, 48);
            label14.Name = "label14";
            label14.Size = new Size(72, 20);
            label14.TabIndex = 11;
            label14.Text = "Số Lượng";
            // 
            // txtSach
            // 
            txtSach.FormattingEnabled = true;
            txtSach.Location = new Point(555, 42);
            txtSach.Name = "txtSach";
            txtSach.Size = new Size(151, 28);
            txtSach.TabIndex = 2;
            txtSach.SelectedIndexChanged += txtSach_SelectedIndexChanged;
            // 
            // btnHD_Xoa
            // 
            btnHD_Xoa.BackColor = Color.LightGray;
            btnHD_Xoa.Location = new Point(400, 119);
            btnHD_Xoa.Name = "btnHD_Xoa";
            btnHD_Xoa.Size = new Size(111, 45);
            btnHD_Xoa.TabIndex = 10;
            btnHD_Xoa.Text = "Xóa";
            btnHD_Xoa.UseVisualStyleBackColor = false;
            btnHD_Xoa.Click += btnHD_Xoa_Click;
            // 
            // BtnHD_Sua
            // 
            BtnHD_Sua.BackColor = Color.LightGray;
            BtnHD_Sua.Location = new Point(205, 119);
            BtnHD_Sua.Name = "BtnHD_Sua";
            BtnHD_Sua.Size = new Size(111, 45);
            BtnHD_Sua.TabIndex = 9;
            BtnHD_Sua.Text = "Sửa";
            BtnHD_Sua.UseVisualStyleBackColor = false;
            BtnHD_Sua.Click += BtnHD_Sua_Click;
            // 
            // btnHD_Them
            // 
            btnHD_Them.BackColor = Color.LightGray;
            btnHD_Them.Location = new Point(31, 119);
            btnHD_Them.Name = "btnHD_Them";
            btnHD_Them.Size = new Size(111, 45);
            btnHD_Them.TabIndex = 8;
            btnHD_Them.Text = "Thêm ";
            btnHD_Them.UseVisualStyleBackColor = false;
            btnHD_Them.Click += btnHD_Them_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(165, 43);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(290, 27);
            dateTimePicker1.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(31, 48);
            label7.Name = "label7";
            label7.Size = new Size(128, 20);
            label7.TabIndex = 2;
            label7.Text = "Ngày lập hóa đơn";
            // 
            // tabPage2
            // 
            tabPage2.AccessibleName = "tab2";
            tabPage2.Controls.Add(panel2);
            tabPage2.Controls.Add(LoaddataType);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1060, 535);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Loại sách";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnType_Them);
            panel2.Controls.Add(btnType_Sua);
            panel2.Controls.Add(btnType_Xoa);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(32, 392);
            panel2.Name = "panel2";
            panel2.Size = new Size(976, 113);
            panel2.TabIndex = 1;
            // 
            // btnType_Them
            // 
            btnType_Them.BackColor = Color.LightGray;
            btnType_Them.Location = new Point(18, 53);
            btnType_Them.Name = "btnType_Them";
            btnType_Them.Size = new Size(106, 42);
            btnType_Them.TabIndex = 5;
            btnType_Them.Text = "Thêm";
            btnType_Them.UseVisualStyleBackColor = false;
            btnType_Them.Click += btnType_Them_Click_1;
            // 
            // btnType_Sua
            // 
            btnType_Sua.BackColor = Color.LightGray;
            btnType_Sua.Location = new Point(186, 53);
            btnType_Sua.Name = "btnType_Sua";
            btnType_Sua.Size = new Size(106, 42);
            btnType_Sua.TabIndex = 4;
            btnType_Sua.Text = "Sửa";
            btnType_Sua.UseVisualStyleBackColor = false;
            btnType_Sua.Click += btnType_Sua_Click_1;
            // 
            // btnType_Xoa
            // 
            btnType_Xoa.BackColor = Color.LightGray;
            btnType_Xoa.Location = new Point(357, 53);
            btnType_Xoa.Name = "btnType_Xoa";
            btnType_Xoa.Size = new Size(106, 42);
            btnType_Xoa.TabIndex = 3;
            btnType_Xoa.Text = "Xóa";
            btnType_Xoa.UseVisualStyleBackColor = false;
            btnType_Xoa.Click += btnType_Xoa_Click_1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(118, 12);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(357, 27);
            textBox3.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 15);
            label6.Name = "label6";
            label6.Size = new Size(94, 20);
            label6.TabIndex = 1;
            label6.Text = "Tên loại sách";
            // 
            // LoaddataType
            // 
            LoaddataType.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LoaddataType.Location = new Point(32, 26);
            LoaddataType.Name = "LoaddataType";
            LoaddataType.RowHeadersWidth = 51;
            LoaddataType.Size = new Size(976, 329);
            LoaddataType.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AccessibleName = "tab1";
            tabPage1.Controls.Add(btnTimKiem);
            tabPage1.Controls.Add(txtTimKiem);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(LoaddataSach);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1060, 535);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Sách";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.LightGray;
            btnTimKiem.Location = new Point(881, 28);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(127, 29);
            btnTimKiem.TabIndex = 3;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(34, 28);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(812, 27);
            txtTimKiem.TabIndex = 2;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(cboLoaiSach);
            panel1.Controls.Add(txtGiaBan);
            panel1.Controls.Add(btnXoa);
            panel1.Controls.Add(nudSoLuong);
            panel1.Controls.Add(txtTenSach);
            panel1.Controls.Add(txtTacGia);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnSua);
            panel1.Controls.Add(btnThem);
            panel1.Location = new Point(34, 323);
            panel1.Name = "panel1";
            panel1.Size = new Size(986, 180);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // cboLoaiSach
            // 
            cboLoaiSach.FormattingEnabled = true;
            cboLoaiSach.Location = new Point(93, 67);
            cboLoaiSach.Name = "cboLoaiSach";
            cboLoaiSach.Size = new Size(288, 28);
            cboLoaiSach.TabIndex = 10;
            // 
            // txtGiaBan
            // 
            txtGiaBan.Location = new Point(624, 65);
            txtGiaBan.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            txtGiaBan.Name = "txtGiaBan";
            txtGiaBan.Size = new Size(150, 27);
            txtGiaBan.TabIndex = 9;
            txtGiaBan.ValueChanged += txtGiaBan_ValueChanged;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.LightGray;
            btnXoa.Location = new Point(378, 118);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(115, 39);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Location = new Point(480, 65);
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(59, 27);
            nudSoLuong.TabIndex = 8;
            nudSoLuong.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // txtTenSach
            // 
            txtTenSach.Location = new Point(93, 17);
            txtTenSach.Name = "txtTenSach";
            txtTenSach.Size = new Size(288, 27);
            txtTenSach.TabIndex = 7;
            // 
            // txtTacGia
            // 
            txtTacGia.Location = new Point(462, 17);
            txtTacGia.Name = "txtTacGia";
            txtTacGia.Size = new Size(312, 27);
            txtTacGia.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(561, 70);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 5;
            label5.Text = "Giá bán";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(401, 70);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 4;
            label4.Text = "Số lượng ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(401, 19);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 3;
            label3.Text = "Tác giả";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 67);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 2;
            label2.Text = "Loại sách";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 19);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 1;
            label1.Text = "Tên sách";
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.LightGray;
            btnSua.Location = new Point(200, 118);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(112, 39);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.LightGray;
            btnThem.Location = new Point(22, 118);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(115, 39);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm ";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // LoaddataSach
            // 
            LoaddataSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LoaddataSach.Location = new Point(34, 81);
            LoaddataSach.Name = "LoaddataSach";
            LoaddataSach.RowHeadersWidth = 51;
            LoaddataSach.Size = new Size(986, 215);
            LoaddataSach.TabIndex = 1;
            LoaddataSach.CellContentClick += LoaddataSach_CellContentClick;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Location = new Point(80, 71);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1068, 568);
            tabControl1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1176, 672);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabPage6.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSachSapHet).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopSach).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLichSuHoaDon).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gio_hang).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)So_lương).EndInit();
            tabPage2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LoaddataType).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)LoaddataSach).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TabPage tabPage6;
        private TabPage tabPage5;
        private DataGridView dgvLichSuHoaDon;
        private TabPage tabPage3;
        private DataGridView gio_hang;
        private Panel panel3;
        private Button btn_;
        private Label tong;
        private NumericUpDown So_lương;
        private Label label16;
        private Label label14;
        private ComboBox txtSach;
        private Button btnHD_Xoa;
        private Button BtnHD_Sua;
        private Button btnHD_Them;
        private DateTimePicker dateTimePicker1;
        private Label label7;
        private TabPage tabPage2;
        private Panel panel2;
        private Button btnType_Them;
        private Button btnType_Sua;
        private Button btnType_Xoa;
        private TextBox textBox3;
        private Label label6;
        private DataGridView LoaddataType;
        private TabPage tabPage1;
        private Panel panel1;
        private ComboBox cboLoaiSach;
        private NumericUpDown txtGiaBan;
        private Button btnXoa;
        private NumericUpDown nudSoLuong;
        private TextBox txtTenSach;
        private TextBox txtTacGia;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnSua;
        private Button btnThem;
        private DataGridView LoaddataSach;
        private TabControl tabControl1;
        private Panel panel4;
        private Panel panel6;
        private Panel panel5;
        private Label lblTongSachBan;
        private Label lblTongSoHD;
        private Label lblTongDoanhThu;
        private Button btnThốngKê;
        private DateTimePicker dtpNgayThongKe;
        private DataGridView dgvSachSapHet;
        private DataGridView dgvTopSach;
        private Button btnTimKiem;
        private TextBox txtTimKiem;
        private ContextMenuStrip contextMenuStrip1;
    }
}
