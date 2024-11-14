using DemoMvcApp.Models;
using DemoMvcApp.Data;


namespace DemoMvcApp.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to authenticate user from the database
        public User Authenticate(string username, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        // Method to retrieve user by ID from the database
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
