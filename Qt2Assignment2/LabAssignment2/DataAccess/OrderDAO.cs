using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class OrderDAO
    {
        public DataTable GetAllOrders()
        {
            using (var conn = DbConnection.GetConnection())
            {
                string sql = @"SELECT o.OrderID, o.OrderDate,
                                      a.AgentName, o.AgentID
                               FROM [Order] o
                               JOIN Agent a ON o.AgentID = a.AgentID
                               ORDER BY o.OrderID DESC";
                var da = new SqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetOrderDetails(int orderId)
        {
            using (var conn = DbConnection.GetConnection())
            {
                string sql = @"SELECT od.ID, i.ItemName, od.Quantity,
                                      od.UnitAmount,
                                      (od.Quantity * od.UnitAmount) AS ThanhTien
                               FROM OrderDetail od
                               JOIN Item i ON od.ItemID = i.ItemID
                               WHERE od.OrderID = @id";
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", orderId);
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int InsertOrder(DateTime date, int agentId)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    @"INSERT INTO [Order](OrderDate,AgentID)
                      VALUES(@d,@a); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@d", date);
                cmd.Parameters.AddWithValue("@a", agentId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool InsertDetail(int orderId, int itemId, int qty, decimal unit)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    @"INSERT INTO OrderDetail(OrderID,ItemID,Quantity,UnitAmount)
                      VALUES(@o,@i,@q,@u)", conn);
                cmd.Parameters.AddWithValue("@o", orderId);
                cmd.Parameters.AddWithValue("@i", itemId);
                cmd.Parameters.AddWithValue("@q", qty);
                cmd.Parameters.AddWithValue("@u", unit);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                var cmd1 = new SqlCommand(
                    "DELETE FROM OrderDetail WHERE OrderID=@id", conn);
                cmd1.Parameters.AddWithValue("@id", orderId);
                cmd1.ExecuteNonQuery();

                var cmd2 = new SqlCommand(
                    "DELETE FROM [Order] WHERE OrderID=@id", conn);
                cmd2.Parameters.AddWithValue("@id", orderId);
                return cmd2.ExecuteNonQuery() > 0;
            }
        }
    }
}
