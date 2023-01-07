using Microsoft.EntityFrameworkCore;
using AuthApi.DpContext;
using AuthApi.Models;

namespace AuthApi.Services;

public class UserRepo:IUserRepo
{
    private readonly ModelContext _con;
    public UserRepo(ModelContext con)
    {
        _con = con;
    }
    public async Task<bool> UserExists(string email)
    {
        var exists=await _con.users.AnyAsync(user => user.email.Equals(email));
        return exists;
    }

    public async Task<User> addUsersAsync(User user)
    {
        var added = await _con.users.AddAsync(user);
        return added.Entity;
    }

    public async Task<bool> SaveChangesAsync()
    {
        var saved=await _con.SaveChangesAsync();
        if (saved > 0) return true;
        return false;
    }
}