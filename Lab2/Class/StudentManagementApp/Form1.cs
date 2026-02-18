using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class Form1 : Form
    {
        private string connectionString;
        public Form1()
        {
            InitializeComponent();
            connectionString =
        ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblStudents", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO" +
                "tblStudents (StudentName,DateOfBirth, City, Age, YearOfEnroll, Major, GPA) " +
                "VALUES" +
                "(@StudentName, @DateOfBirth,@City, @Age, @YearOfEnroll, @Major, @GPA)", connection);
                cmd.Parameters.AddWithValue("@StudentName", "New Student");
                cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Now);
                cmd.Parameters.AddWithValue("@City", "City");
                cmd.Parameters.AddWithValue("@Age", 20);
                cmd.Parameters.AddWithValue("@YearOfEnroll", 2021);
                cmd.Parameters.AddWithValue("@Major", "Major");
                cmd.Parameters.AddWithValue("@GPA", 4.0);
                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int studentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StudentID"].Value);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE tblStudent SET " +
                            "StudentName = @StudentName," +
                            " DateOfBirth = @DateOfBirth, " +
                            "City = @City, " +
                            "Age = @Age, " +
                            "YearOfEnroll = @YearOfEnroll, " +
                            "Major = @Major, " +
                            "GPA = @GPA " +
                            "WHERE StudentID = @StudentID",
                        connection);
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        cmd.Parameters.AddWithValue("@StudentName", "Updated Student");
                        cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Now);
                        cmd.Parameters.AddWithValue("@City", "Updated City");
                        cmd.Parameters.AddWithValue("@Age", 21);
                        cmd.Parameters.AddWithValue("@YearOfEnroll", 2022);
                        cmd.Parameters.AddWithValue("@Major", "Updated Major");
                        cmd.Parameters.AddWithValue("@GPA", 3.9);
                        cmd.ExecuteNonQuery();
                        LoadData();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int studentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StudentID"].Value);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM tblStudents " +
                            "WHERE StudentID = @StudentID", connection);
                        cmd.Parameters.AddWithValue("@StudentID", studentID);
                        cmd.ExecuteNonQuery();
                        LoadData();
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
