using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.ValueObjects;

namespace UserMicroservice.Test.DomainTests
{
    public class UserTests
    {
        [Fact]
        public void User_Creation_Valid()
        {
            //var username = new Username("fernanda");
            //var user = new User(username, "teste@email.com","hashedpass", "Admin");
            //Assert.Equal("fernanda", user.Username);
            //Assert.Equal("Admin", user.Role);
        }

        [Fact]
        public void ComparePassword_ShouldReturnTrue()
        {
            //var user = new User(new Username("bob"), "123", "User");
            //Assert.True(user.ComparePassword("123"));
            //Assert.False(user.ComparePassword("xxx"));
        }
    }
}
