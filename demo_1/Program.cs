using demo_1.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace demo_1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                using (var db = new NhaSachContext())
                {
                    db.Database.EnsureCreated(); // tạo DB nếu chưa có
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối CSDL:\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return; // KHÔNG chạy app nếu lỗi DB
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new login());
        }
    }
}
