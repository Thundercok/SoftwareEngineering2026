using System;
using System.Data;
using DataAccess;

namespace BusinessLogic
{
    public class OrderBLL
    {
        private readonly OrderDAO _dao = new OrderDAO();

        public DataTable GetAllOrders() => _dao.GetAllOrders();

        public DataTable GetOrderDetails(int orderId) => _dao.GetOrderDetails(orderId);

        public int CreateOrder(DateTime date, int agentId)
        {
            if (agentId <= 0)
                throw new Exception("Vui lòng chọn đại lý!");
            return _dao.InsertOrder(date, agentId);
        }

        public bool AddDetail(int orderId, int itemId, int qty, decimal unit)
        {
            if (qty <= 0)
                throw new Exception("Số lượng phải lớn hơn 0!");
            if (unit <= 0)
                throw new Exception("Đơn giá phải lớn hơn 0!");
            return _dao.InsertDetail(orderId, itemId, qty, unit);
        }

        public bool DeleteOrder(int orderId) => _dao.DeleteOrder(orderId);
    }
}