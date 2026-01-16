using Microsoft.EntityFrameworkCore; // Cần thêm namespace này để dùng .Include
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using demo_1.BLL.DTO;
using demo_1.BLL.Interfaces;
using demo_1.DAL.Entity;
using demo_1.DAL.Contexts;

namespace demo_1.DAL.DAO
{
    public class SachRepository : ISachRepository
    {
        private readonly NhaSachContext db = new NhaSachContext();

        // sử dung async giúp ứng dụng không bị chặn (block) trong lúc chờ SQL Server trả dữ liệu.
        public async Task<List<SachDTO>> GetSachesAsync()// hiển thị danh sách sách trên tab sách
        {
            var query = from sach in db.Sachs
                        join loaiSach in db.LoaiSachs on sach.MaLoaiSach equals loaiSach.MaLoaiSach
                        select new SachDTO
                        {
                            MaSach = sach.MaSach,
                            TenSach = sach.TenSach,
                            TacGia = sach.TacGia,
                            SoLuong = sach.SoLuong,
                            GiaBan = sach.GiaBan,
                            TenLoaiSach = loaiSach.TenLoaiSach
                        };
            return await query.ToListAsync();
        }

        public async Task<bool> Add(SachDTO sach)
        {
            try
            {
                // Map SachDTO to Sach entity
                var maLoai = db.LoaiSachs.FirstOrDefault(ls => ls.TenLoaiSach == sach.TenLoaiSach)?.MaLoaiSach;

                if (maLoai == null)
                {
                    throw new Exception("Không tìm thấy loại sách phù hợp với tên đã cung cấp.");
                }

                var entity = new Sach
                {
                    MaSach = sach.MaSach,
                    TenSach = sach.TenSach,
                    TacGia = sach.TacGia,
                    SoLuong = sach.SoLuong,
                    GiaBan = sach.GiaBan,
                    MaLoaiSach = maLoai // Now guaranteed non-null
                };

                db.Sachs.Add(entity);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                // Surface inner exception message for caller
                throw new Exception("Lỗi khi thêm sách: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm sách: " + ex.Message, ex);
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                var sach = await db.Sachs.FindAsync(id);
                if (sach == null)
                {
                    return false;
                }
                db.Sachs.Remove(sach);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi khi xóa sách: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa sách: " + ex.Message, ex);
            }
        }
        public async Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets)
        {
            // Example implementation: add HoaDon and its details in a transaction
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                db.HoaDons.Add(hoaDon);
                await db.SaveChangesAsync();

                foreach (var chiTiet in chiTiets)
                {
                    db.ChiTietHoaDons.Add(chiTiet);
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

        public async Task<List<SachDTO>> Search(string keyword)
        {
            var query = from sach in db.Sachs
                        join loaiSach in db.LoaiSachs on sach.MaLoaiSach equals loaiSach.MaLoaiSach
                        where sach.TenSach.Contains(keyword) ||
                              sach.TacGia.Contains(keyword) ||
                              loaiSach.TenLoaiSach.Contains(keyword)
                        select new SachDTO
                        {
                            MaSach = sach.MaSach,
                            TenSach = sach.TenSach,
                            TacGia = sach.TacGia,
                            SoLuong = sach.SoLuong,
                            GiaBan = sach.GiaBan,
                            TenLoaiSach = loaiSach.TenLoaiSach
                        };
            return await query.ToListAsync();
        }

        public async Task<bool> Update(SachDTO sach)
        {
            try
            {
                var entity = await db.Sachs.FindAsync(sach.MaSach);
                if (entity == null)
                {
                    return false;
                }
                entity.TenSach = sach.TenSach;
                entity.TacGia = sach.TacGia;
                entity.SoLuong = sach.SoLuong;
                entity.GiaBan = sach.GiaBan;
                entity.MaLoaiSach = db.LoaiSachs.FirstOrDefault(ls => ls.TenLoaiSach == sach.TenLoaiSach)?.MaLoaiSach;
                db.Sachs.Update(entity);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi khi cập nhật sách: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật sách: " + ex.Message, ex);
            }
        }

    }
}