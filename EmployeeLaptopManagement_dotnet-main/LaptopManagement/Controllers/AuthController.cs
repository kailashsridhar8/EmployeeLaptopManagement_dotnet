using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId && x.Password == user.Password);
            if (dbUser == null)
            {
                return BadRequest("User Not Found");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(Login user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim(ClaimTypes.Role,"0")
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


        [HttpGet]
        [Route("getusers"), Authorize(Roles = "0")]
        public async Task<IActionResult> GetUsers()
        {


            return
               Ok("Hi");

            //var isAdmin = "";
            //var re = Request;
            //var headers = re.Headers;
            //if (headers.ContainsKey("Authorization"))
            //{
            //    var token = headers["Authorization"];
            //    if (token != 0)
            //    {
            //        return Ok("allDetails");
            //    }

            //    return Ok("hel");
            //}
            //return Ok();


        }
    }
}


