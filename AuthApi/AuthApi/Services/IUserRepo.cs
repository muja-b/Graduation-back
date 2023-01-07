using AuthApi.Models;

namespace AuthApi.Services;

public interface IUserRepo
{
    Task<bool> UserExists(string email);
    Task<User> addUsersAsync(User user);
    Task<bool> SaveChangesAsync();
}