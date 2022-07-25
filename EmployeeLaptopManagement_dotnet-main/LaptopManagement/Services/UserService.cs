using LaptopManagement.Models;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;
namespace LaptopManagement.Services
{
    public class UserService
    {
        private readonly laptopmanagementContext _context;
        public UserService()
        {

        }
        public UserService(laptopmanagementContext context)
        {
            _context = context;
        }
    

        public virtual async Task<object> UpdateUser(int userid, User user)
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
                    return userdb;

                }
                return null;

            }

        }

        private object Ok(User userdb)
        {
            throw new NotImplementedException();
        }
    }
}
