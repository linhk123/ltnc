using demo_1.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
namespace demo_1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            using (var db = new NhaSachContext())
            {
                var user = await db.NguoiDungs.FirstOrDefaultAsync(u =>
                    u.TenDangNhap == txtUser.Text && u.MatKhau == txtPass.Text);

                if (user != null)
                {
                    // 1. Khởi tạo Form1 và truyền user vào
                    Form1 mainForm = new Form1(user);

                    // 2. Ẩn form login đi
                    this.Hide();

                    // 3. Hiển thị Form chính
                    mainForm.ShowDialog();

                    // 4. Sau khi đóng Form chính thì đóng luôn ứng dụng
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }

            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
