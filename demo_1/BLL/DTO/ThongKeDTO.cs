using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.DTO
{
    public class ThongKeDTO
    {
        // Các chỉ số tổng hợp hiện trên Label
        public decimal TongDoanhThu { get; set; }
        public int TongSoHoaDon { get; set; }
        public int TongSoSachDaBan { get; set; }

        // Danh sách dữ liệu hiện trên DataGridView
        // Chỉ khai báo mỗi loại 1 lần duy nhất
        public List<dynamic> TopSachBanChay { get; set; }
        public List<dynamic> SachSapHet { get; set; }

        // Các thuộc tính bổ trợ khác (nếu cần dùng cho mục đích khác)
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuongBan { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal LoiNhuan { get; set; }
    }
}
