using Fiap.Api.Controllers;
using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Fiap.Domain.SeedWork.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Tests._1._Api_Layer_Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly Mock<INotification> _mockNotification;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _mockNotification = new Mock<INotification>();
            _controller = new UsersController(_usersServiceMock.Object, _mockNotification.Object);
        }

        #region Create

        [Fact]
        public async Task Create_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            var mockCreateRequest = new CreateUserRequest
            {
                Name = "Bruno Moura",
                Email = "bruno.moura@example.com",
                Password = "Password@123"
            };

            var mockCreateResponse = new UserResponse
            {
                UserId = 1,
                Name = mockCreateRequest.Name,
                Email = mockCreateRequest.Email,
                Active = true,
                Type = TypeUser.User
            };

            _usersServiceMock
                .Setup(x => x.Create(mockCreateRequest))
                .ReturnsAsync(mockCreateResponse);
            #endregion

            #region Act
            var result = await _controller.Create(mockCreateRequest);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal(mockCreateResponse.UserId, response.Data.UserId);
            Assert.Equal(mockCreateResponse.Name, response.Data.Name);
            Assert.Equal(mockCreateResponse.Email, response.Data.Email);
            Assert.Equal(mockCreateResponse.Active, response.Data.Active);
            Assert.Equal(mockCreateResponse.Type, response.Data.Type);
            #endregion
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            int userId = 1;

            var mockUpdateRequest = new UpdateUserRequest
            {
                Name = "Brunão Updated Name",
                Email = "brunao.updated.name@example.com",
                Active = true,
                Type = TypeUser.Admin
            };

            var mockUpdateResponse = new UserResponse
            {
                UserId = userId,
                Name = mockUpdateRequest.Name,
                Email = mockUpdateRequest.Email,
                Active = mockUpdateRequest.Active.Value,
                Type = mockUpdateRequest.Type.Value
            };

            _usersServiceMock
                .Setup(x => x.Update(userId, mockUpdateRequest))
                .ReturnsAsync(mockUpdateResponse);
            #endregion

            #region Act
            var result = await _controller.Update(userId, mockUpdateRequest);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal(mockUpdateResponse.UserId, response.Data.UserId);
            Assert.Equal(mockUpdateResponse.Name, response.Data.Name);
            Assert.Equal(mockUpdateResponse.Email, response.Data.Email);
            Assert.Equal(mockUpdateResponse.Active, response.Data.Active);
            Assert.Equal(mockUpdateResponse.Type, response.Data.Type);
            #endregion
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            int userId = 1;

            _usersServiceMock
                .Setup(x => x.Delete(userId))
                .ReturnsAsync(BaseResponse<object>.Ok(null));
            #endregion

            #region Act
            var result = await _controller.Delete(userId);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<EmptyResultModel>>(okResult.Value);

            Assert.True(response.Success);
            #endregion
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            int userId = 1;

            var mockGetResponse = new UserResponse
            {
                UserId = userId,
                Name = "Bruno Moura",
                Email = "bruno.moura@example.com",
                Active = true,
                Type = TypeUser.User
            };

            _usersServiceMock
                .Setup(x => x.Get(userId))
                .ReturnsAsync(mockGetResponse);
            #endregion

            #region Act
            var result = await _controller.Get(userId);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<UserResponse>>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal(mockGetResponse.UserId, response.Data.UserId);
            Assert.Equal(mockGetResponse.Name, response.Data.Name);
            Assert.Equal(mockGetResponse.Email, response.Data.Email);
            Assert.Equal(mockGetResponse.Active, response.Data.Active);
            Assert.Equal(mockGetResponse.Type, response.Data.Type);            
            #endregion
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            var mockGetAllResponse = new List<UserResponse>
            {
                new UserResponse { UserId = 1, Name = "Bruno Moura", Email = "bruno.moura@example.com", Active = true, Type = TypeUser.User },
                new UserResponse { UserId = 2, Name = "Stella Moura", Email = "stella.moura@example.com", Active = true, Type = TypeUser.Admin }
            };

            _usersServiceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(mockGetAllResponse);
            #endregion

            #region Act
            var result = await _controller.GetAll();
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<List<UserResponse>>>(okResult.Value);

            Assert.True(response.Success);
            Assert.Equal(mockGetAllResponse, response.Data);
            #endregion
        }

        #endregion
    }
}
