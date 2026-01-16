using demo_1.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace demo_1.DAL.Contexts
{
    public class NhaSachContext : DbContext
    {
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<LoaiSach> LoaiSachs { get; set; }
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Kiểm tra xem đã cài package System.Configuration.ConfigurationManager chưa
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["NhaSachConnection"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình khóa chính phức hợp
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasKey(x => new { x.ma_hoa_don, x.ma_sach });

           
            base.OnModelCreating(modelBuilder);
        }
    }
}