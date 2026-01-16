using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using demo_1.BLL.Implements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace demo_1
{
    public partial class khách_hàng : Form
    {
        private List<ChiTietHoaDonDTO> _data;
        private readonly IHoaDonRepository _hdService = new HoaDonService();

        public khách_hàng(List<ChiTietHoaDonDTO> giỏHàng)
        {
            InitializeComponent();
            _data = giỏHàng;
        }

        private void khách_hàng_Load(object sender, EventArgs e)
        {

        }

        private void btn_Ten_kh_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_SDT_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btn_Lưu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btn_Ten_kh.Text) || string.IsNullOrWhiteSpace(btn_SDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!");
                return;
            }

            try
            {
                // Tạo đối tượng HoaDon Entity
                var hoaDon = new HoaDon
                {
                    ma_hoa_don = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    ngay_lap_hoa_don = DateTime.Now,
                    ten_khach_hang = btn_Ten_kh.Text.Trim(),
                    sdt_khach_hang = btn_SDT.Text.Trim()
                };

                // Chuyển DTO sang Entity ChiTietHoaDon
                var chiTiets = _data.Select(x => new ChiTietHoaDon
                {
                    ma_hoa_don = hoaDon.ma_hoa_don,
                    ma_sach = x.MaSach,
                    so_luong = x.SoLuong
                }).ToList();

                // 4. Thực thi Transaction (Lỗi await đã được sửa bằng từ khóa async ở trên)
                bool result = await _hdService.SaveHoaDonTransaction(hoaDon, chiTiets);

                if (result)
                {
                    MessageBox.Show("Thanh toán thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
    }
}
