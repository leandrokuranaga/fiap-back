using Fiap.Application.Auth.Models.Request;
using Fiap.Application.Auth.Services;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;
using Fiap.Domain.UserAggregate.ValueObjects;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Fiap.Unit.Tests._2._Application_Layer_Tests
{
    public class AuthServiceTests
    {
        readonly Mock<IUserRepository> _userRepoMock;
        readonly Mock<INotification> _notificationMock;
        readonly Mock<IConfiguration> _configMock;
        readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _notificationMock = new Mock<INotification>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(c => c["JwtSettings:SecretKey"]).Returns("5037baaf-7201-4b9c-b4dc-6c51306b18fa");

            _authService = new AuthService(_userRepoMock.Object, _notificationMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var plainPassword = "Teste@teste12334";

            var user = new User("User Name", "user@test.com", plainPassword, TypeUser.Admin, true)
            {
                Id = 1,
                Password = new Password(plainPassword)
            };

            _userRepoMock
                .Setup(r => r.GetOneNoTracking(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            var request = new LoginRequest
            {
                Username = "user@test.com",
                Password = plainPassword
            };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.False(string.IsNullOrEmpty(result.Token));
            Assert.True(result.Expiration > DateTime.UtcNow);
        }

        [Fact]
        public async Task LoginAsync_ShouldAddNotification_WhenUserNotFound()
        {
            // Arrange
            var request = new LoginRequest { Username = "invalid@test.com", Password = "any" };
            _userRepoMock.Setup(r =>
                r.GetOneNoTracking(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null!);

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.Null(result.Token);
            Assert.Equal(default, result.Expiration);
            _notificationMock.Verify(n =>
                n.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.NotFound), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldAddNotification_WhenUserIsInactive()
        {
            // Arrange
            var request = new LoginRequest { Username = "inactive@test.com", Password = "123456" };
            var user = new User("Inactive User", "inactive@test.com", "invalidHash@1salt", TypeUser.Admin, false)
            {
                Id = 2
            };

            _userRepoMock
                .Setup(r => r.GetOneNoTracking(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.Null(result.Token);
            Assert.Equal(default, result.Expiration);
            _notificationMock.Verify(n =>
                n.AddNotification("Login Failed", "Your account is disabled. Please contact support.", NotificationModel.ENotificationType.BusinessRules), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldAddNotification_WhenPasswordIsInvalid()
        {
            // Arrange
            var request = new LoginRequest { Username = "user@test.com", Password = "wrongpass" };

            // Sobrescrevendo método Challenge para simular falha
            var user = new User("User", "user@test.com", "invalidHash@1salt", TypeUser.Admin, true)
            {
                Id = 3
            };

            // Adapte Challenge() no VO para este caso
            _userRepoMock
                .Setup(r => r.GetOneNoTracking(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.Null(result.Token);
            Assert.Equal(default, result.Expiration);
            _notificationMock.Verify(n =>
                n.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized), Times.Once);
        }
    }
}
