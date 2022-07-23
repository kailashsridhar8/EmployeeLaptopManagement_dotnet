using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;

namespace LaptopManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly laptopmanagementContext _context;
        public UserController(laptopmanagementContext context)
        {
            _context = context;
        }
       
        



        [HttpPut]
        [Route("[Action]/{userid}")]
        public async Task<ActionResult<User>> UpdateUserProfile(int userid, [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);
                if (userdb != null)
                {

                    userdb.Name = user.Name;
                    userdb.EmailId = user.EmailId;
                    userdb.Password = bcrypt.HashPassword(user.Password, 12);
                   
                    await _context.SaveChangesAsync();
                    return Ok(userdb);

                }
                return Ok("Error while updating Student Profile data");

            }

        }



    }
}
