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
    public class LoaiSachRepository
    {
        private readonly NhaSachContext db = new NhaSachContext();

        // Hiển thị danh sách loại sách và thống kê số lượng sách mỗi loại
        public async Task<List<LoaiSachDTO>> GetLoaiSachsAsync()
        {
            var query = from ls in db.LoaiSachs
                        select new LoaiSachDTO
                        {
                            MaLoaiSach = ls.MaLoaiSach,
                            TenLoaiSach = ls.TenLoaiSach,
                            SoLuongSach = ls.Sachs.Count // Thống kê nhanh
                        };
            return await query.ToListAsync();
        }

        // Thêm loại sách mới
        public async Task<bool> Add(LoaiSach loai)
        {
            try
            {
                db.LoaiSachs.Add(loai);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi khi thêm loại sách: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi hệ thống: " + ex.Message, ex);
            }
        }

        // Cập nhật loại sách
        public async Task<bool> Update(LoaiSach loai)
        {
            try
            {
                var entity = await db.LoaiSachs.FindAsync(loai.MaLoaiSach);
                if (entity == null) return false;

                entity.TenLoaiSach = loai.TenLoaiSach;

                db.LoaiSachs.Update(entity);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi khi cập nhật: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
        }

        // Xóa loại sách
        public async Task<bool> Delete(string id)
        {
            try
            {
                var entity = await db.LoaiSachs.FindAsync(id);
                if (entity == null) return false;

                // Kiểm tra ràng buộc: Nếu có sách thuộc loại này thì không cho xóa
                bool hasBooks = await db.Sachs.AnyAsync(s => s.MaLoaiSach == id);
                if (hasBooks)
                    throw new Exception("Không thể xóa loại sách này vì vẫn còn sách thuộc thể loại này.");

                db.LoaiSachs.Remove(entity);
                return await db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa loại sách: " + ex.Message, ex);
            }
        }

        // Tìm kiếm loại sách
        public async Task<List<LoaiSachDTO>> Search(string keyword)
        {
            var query = from ls in db.LoaiSachs
                        where ls.TenLoaiSach.Contains(keyword) || ls.MaLoaiSach.Contains(keyword)
                        select new LoaiSachDTO
                        {
                            MaLoaiSach = ls.MaLoaiSach,
                            TenLoaiSach = ls.TenLoaiSach,
                            SoLuongSach = ls.Sachs.Count
                        };
            return await query.ToListAsync();
        }
    }
}