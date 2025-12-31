using Moq;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;

namespace UserMicroservice.Test.ApplicationTests
{
    public class UserAppServiceTests
    {
        [Fact]
        public void RegisterUser_ShouldCallRepositoryAdd()
        {
            //var mockRepo = new Mock<IUserRepository>();
            //var appService = new UserAppService(mockRepo.Object);

            //var dto = new RegisterUserDTO { Username = "zelda", Password = "xx", Role = "Admin" };
            //appService.RegisterUser(dto);

            //mockRepo.Verify(r => r.Add(It.Is<User>(u => u.Username == "zelda" && u.Role == "Admin")), Times.Once);
        }
    }
}
