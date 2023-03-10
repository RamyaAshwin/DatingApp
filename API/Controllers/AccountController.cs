
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
         _context=context;   
         _tokenService = tokenService;
        }

        [HttpPost("register")] //POST : api/ccount/register
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            if (await (UserExists(registerDto.UserName))) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName=registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt=hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Username=user.UserName,
            Token = _tokenService.CreateToken(user)
        };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {
            var user  = await _context.Users.SingleOrDefaultAsync(x => x.UserName == logindto.Username);
            if (user == null) return Unauthorized("Invalid User name!!");
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for(int i=0;i<computedHash.Length;i++)            
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid Password!!");
            }
            return new UserDto
             {
            Username=user.UserName,
            Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(String username)
        {
            return await _context.Users.AnyAsync(x =>x.UserName == username.ToLower());
        }
    }
}