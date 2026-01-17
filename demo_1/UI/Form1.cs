using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using demo_1.BLL.Implements;
using demo_1.DAL.Contexts;
using System.ComponentModel;

namespace demo_1
{
    public partial class Form1 : Form
    {
        private NguoiDung _currentUser;
        private readonly ISachRepository _sachService;
        private readonly BindingSource _bs = new BindingSource();
        private string _selectedMaSach;
        private readonly HoaDonService _hoaDonService;

        private string _selectedMaLoaiSach;
        private readonly BindingSource _bsLoai = new BindingSource();

        private List<ChiTietHoaDonDTO> _giỏHàng = new List<ChiTietHoaDonDTO>();
        private BindingSource _bsGiỏHàng = new BindingSource();

        private readonly thongKeService _tkService = new thongKeService();
        public Form1(NguoiDung user)
        {
            InitializeComponent();

            // Khởi tạo service
            _sachService = new SachService();

            // Setup binding cho sách
            LoaddataSach.DataSource = _bs;
            LoaddataSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoaddataSach.MultiSelect = false;
            LoaddataSach.SelectionChanged += LoaddataSach_SelectionChanged;

            // Setup binding cho loại sách
            LoaddataType.DataSource = _bsLoai;
            LoaddataType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoaddataType.MultiSelect = false;
            LoaddataType.SelectionChanged += LoaddataType_SelectionChanged;
            _hoaDonService = new HoaDonService();
            _currentUser = user;
            ApplyPhanQuyen();

            LoadDataSequentially();


        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadLichSuHoaDon();
        }
        private async void LoadDataSequentially()
        {
            try
            {
                await LoadSachAsync();
                await LoadLoaiSachAsync();
                await FillComboSachHD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }


        private void ApplyPhanQuyen()
        {
            this.Text = $"Nhà Sách - {_currentUser.HoTen} ({_currentUser.VaiTro})";

            switch (_currentUser.VaiTro)
            {
                case "ChuQuan":
                    // Chủ quán vào thẳng Tab Thống kê (Giả sử là Tab cuối cùng)
                    tabControl1.SelectedTab = tabPage5;
                    MessageBox.Show("Chào sếp! Hệ thống báo cáo đã sẵn sàng.");
                    break;

                case "Admin":
                    // Admin vào Tab Quản lý hệ thống/Sách
                    tabControl1.SelectedIndex = 0;
                    break;

                case "NhanVien":
                    if (tabControl1.TabPages.Contains(tabPage1)) tabControl1.TabPages.Remove(tabPage1);
                    if (tabControl1.TabPages.Contains(tabPage2)) tabControl1.TabPages.Remove(tabPage2);

                    if (tabControl1.TabPages.Contains(tabPage5)) tabControl1.TabPages.Remove(tabPage5);
                    if (tabControl1.TabPages.Contains(tabPage6)) tabControl1.TabPages.Remove(tabPage6);

                    tabControl1.SelectedTab = tabPage3;
                    break;
            }
        }


        //napj dữ liệu vào datagridview
        private async Task LoadSachAsync()
        {
            try
            {
                var list = await _sachService.GetSachesAsync();

                _bs.DataSource = list;
                LoaddataSach.AutoGenerateColumns = true;

                // configure column headers and visibility
                if (LoaddataSach.Columns.Contains("MaSach"))
                    LoaddataSach.Columns["MaSach"].Visible = false;

                if (LoaddataSach.Columns.Contains("TenSach"))
                    LoaddataSach.Columns["TenSach"].HeaderText = "Tên sách";
                if (LoaddataSach.Columns.Contains("TacGia"))
                    LoaddataSach.Columns["TacGia"].HeaderText = "Tác giả";
                if (LoaddataSach.Columns.Contains("SoLuong"))
                    LoaddataSach.Columns["SoLuong"].HeaderText = "Số lượng";
                if (LoaddataSach.Columns.Contains("GiaBan"))
                    LoaddataSach.Columns["GiaBan"].HeaderText = "Giá bán";
                if (LoaddataSach.Columns.Contains("TenLoaiSach"))
                    LoaddataSach.Columns["TenLoaiSach"].HeaderText = "Loại sách";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sách: " + ex.Message);
            }
        }

        /// <summary>
        /// Load categories for combo and category grid
        /// </summary>
        private async Task LoadLoaiSachAsync()
        {
            using var db = new NhaSachContext();
            // Lấy nguyên danh sách Object LoaiSach từ DB
            var types = await Task.Run(() => db.LoaiSachs.OrderBy(x => x.TenLoaiSach).ToList());

            cboLoaiSach.DataSource = types;
            cboLoaiSach.DisplayMember = "TenLoaiSach";
            cboLoaiSach.ValueMember = "MaLoaiSach";

            // Gán trực tiếp list object cho BindingSource
            _bsLoai.DataSource = types;
            LoaddataType.DataSource = _bsLoai;
        }

        private void LoaddataSach_SelectionChanged(object sender, EventArgs e)
        {
            if (LoaddataSach.CurrentRow?.DataBoundItem is SachDTO dto)
            {
                // 1. Gán ID sách đang chọn vào biến toàn cục
                _selectedMaSach = dto.MaSach;

                // 2. Đổ dữ liệu lên các ô nhập liệu
                txtTenSach.Text = dto.TenSach;
                txtTacGia.Text = dto.TacGia;
                nudSoLuong.Value = dto.SoLuong;
                txtGiaBan.Value = dto.GiaBan;
                cboLoaiSach.SelectedValue = dto.MaLoaiSach;

                // 3. Bật các nút chức năng
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void LoaddataType_SelectionChanged(object sender, EventArgs e)
        {
            if (LoaddataType.CurrentRow?.DataBoundItem is LoaiSach ls)
            {
                _selectedMaLoaiSach = ls.MaLoaiSach;
                textBox3.Text = ls.TenLoaiSach;
            }
        }

        private SachDTO ReadDtoFromInputs()
        {
            return new SachDTO
            {
                MaSach = _selectedMaSach ?? Guid.NewGuid().ToString(),
                TenSach = txtTenSach.Text.Trim(),
                TacGia = txtTacGia.Text.Trim(),
                SoLuong = (int)nudSoLuong.Value,
                GiaBan = txtGiaBan.Value,
                TenLoaiSach = cboLoaiSach.Text, // display member
                MaLoaiSach = cboLoaiSach.SelectedValue as string
            };
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = ReadDtoFromInputs();
                // ensure new id
                dto.MaSach = Guid.NewGuid().ToString();

                // validate MaLoaiSach
                if (string.IsNullOrEmpty(dto.MaLoaiSach))
                {
                    MessageBox.Show("Vui lòng chọn loại sách trước khi thêm.");
                    return;
                }

                var ok = await _sachService.Add(dto);
                if (ok)
                {
                    await LoadSachAsync();
                    MessageBox.Show("Thêm thành công");
                }
                else MessageBox.Show("Thêm thất bại");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaSach))
            {
                MessageBox.Show("Chọn sách cần sửa");
                return;
            }

            try
            {
                var dto = ReadDtoFromInputs();

                if (string.IsNullOrEmpty(dto.MaLoaiSach))
                {
                    MessageBox.Show("Vui lòng chọn loại sách trước khi cập nhật.");
                    return;
                }

                var ok = await _sachService.Update(dto);
                if (ok)
                {
                    await LoadSachAsync();
                    MessageBox.Show("Cập nhật thành công");
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaSach))
            {
                MessageBox.Show("Chọn sách cần xóa");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                var ok = await _sachService.Delete(_selectedMaSach);
                if (ok)
                {
                    await LoadSachAsync();
                    MessageBox.Show("Xóa thành công");
                }
                else MessageBox.Show("Xóa thất bại");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        // CRUD for LoaiSach (Category) tab
        private async void btnType_Them_Click(object sender, EventArgs e)
        {
            var name = textBox3.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách.");
                return;
            }

            try
            {
                using var db = new NhaSachContext();
                var newItem = new LoaiSach
                {
                    MaLoaiSach = Guid.NewGuid().ToString(),
                    TenLoaiSach = name
                };
                db.LoaiSachs.Add(newItem);
                await db.SaveChangesAsync();

                await LoadLoaiSachAsync();
                MessageBox.Show("Thêm loại sách thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm loại sách: " + ex.Message);
            }
        }

        private async void btnType_Sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLoaiSach))
            {
                MessageBox.Show("Chọn loại sách cần sửa.");
                return;
            }

