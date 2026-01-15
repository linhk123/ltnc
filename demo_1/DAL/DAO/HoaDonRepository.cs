using demo_1.BLL.DTO;
using demo_1.DAL.Contexts;
using demo_1.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.DAL.DAO
{
    public class HoaDonRepository
    {
        private readonly NhaSachContext db = new NhaSachContext();

        public async Task<List<HoaDonDTO>> GetHoaDonsAsync()
        {
            return await db.HoaDons.Select(hd => new HoaDonDTO
            {
                MaHoaDon = hd.ma_hoa_don,
                NgayLap = hd.ngay_lap_hoa_don,
                TenKhachHang = hd.ten_khach_hang,
                SoDienThoai = hd.sdt_khach_hang, // Changed from SdtKhachHang to SoDienThoai
                TongThanhToan = hd.ChiTietHoaDons.Sum(ct => ct.so_luong * ct.Sach.GiaBan) // Changed from TongTien to TongThanhToan
            }).ToListAsync();
        }

        // 2. Lưu hóa đơn với Transaction (Trừ kho + Lưu giá nhập lúc đó)
        public async Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                // Thêm hóa đơn chính
                await db.HoaDons.AddAsync(hoaDon);

                foreach (var item in chiTiets)
                {
                    // Tìm sách để trừ kho
                    var sach = await db.Sachs.FindAsync(item.ma_sach);
                    if (sach == null) throw new Exception($"Sách {item.ma_sach} không tồn tại.");

                    if (sach.SoLuong < item.so_luong)
                        throw new Exception($"Sách {sach.TenSach} không đủ số lượng (Còn: {sach.SoLuong}).");

                    // TRỪ KHO
                    sach.SoLuong -= item.so_luong;

                    // SNAPSHOT GIÁ (Quan trọng để tính lãi)
                    item.gia_ban_luc_do = sach.GiaBan; // Giá bán hiện tại

                    // Lấy giá nhập gần nhất từ phiếu nhập
                    // Logic: Lấy chi tiết phiếu nhập mới nhất của sách này
                    var giaNhapGanNhat = await db.ChiTietPhieuNhaps
                        .Include(x => x.PhieuNhap)
                        .Where(x => x.ma_sach == item.ma_sach)
                        .OrderByDescending(x => x.PhieuNhap.ngay_lap_phieu_nhap)
                        .Select(x => x.gia_nhap)
                        .FirstOrDefaultAsync();

                    // Nếu chưa nhập lần nào thì giá vốn = 0 (hoặc xử lý tùy ý)
                    item.gia_nhap_luc_do = giaNhapGanNhat;

                    // Thêm chi tiết
                    await db.ChiTietHoaDons.AddAsync(item);
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync(); // Xác nhận thành công
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Hoàn tác nếu lỗi
                throw ex; // Ném lỗi ra để UI hiển thị
            }
        }
        // 3. Hàm thống kê chuẩn (Tính lãi dựa trên snapshot)
        public async Task<List<ThongKeDTO>> GetThongKeAsync(DateTime tuNgay, DateTime denNgay)
        {
            // Reset giờ để lấy trọn ngày
            var fromDate = tuNgay.Date;
            var toDate = denNgay.Date.AddDays(1).AddTicks(-1);

            return await db.ChiTietHoaDons
                .Include(ct => ct.HoaDon)
                .Include(ct => ct.Sach)
                .Where(ct => ct.HoaDon.ngay_lap_hoa_don >= fromDate && ct.HoaDon.ngay_lap_hoa_don <= toDate)
                .GroupBy(ct => new { ct.ma_sach, ct.Sach.TenSach })
                .Select(g => new ThongKeDTO
                {
                    MaSach = g.Key.ma_sach,
                    TenSach = g.Key.TenSach,
                    SoLuongBan = g.Sum(x => x.so_luong),

                    // Doanh thu = SL * Giá bán lúc đó
                    DoanhThu = g.Sum(x => x.so_luong * x.gia_ban_luc_do),

                    // Lợi nhuận = SL * (Giá bán lúc đó - Giá nhập lúc đó)
                    LoiNhuan = g.Sum(x => x.so_luong * (x.gia_ban_luc_do - x.gia_nhap_luc_do))
                })
                .ToListAsync();
        }

        
    }
}
