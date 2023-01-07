using AuthApi.Models;
using AuthApi.Entites;
namespace AuthApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AuthApi.DpContext;
using AuthApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class AuthRepo:IAuthRepo
{
    private readonly ModelContext _con;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

        public AuthRepo(ModelContext con,IPasswordHasher passwordHasher,IConfiguration configuration)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task AddAsync(Token refreshToken)
        {
            await _con.Tokens.AddAsync(refreshToken);
        }

        public async Task<AuthResult> GenerateRefreshToken(AuthRequestBody Auth)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var userValid =await ValidateUser(Auth);
            if (!userValid) return new AuthResult()
            {
                Success = false,
                Error = new() { "User Doesnt Exist" },
            };
            var user = await _con.users.FirstOrDefaultAsync(a => a.email.Equals(Auth.email));
            var securekey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ThisIsTheSecretForGeneratingAKey(MustBeAtLeast32BitLong)"));
            var signingCreds=new SigningCredentials(securekey,SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddMonths(6),
                SigningCredentials =signingCreds
                };
            
            var tokenToReturn = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(tokenToReturn);
            return new AuthResult()
            {
                Error = new(),
                Success = true,
                Token = jwtToken
            };


        }

        public async Task<AuthResult> Authenticate(AuthRequestBody Auth)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var userValid =await ValidateUser(Auth);
            if (!userValid) return new AuthResult()
            {
                Success = false,
                Error = new() { "User Doesnt Exist" },
            };
            var user = await _con.users.FirstOrDefaultAsync(a => a.email.Equals(Auth.email));
            var securekey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ThisIsTheSecretForGeneratingAKey(MustBeAtLeast32BitLong)"));
            var signingCreds=new SigningCredentials(securekey,SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.id.ToString()));
            claimsForToken.Add(new Claim("Name", user.email));
            claimsForToken.Add(new Claim("Jti", Guid.NewGuid().ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsForToken),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials =signingCreds
                };
            
            var tokenToReturn = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(tokenToReturn);
            _con.Tokens.Add(new Token()
            {
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddHours(6),
                IsRevoked = false,
                IsUsed = true,
                myToken = jwtToken,
                email=user.email
            });
            return new AuthResult()
            {
                Error = new(),
                Success = true,
                Token = jwtToken
            };

        }

        public String RandomString(int v)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, v)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<bool> SaveChanges()
        {
            return (await _con.SaveChangesAsync() >= 0);

        }

        public async Task<bool> ValidateRefreshToken(string token)
        {
            return await _con.Tokens.AnyAsync(a => a.myToken.Equals(token));
        }

        public async Task<bool> ValidateUser(AuthRequestBody Auth)
        {
            var user= await _con.users.FirstOrDefaultAsync(a=>a.email.Equals(Auth.email));
            if (user == null)return false;
            var passCorrect = _passwordHasher.VerifyPassword(user.password, Auth.Password);
            if(passCorrect)return true;
            return false;
        }
        public async Task<bool> DeleteToken(string token)
        {
            var Token = await _con.Tokens.FirstOrDefaultAsync(a => a.myToken.Equals(token));
            if(Token == null)return false;
            _con.Tokens.Remove(Token);
            return true;
        }
    }
