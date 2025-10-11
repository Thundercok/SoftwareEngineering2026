using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace WindowsFormApp.Pages
{
    public class DepartmentsModel : PageModel
    {
        public List<Dictionary<string, string>> DepartmentList { get; set; } = new();
        private string connectionString = "Data Source=app.db";

        public void OnGet()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createCmd = connection.CreateCommand();
                createCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS tblDepartments (
                        DepartmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        DepartmentName TEXT NOT NULL
                    )";
                createCmd.ExecuteNonQuery();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT DepartmentID, DepartmentName FROM tblDepartments";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartmentList.Add(new Dictionary<string, string> {
                            { "DepartmentID", reader["DepartmentID"].ToString() },
                            { "DepartmentName", reader["DepartmentName"].ToString() }
                        });
                    }
                }
            }
        }

        public IActionResult OnPostAdd(string DepartmentName)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO tblDepartments (DepartmentName) VALUES ($Name)";
                cmd.Parameters.AddWithValue("$Name", DepartmentName);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int DepartmentID)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM tblDepartments WHERE DepartmentID = $ID";
                cmd.Parameters.AddWithValue("$ID", DepartmentID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }
    }
}