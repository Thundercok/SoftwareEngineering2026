using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient; // Nhớ cài package System.Data.SqlClient qua NuGet

namespace WindowsFormApp.Pages
{
    public class LoginFormModel : PageModel
    {
        private readonly IConfiguration _configuration;

        // Bắt dữ liệu từ HTML gửi lên
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public LoginFormModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Chạy khi trang vừa tải lên (để trống)
        }

        public IActionResult OnPost()
        {
            // Lấy chuỗi kết nối từ appsettings.json
            string connectionString = _configuration.GetConnectionString("SchoolDBConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM tblUsers WHERE Username=@Username AND Password=@Password";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        // Đăng nhập thành công, chuyển hướng sang trang quản lý sinh viên
                        return RedirectToPage("/Index"); 
                    }
                    else
                    {
                        ErrorMessage = "Login failed. Please check your username and password.";
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Error: " + ex.Message;
                    return Page();
                }
            }
        }
    }
}