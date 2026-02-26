using System.Linq;

namespace DAL; 

public class UserRepository 
{
    private readonly Exer1DbContext _context; 

    public UserRepository(Exer1DbContext context) 
    {
        _context = context;
    }

    public User? GetUser(string username, string password) => 
        _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
}