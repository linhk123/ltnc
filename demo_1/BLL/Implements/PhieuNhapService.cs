using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Contexts;
using demo_1.DAL.Entity;
using demo_1.DAO;
using Microsoft.EntityFrameworkCore; // <--- THÊM DÒNG NÀY
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace demo_1.BLL.Implements
{
    public class PhieuNhapService : IPhieuNhapRepository
    {
        private readonly NhaSachContext db = new NhaSachContext();

        public async Task<bool> LuuPhieuNhapAsync(PhieuNhap pn, List<ChiTietPhieuNhapDTO> chiTiets)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                db.PhieuNhaps.Add(pn);
                foreach (var item in chiTiets)
                {
                    db.ChiTietPhieuNhaps.Add(new ChiTietPhieuNhap
                    {
                        ma_phieu_nhap = pn.ma_phieu_nhap,
                        ma_sach = item.MaSach,
                        so_luong = item.SoLuong,
                        gia_nhap = item.gia_nhap
                    });

                    var sach = await db.Sachs.FindAsync(item.MaSach);
                    if (sach != null) sach.SoLuong += item.SoLuong;
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

        // Sau khi thêm using, ToListAsync() sẽ hết bị gạch đỏ
        public async Task<List<PhieuNhap>> GetAllPhieuNhapAsync() => await db.PhieuNhaps.ToListAsync();

        public Task<bool> SavePhieuNhapTransaction(PhieuNhap phieuNhap, List<ChiTietPhieuNhap> chiTiets)
        {
            throw new NotImplementedException();
        }
    }
}