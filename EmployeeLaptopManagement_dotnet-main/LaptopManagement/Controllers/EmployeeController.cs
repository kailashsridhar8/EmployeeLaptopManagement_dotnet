using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;

namespace LaptopManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly laptopmanagementContext _context;
        public EmployeeController(laptopmanagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<List<Software>>> GetAllAvailableSoftwares()
        {
            return Ok(_context.Softwares.ToList<Software>());
        }


        [HttpPost, Authorize(Roles = "0")]
        [Route("[Action]")]
        public async Task<object> AddSoftwareToLaptop([FromBody] int softwareid)
        {
           

            int id = decode();

            var dbUser=await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var dbLaptop = await _context.Laptops.FirstOrDefaultAsync(x => x.UserId == dbUser.Id);


            if (dbLaptop!=null)
            {
                InstalledSoftware installedSoftware = new InstalledSoftware();

                installedSoftware.LaptopId = dbLaptop.Id;
                var dbSoft = await _context.Softwares.FirstOrDefaultAsync(x => x.Id == softwareid);

                if (dbSoft != null)
                {
                    var dbSoftware = _context.InstalledSoftwares.Where(x => x.LaptopId == installedSoftware.LaptopId).ToList();

                    foreach (var soft in dbSoftware)
                    {
                        if (soft.SoftwareId == softwareid)
                        {
                            return Ok("Software Already Exists on Laptop");
                        }
                    }


                    installedSoftware.SoftwareId = softwareid;
                    _context.InstalledSoftwares.Add(installedSoftware);
                    await _context.SaveChangesAsync();
                    Send.Producer("Installed a new software");
                    return Ok("Software Added to Laptop Successfully");

                }
                else
                {
                    return Ok("Software Id does not match to any softwares");
                }

            }
            else
            {
                return BadRequest("Employee dont have a laptop yet");
            }


        }




        private int decode()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            return Convert.ToInt32(id);
        }




        [HttpDelete]
        [Route("[Action]")]
        public async Task<ActionResult<InstalledSoftware>> RemoveSoftwareFromLaptop(InstalledSoftware installedsoftware)
        {
            var dbSoftware = await _context.Softwares.FirstOrDefaultAsync(x => x.Id == installedsoftware.SoftwareId);

            if (dbSoftware != null)
            {
                _context.InstalledSoftwares.Remove(installedsoftware);

                await _context.SaveChangesAsync();
                return Ok(new { message = "Laptop removed successfully" });
            }
            else if (dbSoftware == null)
            {
                return BadRequest("No such software id exists");
            }

            return BadRequest("Error removing software from laptop");


        }




        [HttpGet]
        [Route("[Action]/{laptopid}")]
        public async Task<ActionResult<Object>> SoftwaresInstalled(int laptopid)
        {
            var dbSoftwares = _context.InstalledSoftwares.Where(x => x.LaptopId == laptopid).ToList();
            var installedsoftwares = new ArrayList();
            foreach (var softwares in dbSoftwares)
            {
                var software= await _context.Softwares.FirstOrDefaultAsync(x => x.Id == softwares.SoftwareId);
                installedsoftwares.Add(software.Name);
            }

            return Ok(installedsoftwares);
        }






    }
}
