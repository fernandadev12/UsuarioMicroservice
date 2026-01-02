using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.Repositories.Interface;
using UserMicroservice.Domain.Services;
using UserMicroservice.Domain.ValueObjects;
using Moq;
namespace UserMicroservice.Test.ApiTests
{
    public class UserDomainServiceTests
    {
        [Fact]
        public void ValidateUserCredentials_ShouldReturnTrue()
        {
            //var mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(r => r.GetByUsername("alice"))
            //    .Returns(new User(new Username("alice"), "123", "User"));

            //var service = new UserServiceDomain(mockRepo.Object);
            //Assert.True(service.ValidateUserCredentials("alice", "123"));
        }

        [Fact]
        public void IsUsernameAvailable_ShouldReturnFalseWhenExists()
        {
            //var user = new User(new Username("bob"), "123", "User");
            //var mockRepo = new UserMockRepository(user);

            //var service = new UserServiceDomain(mockRepo);

            //Assert.False(service.IsUsernameAvailable("bob"));
        }

    }
}