            var name = textBox3.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách.");
                return;
            }

            try
            {
                using var db = new NhaSachContext();
                var item = await db.LoaiSachs.FindAsync(_selectedMaLoaiSach);
                if (item == null)
                {
                    MessageBox.Show("Không tìm thấy loại sách.");
                    return;
                }
                item.TenLoaiSach = name;
                db.LoaiSachs.Update(item);
                await db.SaveChangesAsync();

                await LoadLoaiSachAsync();
                MessageBox.Show("Cập nhật loại sách thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật loại sách: " + ex.Message);
            }
        }

        private async void btnType_Xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLoaiSach))
            {
                MessageBox.Show("Chọn loại sách cần xóa.");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa loại sách?", "Xóa", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using var db = new NhaSachContext();
                var item = await db.LoaiSachs.FindAsync(_selectedMaLoaiSach);
                if (item == null)
                {
                    MessageBox.Show("Không tìm thấy loại sách.");
                    return;
                }

                // check for related books
                var hasBooks = await Task.Run(() => db.Sachs.Any(s => s.MaLoaiSach == _selectedMaLoaiSach));
                if (hasBooks)
                {
                    MessageBox.Show("Không thể xóa: vẫn còn sách thuộc loại này.");
                    return;
                }

                db.LoaiSachs.Remove(item);
                await db.SaveChangesAsync();

                await LoadLoaiSachAsync();
                MessageBox.Show("Xóa loại sách thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa loại sách: " + ex.Message);
            }
        }

        // optional search method invoked by external control
        private async Task SearchAsync(string keyword)
        {
            try
            {
                List<SachDTO> results;
                if (string.IsNullOrWhiteSpace(keyword))
                    results = await _sachService.GetSachesAsync();
                else
                    results = await _sachService.Search(keyword);

                _bs.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoaddataSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



        public async Task<bool> LuuHoaDonAsync(HoaDon hd, List<ChiTietHoaDonDTO> chiTiets)
        {
            // 1. Khởi tạo Repository (Đảm bảo file HoaDonRepository.cs của bạn đã được lưu)
            var hoaDonRepo = new demo_1.DAL.DAO.HoaDonRepository();

            // 2. Chuyển đổi từ DTO (dùng cho Grid) sang Entity (dùng cho Database)
            var listEntities = chiTiets.Select(x => new ChiTietHoaDon
            {
                ma_hoa_don = hd.ma_hoa_don,
                ma_sach = x.MaSach,
                so_luong = x.SoLuong
                // Nếu có gia_ban_luc_do hãy thêm vào đây
            }).ToList();

            // 3. Gọi hàm đúng tên trong Repository
            return await hoaDonRepo.SaveHoaDonTransaction(hd, listEntities);
        }




        private void NBGia_tien_ValueChanged(object sender, EventArgs e)
        {

        }
        private void UpdateGioHangGrid()
        {
            // Cập nhật nguồn dữ liệu cho DataGridView
            _bsGiỏHàng.DataSource = null;
            _bsGiỏHàng.DataSource = _giỏHàng;
            gio_hang.DataSource = _bsGiỏHàng;

            // TÍNH TOÁN VÀ HIỂN THỊ TỰ ĐỘNG
            // Giả sử ThanhTien đang lưu số rút gọn (ví dụ: 145)
            decimal tongRutGon = _giỏHàng.Sum(x => x.ThanhTien);

            // Gán trực tiếp giá trị đã tính vào Label 'tong'
            // Dùng ToString("N0") để tự động thêm dấu chấm phân cách hàng nghìn
            tong.Text = (tongRutGon * 1000).ToString("N0") + " VNĐ";
        }
        private void btnHD_Them_Click(object sender, EventArgs e)
        {
            var sach = txtSach.SelectedItem as SachDTO;
            if (sach == null || So_lương.Value <= 0) return;

            // Kiểm tra nếu sách đã có trong giỏ hàng thì tăng số lượng
            var itemExist = _giỏHàng.FirstOrDefault(x => x.MaSach == sach.MaSach);
            if (itemExist != null)
            {
                if (itemExist.SoLuong + (int)So_lương.Value > sach.SoLuong)
                {
                    MessageBox.Show("Tổng số lượng vượt quá kho!");
                    return;
                }
                itemExist.SoLuong += (int)So_lương.Value;
            }
            else
            {
                _giỏHàng.Add(new ChiTietHoaDonDTO
                {
                    MaSach = sach.MaSach,
                    TenSach = sach.TenSach,
                    SoLuong = (int)So_lương.Value,
                    DonGia = sach.GiaBan
                });
            }

            UpdateGioHangGrid();
        }

        private async void BtnHD_Sua_Click(object sender, EventArgs e)
        {
            if (gio_hang.CurrentRow?.DataBoundItem is ChiTietHoaDonDTO selectedItem)
            {
                int newQty = (int)So_lương.Value;

                // Kiểm tra tồn kho thực tế của sách này (Lấy từ nguồn dữ liệu sách)
                // Bạn nên bổ sung kiểm tra nếu newQty > số lượng tồn kho

                if (newQty <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0");
                    return;
                }

                selectedItem.SoLuong = newQty;

                // QUAN TRỌNG: Làm mới Binding để Grid hiển thị số mới và ThanhTien mới
                _bsGiỏHàng.ResetBindings(false);

                UpdateGioHangGrid(); // Tính lại tổng tiền Label
                MessageBox.Show("Đã cập nhật số lượng!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng trong giỏ hàng!");
            }
        }

        private async void btnHD_Xoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng có chọn dòng nào trên Grid giỏ hàng không
            if (gio_hang.CurrentRow == null || gio_hang.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng trong giỏ hàng để xóa!");
                return;
            }

            // 2. Lấy đối tượng đang chọn trong giỏ hàng
            var itemCanXoa = gio_hang.CurrentRow.DataBoundItem as ChiTietHoaDonDTO;

            if (itemCanXoa != null)
            {
                var confirm = MessageBox.Show($"Bạn có muốn xóa sách '{itemCanXoa.TenSach}' khỏi giỏ hàng?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    // 3. Xóa khỏi danh sách List tạm thời (Không xóa trong DB)
                    _giỏHàng.Remove(itemCanXoa);

                    // 4. Cập nhật lại giao diện và tính lại tổng tiền
                    UpdateGioHangGrid();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tien_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private async void btn__Click(object sender, EventArgs e)
        {
            if (!_giỏHàng.Any())
            {
                MessageBox.Show("Giỏ hàng trống!");
                return;
            }

            khách_hàng frmKH = new khách_hàng(_giỏHàng);
            if (frmKH.ShowDialog() == DialogResult.OK)
            {
                // 1. Xóa giỏ hàng cũ
                _giỏHàng.Clear();
                UpdateGioHangGrid();

                // 2. Cập nhật lại kho sách (vì vừa bán mất một ít)
                await LoadSachAsync();

                // 3. QUAN TRỌNG: Load lại danh sách hóa đơn để thấy dòng mới vừa lưu
                await LoadLichSuHoaDon();

                MessageBox.Show("Thanh toán thành công!");
            }
        }
        private async Task FillComboSachHD()
        {
            // Lấy danh sách từ Service
            var list = await _sachService.GetSachesAsync();

            // Xóa dữ liệu cũ để tránh trùng lặp
            txtSach.DataSource = null;

            // Gán danh sách mới
            txtSach.DataSource = list;

            // QUAN TRỌNG: Tên thuộc tính phải khớp chính xác với class SachDTO
            txtSach.DisplayMember = "TenSach"; // Cột sẽ hiển thị chữ cho người dùng chọn
            txtSach.ValueMember = "MaSach";    // Giá trị ngầm định bên dưới
        }
        private void So_lương_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSach.SelectedItem is SachDTO selectedSach)
            {
                // Cập nhật giới hạn cho ô Số lượng dựa trên tồn kho của sách vừa chọn
                So_lương.Minimum = selectedSach.SoLuong > 0 ? 1 : 0;
                So_lương.Maximum = selectedSach.SoLuong;
                So_lương.Value = So_lương.Minimum;

                // Bạn có thể hiển thị đơn giá hoặc số lượng tồn lên một Label để người dùng biết
                // Ví dụ: lblTonKho.Text = $"Kho còn: {selectedSach.SoLuong}";
            }
        }

        private void gio_hang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtGiaBan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_currentUser != null && _currentUser.VaiTro == "NhanVien")
            {
                // Nếu trang đang định mở không phải là tab3
                string[] allowedTabs = { "tabPage3" };

                if (!allowedTabs.Contains(e.TabPage.Name))
                {
                    MessageBox.Show("Quyền hạn của bạn (Nhân viên) không được phép vào mục này.", "Từ chối truy cập");
                    e.Cancel = true; // Đây chính là lệnh "Nhấn nhưng không chuyển được"
                }
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async Task LoadLichSuHoaDon()
        {
            try
            {
                // Gọi Service lấy dữ liệu
                var data = await _hoaDonService.GetHoaDonsAsync();

                // Gán dữ liệu vào Grid của Tab Lịch sử hóa đơn
                dgvLichSuHoaDon.DataSource = data;

                // Cấu hình tiêu đề cột cho người dùng dễ đọc
                dgvLichSuHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HĐ";
                dgvLichSuHoaDon.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvLichSuHoaDon.Columns["SoDienThoai"].HeaderText = "SĐT";
                dgvLichSuHoaDon.Columns["NgayLap"].HeaderText = "Thời Gian";
                dgvLichSuHoaDon.Columns["TongThanhToan"].HeaderText = "Tổng Tiền";
                dgvLichSuHoaDon.Columns["GhiChuSach"].HeaderText = "Sách Đã Mua";

                // Cho phép cột Sách Đã Mua hiển thị dài hơn
                dgvLichSuHoaDon.Columns["GhiChuSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        private async void btnType_Them_Click_1(object sender, EventArgs e)
        {
            string tenLoai = textBox3.Text.Trim();
            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại sách!");
                return;
            }

            using var db = new NhaSachContext();
            var loaiMoi = new LoaiSach
            {
                MaLoaiSach = Guid.NewGuid().ToString(), // Tạo mã mới
                TenLoaiSach = tenLoai
            };

            db.LoaiSachs.Add(loaiMoi);
            await db.SaveChangesAsync();

            await LoadLoaiSachAsync(); // Tải lại lưới
            textBox3.Clear();
            MessageBox.Show("Thêm loại sách thành công!");
        }

        private async void btnType_Sua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLoaiSach))
            {
                MessageBox.Show("Vui lòng chọn loại sách cần sửa từ danh sách!");
                return;
            }

            using var db = new NhaSachContext();
            var loai = await db.LoaiSachs.FindAsync(_selectedMaLoaiSach);

            if (loai != null)
            {
                loai.TenLoaiSach = textBox3.Text.Trim();
                await db.SaveChangesAsync();
                await LoadLoaiSachAsync();
                MessageBox.Show("Cập nhật thành công!");
            }
        }

        private async void btnType_Xoa_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLoaiSach))
            {
                MessageBox.Show("Vui lòng chọn loại sách cần xóa!");
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa? Lưu ý: Nếu có sách thuộc loại này, việc xóa sẽ bị lỗi.",
                                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using var db = new NhaSachContext();
                    var loai = await db.LoaiSachs.FindAsync(_selectedMaLoaiSach);
                    if (loai != null)
                    {
                        db.LoaiSachs.Remove(loai);
                        await db.SaveChangesAsync();
                        await LoadLoaiSachAsync();
                        MessageBox.Show("Xóa thành công!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể xóa loại sách này vì đang có sách thuộc danh mục này!");
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnThốngKê_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngayChon = dtpNgayThongKe.Value;

                // SỬA TẠI ĐÂY: Gọi _tkService thay vì _hoaDonService
                var data = await _tkService.GetThongKeByDateAsync(ngayChon);

                // Gán dữ liệu lên UI
                lblTongDoanhThu.Text = $"Tổng doanh thu: {data.TongDoanhThu * 1000:N0} VNĐ";
                lblTongSoHD.Text = $"Tổng số hóa đơn: {data.TongSoHoaDon}";
                lblTongSachBan.Text = $"Tổng số sách đã bán: {data.TongSoSachDaBan}";

                dgvTopSach.DataSource = data.TopSachBanChay;
                dgvSachSapHet.DataSource = data.SachSapHet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            try
            {
                // 1. Gọi hàm Search từ service
                var ketQua = await _sachService.Search(keyword);

                // 2. Cập nhật thông qua BindingSource để Grid tự làm mới
                _bs.DataSource = ketQua;

                // 3. Đảm bảo UI cập nhật ngay lập tức
                _bs.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Nếu người dùng nhấn nút khi ô trống, có thể hiện thông báo hoặc load lại tất cả
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadSachAsync(); // Gọi hàm load toàn bộ danh sách cũ của bạn
                return;
            }

            try
            {
                // Thực hiện tìm kiếm khi nhấn nút
                var ketQua = await _sachService.Search(keyword);

                _bs.DataSource = ketQua;
                _bs.ResetBindings(false);

                if (ketQua.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sách phù hợp!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvSachSapHet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}