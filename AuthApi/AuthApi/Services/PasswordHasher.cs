namespace AuthApi.Services;

public class PasswordHasher :IPasswordHasher
{
    public string GeneratePasswordHash(string password)
    {
        return password;
    }

    public bool VerifyPassword(string password,string HashedPassword)
    {
        var hashed=GeneratePasswordHash(password);
        return hashed.Equals(HashedPassword);
    }
}