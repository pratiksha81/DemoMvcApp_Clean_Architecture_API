using DemoMvcApp.Models;

namespace DemoMvcApp.Infrastructure
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        User GetById(int id);
    }

}
