using demo_1.BLL.DTO;
using demo_1.DAL.Entity;
using System.Collections.Generic;

namespace demo_1.BLL.Interfaces
{
        public interface ISachRepository
        {
            Task<List<SachDTO>> GetSachesAsync();            // lấy danh sách từ cơ sở dữ liệu 
            //đóng vai trò như một hợp đồng cho các lớp triển khai để thực hiện thao tác lấy toàn bộ danh sách hiện thị lên datagridview
            Task<bool> Add(SachDTO sach);
            Task<bool> Update(SachDTO sach);
            Task<bool> Delete(string id);
            Task<List<SachDTO>> Search(string keyword);
            // Xử lý Transaction cho Hóa Đơn
            Task<bool> SaveHoaDonTransaction(HoaDon hoaDon, List<ChiTietHoaDon> chiTiets);
        }
}