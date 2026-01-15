using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using System.ComponentModel;

namespace demo_1
{
    public partial class Form1 : Form
    {
        private NguoiDung _currentUser;
        private readonly ISachRepository _sachService;
        private readonly IPhieuNhapRepository _phieuNhapService;
        private readonly BindingSource _bs = new BindingSource();
        private string _selectedMaSach;

        // selected category id for LoaiSach tab
        private string _selectedMaLoaiSach;
        private readonly BindingSource _bsLoai = new BindingSource();

        private List<ChiTietHoaDonDTO> _giỏHàng = new List<ChiTietHoaDonDTO>();
        private BindingSource _bsGiỏHàng = new BindingSource();

        private BindingList<ChiTietPhieuNhapDTO> _tempPhieuNhap = new BindingList<ChiTietPhieuNhapDTO>();

        // PHẢI CÓ (NguoiDung user) ở đây
        public Form1(NguoiDung user)
        {
            InitializeComponent(); // Để trống ngoặc này, không điền gì vào đây

            // Khởi tạo service
            _sachService = new SachService();
            _phieuNhapService = new PhieuNhapService();

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

            // Gán user từ tham số vào biến private của class
            _currentUser = user;
            ApplyPhanQuyen();

            // Load dữ liệu
            _ = LoadSachAsync();
            _ = LoadLoaiSachAsync();
            _ = FillComboSachHD();
            dgvPhieuNhap.DataSource = _tempPhieuNhap;

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
                    if (tabControl1.TabPages.Contains(tabPage4)) tabControl1.TabPages.Remove(tabPage4);
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
            try
            {
                using var db = new NhaSachContext();
                var types = await Task.Run(() => db.LoaiSachs.OrderBy(x => x.TenLoaiSach).ToList());

                // populate combo used by book tab
                cboLoaiSach.DisplayMember = "TenLoaiSach";
                cboLoaiSach.ValueMember = "MaLoaiSach";
                cboLoaiSach.DataSource = types;

                // populate category grid on tab
                _bsLoai.DataSource = types.Select(t => new { t.MaLoaiSach, t.TenLoaiSach }).ToList();
                LoaddataType.AutoGenerateColumns = true;

                if (LoaddataType.Columns.Contains("MaLoaiSach"))
                    LoaddataType.Columns["MaLoaiSach"].Visible = false;
                if (LoaddataType.Columns.Contains("TenLoaiSach"))
                    LoaddataType.Columns["TenLoaiSach"].HeaderText = "Tên loại sách";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải loại sách: " + ex.Message);
            }
        }

