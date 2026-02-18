using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerRepository
    {
        private readonly Exer1DbContext _context;
        public CustomerRepository()
        {
            _context = new Exer1DbContext();
        }
        public void AddCustomer(Customer customer)
        {
            // Code to add customer to the database
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public Customer GetCustomerById(int id)
        {
            // Code to retrieve customer from the database
            //return new Customer(); // Placeholder
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
        public void UpdateCustomer(Customer customer)
        {
        }
        public void DeleteCustomer(int id)
        {
        }
    }
}
