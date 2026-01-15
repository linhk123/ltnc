using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using demo_1.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using demo_1.DAL.Contexts;
namespace demo_1.DAL.DAO
{
    public class PhieuNhapRepository : IPhieuNhapRepository
    {
        private readonly NhaSachContext db = new NhaSachContext();

        public async Task<bool> SavePhieuNhapTransaction(PhieuNhap phieuNhap, List<ChiTietPhieuNhap> chiTiets)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                await db.PhieuNhaps.AddAsync(phieuNhap);

                foreach (var item in chiTiets)
                {
                    var sach = await db.Sachs.FindAsync(item.ma_sach);
                    if (sach != null)
                    {
                        // CỘNG KHO
                        sach.SoLuong += item.so_luong;

                        // Cập nhật giá bán mới vào danh mục Sách (nếu muốn thay đổi giá niêm yết ngay)
                        // Lưu ý: item.gia_nhap là giá vốn. Giá bán thường phải cao hơn.
                        // Ở đây tôi giữ nguyên giá bán cũ, hoặc bạn có thể logic: sach.GiaBan = item.gia_nhap * 1.2m;
                    }
                    await db.ChiTietPhieuNhaps.AddAsync(item);
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<List<PhieuNhap>> GetAllPhieuNhapAsync()
        {
            return await db.PhieuNhaps.ToListAsync();
        }
        

    }
}
