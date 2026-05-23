using System;
using System.Data;
using DataAccess;

namespace BusinessLogic
{
    public class ItemBLL
    {
        private readonly ItemDAO _dao = new ItemDAO();

        public DataTable GetAllItems() => _dao.GetAll();

        public bool AddItem(string name, string size, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Item name cannot be empty!");
            if (price < 0)
                throw new Exception("Price cannot be negative!");
            return _dao.Insert(name, size, price);
        }

        public bool UpdateItem(int id, string name, string size, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Item name cannot be empty!");
            if (price < 0)
                throw new Exception("Price cannot be negative!");
            return _dao.Update(id, name, size, price);
        }

        public bool DeleteItem(int id) => _dao.Delete(id);
    }
}