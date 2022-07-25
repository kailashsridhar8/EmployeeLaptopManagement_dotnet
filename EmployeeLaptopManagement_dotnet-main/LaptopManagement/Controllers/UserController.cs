using LaptopManagement.Models;
using LaptopManagement.Services;
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
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }
       
        

        [HttpPut]
        [Route("[Action]/{userid}")]
        public async Task<IActionResult> UpdateUserProfile(int userid, [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _service.UpdateUser(userid, user);
                if (userdb != null)
                {

                
                    return Ok(userdb);

                }
                return BadRequest(new { message = "Error while updating Student Profile data" });

            }

        }



    }
}
