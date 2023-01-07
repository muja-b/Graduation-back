using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using AuthApi.Entites;
using AuthApi.Models;
using AuthApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApi.Controllers
{
    [Route("TouchTyping/Authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _authRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepo _UserRepo;

        public AuthController(IAuthRepo auth,IConfiguration configuration,IPasswordHasher passwordHasher,IUserRepo userRepo)
        {
            _authRepo=auth;
            _passwordHasher = passwordHasher;
            _UserRepo = userRepo;
        }
        // POST api/<AuothController>
        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResult>> Authinticate(AuthRequestBody Auth)
        {
            var tokenRes=await _authRepo.Authenticate(Auth);
            var RefreshToken = await _authRepo.GenerateRefreshToken(Auth);
            tokenRes.Refreshtoken = RefreshToken.Token;
            await _authRepo.SaveChanges();
            return Ok(tokenRes);
        }
    [HttpPost("SignUp")]
    public async Task<ActionResult<AuthResult>> SignUp([FromBody]User ReqUser)
        {
            if(await _UserRepo.UserExists(ReqUser.email))
                return BadRequest(new AuthResult()
                {
                    Token = null,
                    Success = false,
                    Error = new() {"User Exists"}
                });
            ReqUser.password = _passwordHasher.GeneratePasswordHash(ReqUser.password);
            await _UserRepo.addUsersAsync(ReqUser);
            await _UserRepo.SaveChangesAsync();
            return  await _authRepo.Authenticate(new AuthRequestBody() {
            email = ReqUser.email,
            Password = ReqUser.password,
            });
        }
        
        [HttpDelete("LogOut")]
        public async Task<ActionResult> Logout()
        {
            string token = getToken();
            var validReq = await _authRepo.DeleteToken(token);
            if (!validReq)
            {
                return BadRequest();
            }
            await _authRepo.SaveChanges();
            return Ok();
        }
        [HttpGet("Validate")]
        public async Task<ActionResult<bool>> TokenValid()
        {
            string token = getToken();
            var validReq = await _authRepo.ValidateRefreshToken(token);
            if (!validReq)
            {
                return BadRequest();
            }
            return Ok(true);
        }
        private string getToken()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            token = token.Substring(7); //To Remove Bearer From the token 
            return token;
        }
    }    
}