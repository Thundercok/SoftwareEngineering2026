using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace WindowsFormApp.Pages
{
    public class EnrollmentModel : PageModel
    {
        private string connectionString = "Data Source=app.db";
        public Dictionary<int, string> Courses { get; set; } = new();
        public List<Dictionary<string, string>> Enrollments { get; set; } = new();
        [BindProperty(SupportsGet = true)] public int SelectedCourseID { get; set; }

        public void OnGet()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var createCmd = connection.CreateCommand();
                createCmd.CommandText = "CREATE TABLE IF NOT EXISTS tblStudentCourses (StudentID INTEGER, CourseID INTEGER, PRIMARY KEY(StudentID, CourseID))";
                createCmd.ExecuteNonQuery();

                var cmdC = connection.CreateCommand();
                cmdC.CommandText = "SELECT CourseID, CourseName FROM tblCourses";
                using (var r = cmdC.ExecuteReader()) {
                    while(r.Read()) Courses.Add(r.GetInt32(0), r.GetString(1));
                }

                if (SelectedCourseID > 0) {
                    var cmdE = connection.CreateCommand();
                    cmdE.CommandText = "SELECT StudentID FROM tblStudentCourses WHERE CourseID = $id";
                    cmdE.Parameters.AddWithValue("$id", SelectedCourseID);
                    using (var r = cmdE.ExecuteReader()) {
                        while(r.Read()) Enrollments.Add(new Dictionary<string, string> { { "StudentID", r["StudentID"].ToString() } });
                    }
                }
            }
        }

        public IActionResult OnPostAdd(int StudentID, int CourseID) {
            using (var conn = new SqliteConnection(connectionString)) {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT OR IGNORE INTO tblStudentCourses (StudentID, CourseID) VALUES ($s, $c)";
                cmd.Parameters.AddWithValue("$s", StudentID);
                cmd.Parameters.AddWithValue("$c", CourseID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage(new { SelectedCourseID = CourseID });
        }
    }
}