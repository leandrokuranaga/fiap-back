using Fiap.Api.Controllers;
using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Unit.Tests._1._Api_Layer_Tests
{
    public class UsersControllerTests
    {
        readonly Mock<IUsersService> _usersServiceMock;
        readonly Mock<INotification> _notificationMock;
        readonly UsersController _controller;

        public UsersControllerTests()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _notificationMock = new Mock<INotification>();
            _controller = new UsersController(_usersServiceMock.Object, _notificationMock.Object);
        }

        #region CreateUser

        [Fact]
        public async Task CreateUser_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var request = new CreateUserRequest { Email = "user@test.com", Name = "Test User", Password = "123456" };

            var response = new UserResponse { UserId = 1, Email = request.Email, Name = request.Name };

            _usersServiceMock
                .Setup(x => x.CreateAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(response.Email, baseResponse.Data.Email);
        }

        #endregion

        #region CreateAdminUser

        [Fact]
        public async Task CreateAdminUser_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var request = new CreateUserAdminRequest { Email = "admin@test.com", Name = "Admin", Password = "admin123", Active = true };

            var response = new UserResponse { UserId = 1, Email = request.Email, Name = request.Name };

            _usersServiceMock
                .Setup(x => x.CreateAdminAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateAdminAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(response.Email, baseResponse.Data.Email);
        }

        #endregion

        #region GetUser

        [Fact]
        public async Task GetUser_ShouldReturnOk_WhenServiceReturnsUser()
        {
            // Arrange
            var response = new UserResponse { UserId = 1, Email = "user@test.com", Name = "Test User" };

            _usersServiceMock
                .Setup(x => x.GetAsync(1))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(response.UserId, baseResponse.Data.UserId);
        }

        #endregion

        #region GetAllUsers

        [Fact]
        public async Task GetAllUsers_ShouldReturnOk_WhenServiceReturnsList()
        {
            // Arrange
            var responseList = new List<UserResponse>
            {
                new() { UserId = 1, Email = "user1@test.com", Name = "User 1" },
                new() { UserId = 2, Email = "user2@test.com", Name = "User 2" }
            };

            _usersServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(responseList);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<List<UserResponse>>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(2, baseResponse.Data.Count);
        }

        #endregion

        #region UpdateUser

        [Fact]
        public async Task UpdateUser_ShouldReturnOk_WhenServiceReturnsUpdatedUser()
        {
            // Arrange
            var request = new UpdateUserRequest { Name = "Updated User" };
            var response = new UserResponse { UserId = 1, Name = request.Name, Email = "user@test.com" };

            _usersServiceMock
                .Setup(x => x.UpdateAsync(1, request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateAsync(1, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(request.Name, baseResponse.Data.Name);
        }

        #endregion

        #region DeleteUser

        [Fact]
        public async Task DeleteUser_ShouldReturnOk_WhenServiceReturnsSuccessResponse()
        {
            // Arrange
            var successResponse = BaseResponse<object>.Ok(null);

            _usersServiceMock
                .Setup(x => x.DeleteAsync(1))
                .ReturnsAsync(successResponse);

            // Act
            var result = await _controller.DeleteAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<EmptyResultModel>>(okResult.Value);

            Assert.True(baseResponse.Success);
        }
        #endregion
    }
}
