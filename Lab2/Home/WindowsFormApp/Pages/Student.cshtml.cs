using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormApp.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<DataRow> StudentList { get; set; } = new List<DataRow>();

        public StudentsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Tương đương với hàm LoadData() và Form_Load trong WinForms
        public void OnGet()
        {
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = _configuration.GetConnectionString("SchoolDBConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Logic y hệt tài liệu Lab
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblStudents", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    StudentList.Add(row);
                }
            }
        }

        // Tương đương với hàm btnAdd_Click trong WinForms
        public IActionResult OnPostAdd(string StudentName, DateTime DateOfBirth, string City, int Age, int YearOfEnroll)
        {
            string connectionString = _configuration.GetConnectionString("SchoolDBConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Dùng đúng câu lệnh SQL trong file Lab PDF
                string query = "INSERT INTO tblStudents (StudentName, DateOfBirth, City, Age, YearOfEnroll) VALUES (@StudentName, @DateOfBirth, @City, @Age, @YearOfEnroll)";
                SqlCommand cmd = new SqlCommand(query, connection);
                
                cmd.Parameters.AddWithValue("@StudentName", StudentName);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@YearOfEnroll", YearOfEnroll);
                
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage(); // Refresh lại trang
        }

        // Tương đương với hàm btnDelete_Click trong WinForms
        public IActionResult OnPostDelete(int StudentID)
        {
            string connectionString = _configuration.GetConnectionString("SchoolDBConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM tblStudents WHERE StudentID=@StudentID", connection);
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage(); // Refresh lại trang
        }
    }
}