using Moq;
using UserMicroservice.Application.DTO;
using UserMicroservice.Application.Services;

namespace UserMicroservice.Test.ApiTests
{
    public class AutenticarUsuarioExistenteTest
    {
        private readonly Mock<UserAppService> _mockService;
        private readonly LoginUserDTO _loginMock;

        public AutenticarUsuarioExistenteTest()
        {
            _loginMock = new LoginUserDTO { DataAcesso = DateTime.Now.ToString(), Username = "anna", Password = "123" };

            _mockService = new Mock<UserAppService>();
            _mockService.Setup(s => s.Login(_loginMock.Username, _loginMock.Password, DateTime.Now));
        }

        [Fact]
        public void AutenticarUsuarioExistenteSucesso()
        {
            //Arrange
            var dados = _mockService.Object.Login(_loginMock.Username, _loginMock.Password, DateTime.Now);

            //Act
            var resultado = dados.Result.Username == _loginMock.Username;

            //Assert
            Assert.True(resultado);

        }
        [Fact]
        public void AutenticarUsuarioExistenteFalha()
        {
            //Arrange
            var dados = _mockService.Object.Login(_loginMock.Username, _loginMock.Password, DateTime.Now);

            //Act
            var resultado = dados.Result.Username != _loginMock.Username;

            //Assert
            Assert.False(resultado);
        }   
      
    }

}