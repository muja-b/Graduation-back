using AuthApi.Models;
using AuthApi.Entites;

namespace AuthApi.Services;
public interface IAuthRepo
{
    Task AddAsync(Token refreshToken);
    string RandomString(int v);
    Task<bool> SaveChanges();
    Task<bool> ValidateUser(AuthRequestBody Auth);
    Task<AuthResult> Authenticate(AuthRequestBody Auth);
    Task<AuthResult> GenerateRefreshToken(AuthRequestBody auth);
    Task<bool> ValidateRefreshToken(string token);
    Task<bool> DeleteToken(string token);    
}