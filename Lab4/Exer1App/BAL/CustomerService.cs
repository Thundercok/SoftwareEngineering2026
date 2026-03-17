using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        public void AddCustomer(Customer customer)
        {
            // Business logic before adding customer
            _customerRepository.AddCustomer(customer);
        }
        public Customer GetCustomer(int id)
        {
            // Business logic before retrieving customer
            return _customerRepository.GetCustomerById(id);
        }
        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }
    }
}
