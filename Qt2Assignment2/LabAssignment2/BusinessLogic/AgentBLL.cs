using System;
using System.Data;
using DataAccess;

namespace BusinessLogic
{
    public class AgentBLL
    {
        private readonly AgentDAO _dao = new AgentDAO();

        public DataTable GetAllAgents() => _dao.GetAll();

        public bool AddAgent(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên đại lý không được để trống!");
            return _dao.Insert(name, address);
        }

        public bool UpdateAgent(int id, string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên đại lý không được để trống!");
            return _dao.Update(id, name, address);
        }

        public bool DeleteAgent(int id) => _dao.Delete(id);
    }
}