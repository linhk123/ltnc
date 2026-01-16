using demo_1.BLL.DTO;
using demo_1.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.Implements
{
    public class thongKeService
    {
        public async Task<ThongKeDTO> GetThongKeByDateAsync(DateTime date)
        {
            using var db = new NhaSachContext();
            var result = new ThongKeDTO();

            // 1. Dùng đúng tên cột ngay_lap_hoa_don
            var hoaDons = db.HoaDons
                .Where(h => h.ngay_lap_hoa_don.Date == date.Date)
                .ToList();

            result.TongSoHoaDon = hoaDons.Count;

            // Tính tổng doanh thu từ chi tiết để chính xác nhất
            var maHoaDons = hoaDons.Select(h => h.ma_hoa_don).ToList();
            var chiTietsNgay = db.ChiTietHoaDons
                .Where(ct => maHoaDons.Contains(ct.ma_hoa_don))
                .ToList();

            result.TongDoanhThu = chiTietsNgay.Sum(ct => ct.so_luong * ct.gia_ban_luc_do);
            result.TongSoSachDaBan = chiTietsNgay.Sum(ct => ct.so_luong);

            // 2. Top sách bán chạy
            result.TopSachBanChay = chiTietsNgay
            .GroupBy(ct => ct.ma_sach)
             .Select(g => new {
                    Mã_Sách = g.Key,
                    Số_Lượng = g.Sum(x => x.so_luong),
                    Doanh_Thu = g.Sum(x => x.so_luong * x.gia_ban_luc_do)
                 }
             )
             .OrderByDescending(x => x.Số_Lượng)
            .Cast<dynamic>().ToList();

            // 3. Sách sắp hết
            result.SachSapHet = db.Sachs
                .Where(s => s.SoLuong < 5)
                .Select(s => new { s.TenSach, s.SoLuong })
                .Cast<dynamic>().ToList();

            return result;
        }
    }
}
