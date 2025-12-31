using Microsoft.EntityFrameworkCore;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Domain.ValueObjects;
using UserMicroservice.Infra.Data;
using UserMicroservice.Infra.Data.Repository;
namespace UserMicroservice.Test.InfraTest
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Add_And_GetByUsername()
        {
            //var options = new DbContextOptionsBuilder<UsersDbContext>()
            //    .UseInMemoryDatabase(databaseName: "UserTestDB")
            //    .Options;
            //using var context = new UsersDbContext(options);
            //var repo = new UserRepository(context);
            //var user = new User(new Username("eve"), "secret", "User");
            //repo.Add(user);

            //var result = repo.GetByUsername("eve");
            //Assert.NotNull(result);
            //Assert.Equal("User", result.Role);
        }
    }
}
