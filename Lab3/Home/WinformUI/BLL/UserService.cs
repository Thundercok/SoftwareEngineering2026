namespace BLL;
using DAL;

public class UserService {
    private readonly UserRepository _userRepo;
    public UserService(UserRepository userRepo) => _userRepo = userRepo;

    public bool Authenticate(string username, string password) {
        return _userRepo.GetUser(username, password) != null;
    }
}
