using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace WindowsFormApp.Pages
{
    public class StudentsModel : PageModel
    {
        // Dùng Dictionary để lưu dữ liệu hiển thị ra bảng HTML cho dễ
        public List<Dictionary<string, string>> StudentList { get; set; } = new();
        
        // Vẫn dùng file app.db chung với Login
        private string connectionString = "Data Source=app.db";

        public void OnGet()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                // 1. Tự động tạo bảng tblStudents nếu chưa có
                var createCmd = connection.CreateCommand();
                createCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS tblStudents (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentName TEXT,
                        DateOfBirth TEXT,
                        City TEXT,
                        Age INTEGER,
                        YearOfEnroll INTEGER
                    )";
                createCmd.ExecuteNonQuery();

                // 2. Load dữ liệu từ SQLite ra
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM tblStudents";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>
                        {
                            { "StudentID", reader["StudentID"].ToString() },
                            { "StudentName", reader["StudentName"].ToString() },
                            { "DateOfBirth", reader["DateOfBirth"].ToString() },
                            { "City", reader["City"].ToString() },
                            { "Age", reader["Age"].ToString() },
                            { "YearOfEnroll", reader["YearOfEnroll"].ToString() }
                        };
                        StudentList.Add(row);
                    }
                }
            }
        }

        // Hàm xử lý nút Add
        public IActionResult OnPostAdd(string StudentName, string DateOfBirth, string City, int Age, int YearOfEnroll)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                // SQLite dùng ký hiệu $ thay cho @
                cmd.CommandText = "INSERT INTO tblStudents (StudentName, DateOfBirth, City, Age, YearOfEnroll) VALUES ($StudentName, $DateOfBirth, $City, $Age, $YearOfEnroll)";
                
                cmd.Parameters.AddWithValue("$StudentName", StudentName);
                cmd.Parameters.AddWithValue("$DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("$City", City);
                cmd.Parameters.AddWithValue("$Age", Age);
                cmd.Parameters.AddWithValue("$YearOfEnroll", YearOfEnroll);
                
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage(); // Refresh lại trang
        }

        // Hàm xử lý nút Delete
        public IActionResult OnPostDelete(int StudentID)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM tblStudents WHERE StudentID=$StudentID";
                cmd.Parameters.AddWithValue("$StudentID", StudentID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage(); // Refresh lại trang
        }
    }
}