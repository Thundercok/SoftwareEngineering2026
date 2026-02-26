namespace BLL;

public class CustomerService
{
    private readonly CustomerRepository _repo;
    public CustomerService(CustomerRepository repo) => _repo = repo;

    public List<Customer> GetCustomers() => _repo.GetAll(); // [cite: 304, 306]
    public void CreateCustomer(Customer c) => _repo.Add(c); // [cite: 294, 297]
}