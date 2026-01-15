using demo_1.DAL.Entity;
using demo_1.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo_1.BLL.Interfaces
{
    public interface IPhieuNhapRepository
    {
        Task<bool> SavePhieuNhapTransaction(PhieuNhap phieuNhap, List<ChiTietPhieuNhap> chiTiets);
        Task<List<PhieuNhap>> GetAllPhieuNhapAsync();
    }
}
