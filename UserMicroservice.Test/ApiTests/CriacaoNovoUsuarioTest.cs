using Moq;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services;
using UserMicroservice.Domain.Services;

namespace UserMicroservice.Test.ApiTests
{
    public class CriacaoNovoUsuarioTest
    {
        private readonly Mock<UserAppService> _mockService;
        private readonly UserDTO _userRegisterMock;
        private readonly Mock<UserServiceDomain> _serviceDomain;

        public CriacaoNovoUsuarioTest()
        {

            _userRegisterMock = new UserDTO
            { 
                Id = Guid.NewGuid(),
                Username = "anna", 
                Email = "anna@teste.com",
                Password = "@#Teste123",
                Role ="usuario",
                DataModificacao = DateTime.Now
            };
            _mockService = new Mock<UserAppService>(_userRegisterMock);
        }

        [Fact]
        public void CriacaoNovoUsuarioSucesso()
        {
            //Arrange
            var dados = _mockService.Object.Register(_userRegisterMock);
            _serviceDomain.Setup(service => service.IsUsernameAvailableAsync(_userRegisterMock.Username))
            .ReturnsAsync(true);

            //Act

            //Assert
            Assert.True(dados.Result);
        }

        [Fact]
        public void CriacaoNovoUsuarioComErro()
        {
            // Arrange
            _serviceDomain.Setup(service => service.IsUsernameAvailableAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
           // Act
            var dados = _mockService.Object.Register(_userRegisterMock);

            // Assert
            Assert.False(dados.Result);
        }
    }    
}
    