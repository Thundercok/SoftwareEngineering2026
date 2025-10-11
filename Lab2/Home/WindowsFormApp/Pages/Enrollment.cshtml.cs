using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace WindowsFormApp.Pages
{
    public class CoursesModel : PageModel
    {
        public List<Dictionary<string, string>> CourseList { get; set; } = new();
        private string connectionString = "Data Source=app.db";

        public void OnGet()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createCmd = connection.CreateCommand();
                createCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS tblCourses (
                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseName TEXT NOT NULL,
                        Credits INTEGER NOT NULL,
                        DepartmentID INTEGER,
                        InstructorID INTEGER
                    )";
                createCmd.ExecuteNonQuery();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM tblCourses";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CourseList.Add(new Dictionary<string, string> {
                            { "CourseID", reader["CourseID"].ToString() },
                            { "CourseName", reader["CourseName"].ToString() },
                            { "Credits", reader["Credits"].ToString() }
                        });
                    }
                }
            }
        }

        public IActionResult OnPostAdd(string CourseName, int Credits)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO tblCourses (CourseName, Credits) VALUES ($Name, $Credits)";
                cmd.Parameters.AddWithValue("$Name", CourseName);
                cmd.Parameters.AddWithValue("$Credits", Credits);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }
    }
}