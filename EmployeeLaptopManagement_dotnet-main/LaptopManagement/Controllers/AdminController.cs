using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly laptopmanagementContext _context;
        public AdminController(laptopmanagementContext context)
        {
            _context = context;
        }

        [HttpPut, Authorize(Roles = "1")]
        [Route("[Action]/{userid}")]
        public async Task<ActionResult<User>> UpdateUserRole(int userid, [FromBody] sbyte newrole)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);
                if (userdb != null)
                {
                    userdb.Role = newrole;
                    await _context.SaveChangesAsync();
                    return Ok(userdb);
                }
                return Ok("Error while updating User Role");

            }

        }


        [HttpGet, Authorize(Roles = "1")]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Object>> GetUserById(int id)
        {
            if (id == null || _context?.Users == null)
            {
                return BadRequest(new { msg = "Id cannot not be null" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new { msg = $"User not found with id {id}" });
            }

            return Ok(user);
        }

        [HttpGet, Authorize(Roles = "1")]
        [Route("[Action]")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(_context.Users.ToList<User>());
        }

        //-----> on the working api
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult<Laptop>> AddNewLaptop([FromBody] Laptop laptop)
        {

            _context.Laptops.Add(laptop);
            await _context.SaveChangesAsync();
            return Ok("Laptop Added Successfully");

        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult<Software>> AddNewSoftware([FromBody] Software software)
        {

            _context.Softwares.Add(software);
            await _context.SaveChangesAsync();
            return Ok("Software Added Successfully");

        }


        [HttpPut, Authorize(Roles = "1")]
        [Route("[Action]")]
        public async Task<ActionResult<User>> MapLaptopToUser( [FromBody] User user)
        {
            if (true)
            {
                var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (userdb != null)
                {
                    userdb.LaptopId = user.LaptopId;
                    await _context.SaveChangesAsync();
                    return Ok(userdb);
                }
                return Ok("Error while Mapping");

            }

        }

      }







    }

