using Fiap.Application.User.Services;
using Fiap.Application.Users.Models.Request;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;
using Fiap.Domain.UserAggregate.ValueObjects;
using Moq;
using System.Linq.Expressions;

namespace Fiap.Tests._2._Application_Layer_Tests
{
    public class UsersServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<INotification> _mockNotification;
        private readonly UsersService _usersService;

        public UsersServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockNotification = new Mock<INotification>();
            _usersService = new UsersService(_mockNotification.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnUserResponse_WhenValidRequest()
        {
            #region Arrange
            var request = new CreateUserRequest
            {
                Name = "Bruno Moura",
                Email = "bruno@example.com",
                Password = "Password@123"
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);

            _mockUserRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    user.Id = 1;
                    return user;
                });
            #endregion

            #region Act
            var result = await _usersService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(1, result.UserId);
            #endregion
        }

        [Fact]
        public async Task Create_ShouldAddNotification_WhenEmailAlreadyExists()
        {
            #region Arrange
            var request = new CreateUserRequest
            {
                Name = "Bruno Moura",
                Email = "bruno@example.com",
                Password = "Password@123"
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);

            _mockUserRepository
              .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<User>()))
              .ReturnsAsync((User user) =>
              {
                  user.Id = 1;
                  return user;
              });
            #endregion

            #region Act
            var result = await _usersService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules), Times.Once);
            #endregion
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedUserResponse_WhenValidRequest()
        {
            #region Arrange
            int userId = 1;
            var request = new UpdateUserRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com"
            };

            var user = new User
            {
                Id = userId,
                Name = "Original Name",
                Email = new Email("original.email@example.com")
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync(user);

            _mockUserRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);
            #endregion

            #region Act
            var result = await _usersService.UpdateAsync(userId, request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public async Task Update_ShouldAddNotification_WhenUserNotFound()
        {
            #region Arrange
            int userId = 1;
            var request = new UpdateUserRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com"
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync((User)null);
            #endregion

            #region Act
            var result = await _usersService.UpdateAsync(userId, request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);
            #endregion
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccess_WhenUserExists()
        {
            #region Arrange
            int userId = 1;

            var user = new User
            {
                Id = userId,
                Name = "Bruno Moura",
                Email = new Email("bruno@example.com")
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync(user);

            _mockUserRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);
            #endregion

            #region Act
            var result = await _usersService.DeleteAsync(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            #endregion
        }

        [Fact]
        public async Task Delete_ShouldAddNotification_WhenUserNotFound()
        {
            #region Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync((User)null);
            #endregion

            #region Act
            var result = await _usersService.DeleteAsync(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);
            #endregion
        }

        [Fact]
        public async Task Get_ShouldReturnUserResponse_WhenUserExists()
        {
            #region Arrange
            int userId = 1;

            var user = new User
            {
                Id = userId,
                Name = "Bruno Moura",
                Email = new Email("bruno@example.com")
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync(user);
            #endregion

            #region Act
            var result = await _usersService.GetAsync(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email.Address, result.Email);
            #endregion
        }

        [Fact]
        public async Task Get_ShouldAddNotification_WhenUserNotFound()
        {
            #region Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync((User)null);
            #endregion

            #region Act
            var result = await _usersService.GetAsync(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);
            #endregion
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfUserResponses()
        {
            #region Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "Bruno Moura", Email = new Email("bruno@example.com") },
                new User { Id = 2, Name = "Jane Doe", Email = new Email("jane.doe@example.com") }
            };

            _mockUserRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(users);
            #endregion

            #region Act
            var result = await _usersService.GetAllAsync();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
            Assert.Equal(users[0].Name, result[0].Name);
            Assert.Equal(users[1].Name, result[1].Name);
            #endregion
        }
        [Fact]        
        public async Task Create_ShouldAddNotificationAndThrowException_WhenExceptionIsThrown()
        {
            #region Arrange
            var request = new CreateUserRequest
            {
                Name = "Bruno Moura",
                Email = "bruno@example.com",
                Password = "Password@123"
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ThrowsAsync(new Exception("Unexpected error"));
            #endregion

            #region Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _usersService.CreateAsync(request));
            #endregion

            #region Assert
            Assert.NotNull(exception);
            Assert.Equal("Unexpected error", exception.Message);
            _mockNotification.Verify(n =>
                n.AddNotification("Create User", "Unexpected error", NotificationModel.ENotificationType.InternalServerError),
                Times.Once);
            #endregion
        }

        [Fact]
        public async Task Update_ShouldAddNotificationAndThrowException_WhenExceptionIsThrown()
        {
            #region Arrange
            int userId = 1;
            var request = new UpdateUserRequest
            {
                Name = "Updated Name",
                Email = "updated.email@example.com"
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ThrowsAsync(new Exception("Unexpected error"));
            #endregion

            #region Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _usersService.UpdateAsync(userId, request));
            #endregion

            #region Assert
            Assert.NotNull(exception);
            Assert.Equal("Unexpected error", exception.Message);

            _mockNotification.Verify(n =>
                n.AddNotification("Update User", "Unexpected error", NotificationModel.ENotificationType.InternalServerError),
                Times.Once);
            #endregion
        }
        [Fact]
        public async Task Get_ShouldAddNotification_WhenExceptionIsThrown()
        {
            #region Arrange
            int userId = 1;

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ThrowsAsync(new Exception("Unexpected error"));
            #endregion

            #region Act
            var result = await _usersService.GetAsync(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification("Get User", "Unexpected error", NotificationModel.ENotificationType.InternalServerError), Times.Once);
            #endregion
        }
        [Fact]
        public async Task GetAll_ShouldAddNotification_WhenExceptionIsThrown()
        {
            #region Arrange
            _mockUserRepository
                .Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Unexpected error"));
            #endregion

            #region Act
            var result = await _usersService.GetAllAsync();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockNotification.Verify(n => n.AddNotification("Get Users", "Unexpected error", NotificationModel.ENotificationType.InternalServerError), Times.Once);
            #endregion
        }

        [Fact]
        public async Task Delete_ShouldAddNotificationAndThrowException_WhenExceptionIsThrown()
        {
            #region Arrange
            int userId = 1;

            var user = new User
            {
                Id = userId,
                Name = "Bruno Moura",
                Email = new Email("bruno@example.com")
            };

            _mockUserRepository
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<bool>()))
                .ReturnsAsync(user);

            _mockUserRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            _mockUserRepository
                .Setup(repo => repo.RollbackAsync())
                .Returns(Task.CompletedTask);
            #endregion

            #region Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _usersService.DeleteAsync(userId));
            #endregion

            #region Assert
            Assert.NotNull(exception);
            Assert.Equal("Unexpected error", exception.Message);
            _mockUserRepository.Verify(r => r.RollbackAsync(), Times.Once);
            _mockNotification.Verify(n =>
                n.AddNotification("Delete User", "Unexpected error", NotificationModel.ENotificationType.InternalServerError),
                Times.Once);
            #endregion
        }

        [Fact]
        public async Task CreateAdminAsync_ShouldReturnUserResponse_WhenValidRequest()
        {
            // Arrange
            var request = new CreateUserAdminRequest
            {
                Name = "Admin User",
                Email = "admin@example.com",
                Password = "Secure@123",
                Active = true,
                TypeUser = TypeUser.Admin
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(false);

            _mockUserRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    user.Id = 1;
                    return user;
                });

            // Act
            var result = await _usersService.CreateAdminAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal(request.Email.ToLower(), result.Email.ToLower());
            Assert.Equal(request.Name, result.Name);
        }

        [Fact]
        public async Task CreateAdminAsync_ShouldAddNotification_WhenEmailAlreadyExists()
        {
            // Arrange
            var request = new CreateUserAdminRequest
            {
                Name = "Admin User",
                Email = "admin@example.com",
                Password = "Secure@123",
                Active = true,
                TypeUser = TypeUser.Admin
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _usersService.CreateAdminAsync(request);

            // Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n =>
                n.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules),
                Times.Once);
        }

        [Fact]
        public async Task CreateAdminAsync_ShouldAddNotificationAndThrowException_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new CreateUserAdminRequest
            {
                Name = "Admin User",
                Email = "admin@example.com",
                Password = "Secure@123",
                Active = true,
                TypeUser = TypeUser.Admin
            };

            _mockUserRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            _mockUserRepository
                .Setup(repo => repo.RollbackAsync())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _usersService.CreateAdminAsync(request));

            // Assert
            #region Assert
            Assert.NotNull(exception);
            Assert.Equal("Unexpected error", exception.Message);

            _mockNotification.Verify(n =>
                n.AddNotification("Create User", "Unexpected error", NotificationModel.ENotificationType.InternalServerError),
                Times.Once);
            #endregion
        }
    }
}