        private void LoaddataSach_SelectionChanged(object sender, EventArgs e)
        {
            if (LoaddataSach.CurrentRow?.DataBoundItem is SachDTO dto)
            {
                _selectedMaSach = dto.MaSach;
                txtTenSach.Text = dto.TenSach;
                txtTacGia.Text = dto.TacGia;
                nudSoLuong.Value = Math.Max(0, dto.SoLuong);
                txtGiaBan.Value = dto.GiaBan;

                // select combo by MaLoaiSach if available
                if (!string.IsNullOrEmpty(dto.MaLoaiSach))
                {
                    cboLoaiSach.SelectedValue = dto.MaLoaiSach;
                }
                else if (!string.IsNullOrEmpty(dto.TenLoaiSach))
                {
                    for (int i = 0; i < cboLoaiSach.Items.Count; i++)
                    {
                        if (cboLoaiSach.Items[i] is LoaiSach ls && ls.TenLoaiSach == dto.TenLoaiSach)
                        {
                            cboLoaiSach.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void LoaddataType_SelectionChanged(object sender, EventArgs e)
        {
            // Selection uses anonymous type with MaLoaiSach and TenLoaiSach
            var row = LoaddataType.CurrentRow;
            if (row?.DataBoundItem != null)
            {
                var item = row.DataBoundItem;
                var ma = item.GetType().GetProperty("MaLoaiSach")?.GetValue(item) as string;
                var ten = item.GetType().GetProperty("TenLoaiSach")?.GetValue(item) as string;
                _selectedMaLoaiSach = ma;
                textBox3.Text = ten ?? string.Empty;
            }
            else
            {
                _selectedMaLoaiSach = null;
                textBox3.Text = string.Empty;
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

        private void label13_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện (như trong ảnh bạn chụp)
            var tenSach = cboSachPN.Text; // ComboBox tên sách
            int soLuong = (int)numSoLuong.Value;
            decimal giaNhap = numGiaTien.Value;

            // 2. Thêm vào một List tạm hoặc BindingSource hiển thị lên Grid
            // Sau đó mới bấm nút LƯU để đẩy vào Database
        }

        private async void btnPN_Them_Click(object sender, EventArgs e)
        {
            if (cboSachPN.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách từ danh sách!");
                return;
            }

            var item = new ChiTietPhieuNhapDTO
            {
                MaSach = cboSachPN.SelectedValue.ToString(),
                TenSach = cboSachPN.Text,
                SoLuong = (int)numSoLuong.Value,
                gia_nhap = numGiaTien.Value // Đảm bảo tên thuộc tính khớp với DTO (GiaNhap)
            };
            _tempPhieuNhap.Add(item);
        }
        public async Task<bool> LuuHoaDonAsync(HoaDon hd, List<ChiTietHoaDonDTO> chiTiets)
        {
            // 1. Khởi tạo Repository (Đảm bảo file HoaDonRepository.cs của bạn đã được lưu)
            var hoaDonRepo = new demo_1.DAO.HoaDonRepository();

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

        private void btnPN_Sua_Click(object sender, EventArgs e)
        {

        }

        private void btnPN_Xoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow != null)
            {
                _tempPhieuNhap.RemoveAt(dgvPhieuNhap.CurrentRow.Index);
            }
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
            if (string.IsNullOrEmpty(_selectedMaSach)) return;

            try
            {
                var dto = ReadDtoFromInputs();
                var ok = await _sachService.Update(dto);

                if (ok)
                {
                    await LoadSachAsync(); // Tải lại danh sách sau khi sửa
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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

        private void btn__Click(object sender, EventArgs e)
        {
            if (!_giỏHàng.Any())
            {
                MessageBox.Show("Giỏ hàng trống!");
                return;
            }

            // Mở Form khách hàng và truyền danh sách giỏ hàng sang
            khách_hàng frmKH = new khách_hàng(_giỏHàng);
            if (frmKH.ShowDialog() == DialogResult.OK)
            {
                // Nếu thanh toán thành công, xóa giỏ hàng và load lại kho
                _giỏHàng.Clear();
                UpdateGioHangGrid();
                _ = LoadSachAsync();
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
            bool hasSelection = LoaddataSach.CurrentRow != null;

            // 1. Kích hoạt hoặc vô hiệu hóa nút
            btnSua.Enabled = hasSelection;
            btnXoa.Enabled = hasSelection;

            // 2. Đổ dữ liệu từ dòng được chọn lên các TextBox/NumericUpDown
            if (hasSelection && LoaddataSach.CurrentRow.DataBoundItem is SachDTO dto)
            {
                _selectedMaSach = dto.MaSach;
                txtTenSach.Text = dto.TenSach;
                txtTacGia.Text = dto.TacGia;
                nudSoLuong.Value = dto.SoLuong;

                // Hiển thị giá lên TextBox hoặc NumericUpDown
                txtGiaBan.Value = dto.GiaBan;

                // 3. Hiển thị giá tiền có đơn vị nghìn lên Label
                // Ví dụ: lblGiaHienThi.Text = dto.GiaBan.ToString("N0") + " VNĐ";
                // Cách khác nếu bạn muốn tự thêm 3 số 0 thủ công (không khuyến khích): 
                // lblGiaHienThi.Text = (dto.GiaBan).ToString() + ".000 VNĐ";
            }
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

        private async void btnPN_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new NhaSachContext();
                using var transaction = await db.Database.BeginTransactionAsync();

                // 1. Tạo Phiếu Nhập mới
                var pn = new PhieuNhap
                {
                    ma_phieu_nhap = Guid.NewGuid().ToString(),
                    ngay_lap_phieu_nhap = dtpNgayLapPN.Value
                };
                db.PhieuNhaps.Add(pn);

                foreach (var item in _tempPhieuNhap)
                {
                    // 2. Thêm Chi tiết Phiếu Nhập (để sau này Thống kê giá nhập)
                    var ct = new ChiTietPhieuNhap
                    {
                        ma_phieu_nhap = pn.ma_phieu_nhap,
                        ma_sach = item.MaSach,
                        so_luong = item.SoLuong,
                        gia_nhap = item.gia_nhap // Lưu để tính lãi sau này
                    };
                    db.ChiTietPhieuNhaps.Add(ct);

                    // 3. Cập nhật bảng Sách (Tăng số lượng kho)
                    var sach = await db.Sachs.FindAsync(item.MaSach);
                    if (sach != null)
                    {
                        sach.SoLuong += item.SoLuong; // Cập nhật tồn kho
                                                      // Nếu muốn cập nhật luôn tác giả/tên từ phiếu nhập:
                                                      // sach.TenSach = item.TenSach; 
                    }
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                _tempPhieuNhap.Clear(); // Xóa sạch grid sau khi lưu thành công
                MessageBox.Show("Nhập kho thành công và đã cập nhật số lượng sách!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }
    }
}