using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AgentDAO
    {
        public DataTable GetAll()
        {
            using (var conn = DbConnection.GetConnection())
            {
                var da = new SqlDataAdapter("SELECT * FROM Agent ORDER BY AgentID", conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(string name, string address)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Agent(AgentName,Address) VALUES(@n,@a)", conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@a", address);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(int id, string name, string address)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Agent SET AgentName=@n, Address=@a WHERE AgentID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@a", address);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Agent WHERE AgentID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}