using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ItemDAO
    {
        public DataTable GetAll()
        {
            using (var conn = DbConnection.GetConnection())
            {
                var da = new SqlDataAdapter("SELECT * FROM Item ORDER BY ItemID", conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool Insert(string name, string size, decimal price)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Item(ItemName,Size,Price) VALUES(@n,@s,@p)", conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@s", size);
                cmd.Parameters.AddWithValue("@p", price);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(int id, string name, string size, decimal price)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Item SET ItemName=@n,Size=@s,Price=@p WHERE ItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@s", size);
                cmd.Parameters.AddWithValue("@p", price);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Item WHERE ItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}