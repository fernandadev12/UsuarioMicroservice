using Microsoft.AspNetCore.Mvc;
using Moq;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services;
namespace UserMicroservice.Test.ApiTests
{
    public class UsersControllerTests
    {
        [Fact]
        public void Register_ReturnsOk()
        {
            //var mockService = new Mock<UserAppService>();
            //mockService.Setup(s => s.RegisterUser(It.IsAny<RegisterUserDTO>()));

            //var controller = new UserController(mockService.Object);
            //var result = controller.Register(new RegisterUserDTO { Username = "anna", Password = "123", Role = "User" });

            // Assert.IsType<OkObjectResult>(result);
        }
    }
}
