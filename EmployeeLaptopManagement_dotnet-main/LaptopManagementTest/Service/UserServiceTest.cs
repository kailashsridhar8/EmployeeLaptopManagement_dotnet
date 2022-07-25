using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaptopManagement.Controllers;
using LaptopManagement.Models;
using LaptopManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LaptopManagementTest.Services
{
    public class UserServiceTest
    {
        private readonly UserController _controller;
        Mock<UserService> servicemock = new Mock<UserService>();

        public UserServiceTest()
        {
            _controller = new UserController(servicemock.Object);
        }

        [Fact]
        public async Task UpdateUser_Return_Success()
        {
            var userid = 2;
            var ExpectedUser = new User
            {
                 Id =4,
                 Name= "mega",
                 EmailId="iniyan", 
                 Password="iniyan", 
                 Role =0

    };
            servicemock.Setup(x => x.UpdateUser(userid,ExpectedUser)).ReturnsAsync(ExpectedUser);

            var ActualUser = await _controller.UpdateUserProfile(userid,ExpectedUser);
            var result = ActualUser as OkObjectResult;

            Assert.IsType<OkObjectResult>(ActualUser);

        }

      
    }
}