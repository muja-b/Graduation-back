namespace AuthApi.Services;

public interface IPasswordHasher
{
    string GeneratePasswordHash(string password);
    bool VerifyPassword(string password,string HashedPassword); 
}