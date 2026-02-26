public class CustomerRepository {
    private readonly Exer1DbContext _context;
    public CustomerRepository(Exer1DbContext context) => _context = context;

    public List<Customer> GetAll() => _context.Customers.ToList(); // [cite: 211, 214]
    public void Add(Customer c) { _context.Customers.Add(c); _context.SaveChanges(); } // [cite: 202, 206]
}