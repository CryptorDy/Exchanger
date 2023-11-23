using Exchanger.Models;

namespace Exchanger.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(Guid id);
    Task Create(User user);
}
