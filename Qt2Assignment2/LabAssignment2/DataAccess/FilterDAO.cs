using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class FilterDAO
    {
        public DataTable GetBestItems()
        {
            using (var conn = DbConnection.GetConnection())
            {
                string sql = @"SELECT TOP 10
                                i.ItemName, i.Size,
                                SUM(od.Quantity) AS TongSoLuong,
                                SUM(od.Quantity * od.UnitAmount) AS DoanhThu
                               FROM OrderDetail od
                               JOIN Item i ON od.ItemID = i.ItemID
                               GROUP BY i.ItemID, i.ItemName, i.Size
                               ORDER BY TongSoLuong DESC";
                var da = new SqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetItemsByAgent()
        {
            using (var conn = DbConnection.GetConnection())
            {
                string sql = @"SELECT a.AgentName, i.ItemName,
                                      SUM(od.Quantity) AS SoLuong,
                                      SUM(od.Quantity * od.UnitAmount) AS ThanhTien
                               FROM OrderDetail od
                               JOIN [Order] o ON od.OrderID = o.OrderID
                               JOIN Agent a   ON o.AgentID  = a.AgentID
                               JOIN Item i    ON od.ItemID   = i.ItemID
                               GROUP BY a.AgentName, i.ItemName
                               ORDER BY a.AgentName, ThanhTien DESC";
                var da = new SqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAgentPurchases()
        {
            using (var conn = DbConnection.GetConnection())
            {
                string sql = @"SELECT a.AgentName,
                                      COUNT(DISTINCT o.OrderID) AS SoDon,
                                      SUM(od.Quantity * od.UnitAmount) AS TongTien
                               FROM [Order] o
                               JOIN Agent a ON o.AgentID = a.AgentID
                               JOIN OrderDetail od ON od.OrderID = o.OrderID
                               GROUP BY a.AgentID, a.AgentName
                               ORDER BY TongTien DESC";
                var da = new SqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}