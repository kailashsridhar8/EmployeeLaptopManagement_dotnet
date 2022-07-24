using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using bcrypt = BCrypt.Net.BCrypt;
namespace LaptopManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly laptopmanagementContext _context;

        public AuthController(IConfiguration configuration, laptopmanagementContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
         
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId);
            if(dbUser != null)
            {
                return BadRequest("User Already exists");
            }
            user.Password = bcrypt.HashPassword(user.Password,12);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully");

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId);
               if (bcrypt.Verify(user.Password, dbUser.Password)) { 
            if (dbUser == null)
            {
                return BadRequest("User Not Found");
            }
            }
            string role = dbUser.Role.ToString();
            string token = CreateToken(dbUser,role);
                return Ok(token);
        }

        private string CreateToken(User user,string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}


