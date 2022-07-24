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

            var dbSoftware = await _context.Softwares.FirstOrDefaultAsync(x => x.Name == software.Name);

            if (dbSoftware == null)
            {
                _context.Softwares.Add(software);
                await _context.SaveChangesAsync();
                return Ok("Software Added Successfully");
            }

            return Ok("Software already exists");
        }


        [HttpPut, Authorize(Roles = "1")]
        [Route("[Action]")]
        public async Task<ActionResult<Laptop>> MapLaptopToUser([FromBody] Laptop laptop)
        {
            if (true)
            {
                var lapdb = await _context.Laptops.FirstOrDefaultAsync(x => x.Id == laptop.Id);

                var lapWithGivenUserId = await _context.Laptops.FirstOrDefaultAsync(x => x.UserId == laptop.UserId);



                if (lapWithGivenUserId != null)
                {
                    return Ok("User has laptop already!");
                }
                else
                {
                    if (lapdb != null)
                    {
                        lapdb.UserId = laptop.UserId;
                        await _context.SaveChangesAsync();
                        return Ok(lapdb);
                    }
                }


                return Ok("Error");

            }

        }



        [HttpDelete]
        [Route("[Action]/{id}")]

        public async Task<ActionResult<User>> RemoveEmployee(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var dbLaptop = await _context.Laptops.FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser != null && dbUser.Role != 1)
            {
                _context.Users.Remove(dbUser);
                dbLaptop.UserId = null;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Employee removed successfully" });
            }
            else if(dbUser == null)
            {
                return BadRequest("No such id exists");
            }
            else if (dbUser.Role == 1)
            {
                return BadRequest("Cannot remove a admin");
            }
            return BadRequest("Error removing");

        }


        [HttpDelete]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Software>> RemoveSoftware(int id)
        {
            var dbSoftware = await _context.Softwares.FirstOrDefaultAsync(x => x.Id == id);

            if (dbSoftware != null)
            {
                _context.Softwares.Remove(dbSoftware);
              
                await _context.SaveChangesAsync();
                return Ok(new { message = "Software removed successfully" });
            }
            else if (dbSoftware == null)
            {
                return BadRequest("No such software id exists");
            }
           
            return BadRequest("Error removing software");

        }


        [HttpDelete]
        [Route("[Action]/{id}")]
        public async Task<ActionResult<Laptop>> RemoveLaptop(int id)
        {
            var dbLaptop = await _context.Laptops.FirstOrDefaultAsync(x => x.Id == id);

            if (dbLaptop != null)
            {
                _context.Laptops.Remove(dbLaptop);

                await _context.SaveChangesAsync();
                return Ok(new { message = "Laptop removed successfully" });
            }
            else if (dbLaptop == null)
            {
                return BadRequest("No such laptop id exists");
            }

            return BadRequest("Error removing laptop");


        }




    }

    }

