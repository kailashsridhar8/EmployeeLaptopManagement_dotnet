using LaptopManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost, Authorize(Roles = "0")]
        [Route("[Action]")]
        public async Task<object> AddSoftwareToLaptop([FromBody] int softwareid)
        {
            InstalledSoftware installedSoftware = new InstalledSoftware();



            int id = decode();

            var dbUser=_context.Users.Where(x => x.Id == id).ToList();


            installedSoftware.LaptopId = dbUser[0].LaptopId;

             var dbSoft = await _context.Softwares.FirstOrDefaultAsync(x => x.Id == softwareid);
            
            if (dbSoft != null)
            {
                var dbLap = _context.InstalledSoftwares.Where(x => x.LaptopId == installedSoftware.LaptopId).ToList();

                if (dbLap[0].SoftwareId == softwareid)
                {
                    return Ok("Software Already Exists on Laptop");
                }


                installedSoftware.SoftwareId = softwareid;
                _context.InstalledSoftwares.Add(installedSoftware);
                await _context.SaveChangesAsync();
                return Ok("Software Added to Laptop Successfully");

            }
            else
            {
                return Ok("Software Id does not match to any softwares");
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


    }
}
