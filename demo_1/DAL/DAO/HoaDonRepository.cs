using demo_1.BLL.DTO;
using demo_1.DAL.Contexts;
using demo_1.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_1.DAL.DAO
{
    public class HoaDonRepository
    {
        
        private readonly NhaSachContext db = new NhaSachContext();

        public async Task<List<HoaDonDTO>> GetHoaDonsAsync()
        {
            // Sử dụng ToListAsync để tránh lỗi đa luồng và lấy đầy đủ thông tin
            var hoaDons = await db.HoaDons
                .Include(hd => hd.ChiTietHoaDons)
                .ThenInclude(ct => ct.Sach)
                .ToListAsync();

            return hoaDons.Select(hd => new HoaDonDTO
            {
                MaHoaDon = hd.ma_hoa_don,
                NgayLap = hd.ngay_lap_hoa_don,
                TenKhachHang = hd.ten_khach_hang,
                SoDienThoai = hd.sdt_khach_hang,
                TongThanhToan = hd.ChiTietHoaDons.Sum(ct => ct.so_luong * ct.gia_ban_luc_do),

                // Tạo chuỗi danh sách sách mua (Ví dụ: "Sách A (2), Sách B (1)")
                GhiChuSach = string.Join(", ", hd.ChiTietHoaDons
                    .Select(ct => $"{ct.Sach.TenSach} ({ct.so_luong})"))
            }).ToList();
        }

        // 2. Lưu hóa đơn: Trừ kho trực tiếp từ bảng Sách
        public async Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets)
        {
            // Sử dụng using để đảm bảo giải phóng kết nối
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                await db.HoaDons.AddAsync(hoaDon);

                // Trong hàm SaveHoaDonTransaction của HoaDonRepository
                foreach (var item in chiTiets)
                {
                    // 1. Tìm sách trong kho
                    var sach = await db.Sachs.FindAsync(item.ma_sach);
                    if (sach == null) throw new Exception($"Sách mã {item.ma_sach} không tồn tại.");

                    // 2. Trừ kho
                    sach.SoLuong -= item.so_luong;

                    // 3. Sử dụng GiaBan để lấp vào cả hai vị trí
                    item.gia_ban_luc_do = sach.GiaBan; // Đây là giá bán cho khách

                    // Thay vì dùng gia_nhap (gây lỗi), ta dùng chính GiaBan của sách
                    // Hoặc bạn có thể để giá trị này bằng 0 nếu không muốn tính lợi nhuận
                    item.gia_nhap_luc_do = sach.GiaBan;

                    await db.ChiTietHoaDons.AddAsync(item);
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw; // Đẩy lỗi ra ngoài để UI xử lý (hiện MessageBox)
            }
        }

        // 3. Thống kê dựa trên dữ liệu Sách
        public async Task<List<ThongKeDTO>> GetThongKeAsync(DateTime tuNgay, DateTime denNgay)
        {
            var fromDate = tuNgay.Date;
            var toDate = denNgay.Date.AddDays(1).AddTicks(-1);

            return await db.ChiTietHoaDons
                .Where(ct => ct.HoaDon.ngay_lap_hoa_don >= fromDate && ct.HoaDon.ngay_lap_hoa_don <= toDate)
                .GroupBy(ct => new { ct.ma_sach, ct.Sach.TenSach })
                .Select(g => new ThongKeDTO
                {
                    MaSach = g.Key.ma_sach,
                    TenSach = g.Key.TenSach,
                    SoLuongBan = g.Sum(x => x.so_luong),
                    DoanhThu = g.Sum(x => x.so_luong * x.gia_ban_luc_do),
                    // Lợi nhuận tính theo giá nhập chốt trong hóa đơn
                    LoiNhuan = g.Sum(x => x.so_luong * (x.gia_ban_luc_do - x.gia_nhap_luc_do))
                })
                .ToListAsync();
        }
    }
}