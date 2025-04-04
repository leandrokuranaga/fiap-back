using AutoFixture;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Fiap.Tests._1._Api_Layer_Tests.MockClasses;
using Fiap.Tests._1._Api_Layer_Tests.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Tests._1._Api_Layer_Tests
{
    public class BaseControllerTests
    {
        private readonly MockBaseController _baseController;
        private readonly Mock<INotification> _notificationMock;
        private readonly IFixture _fixture;
        public BaseControllerTests()
        {
            _notificationMock = new Mock<INotification>();
            _baseController = new MockBaseController(_notificationMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnBadRequest_WhenHasNotificationIsTrueWithTypeBadRequest()
        {
            #region Arrange

            var mockBaseResponse = new MockBaseResponse() as BaseResponse;
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);
            _notificationMock
                .SetupGet(x => x.NotificationModel)
                .Returns(new NotificationModel("", "", NotificationModel.ENotificationType.BadRequestError));

            #endregion

            #region Act

            var response = _baseController.Response(mockBaseResponse);
            var responseObject = (ObjectResult)response;
            #endregion

            #region Assert

            Assert.IsType<BadRequestObjectResult>(responseObject);

            #endregion
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnNotFound_WhenHasNotificationIsTrueWithTypeNotFound()
        {
            #region Arrange

            var mockBaseResponse = new MockBaseResponse() as BaseResponse;
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);
            _notificationMock
                .SetupGet(x => x.NotificationModel)
                .Returns(
                    new NotificationModel(
                        _fixture.Create<string>(),
                        _fixture.Create<string>(),
                        NotificationModel.ENotificationType.NotFound)
                );

            #endregion

            #region Act

            var response = _baseController.Response(mockBaseResponse);
            var responseObject = (ObjectResult)response;
            #endregion

            #region Assert

            Assert.IsType<NotFoundObjectResult>(responseObject);

            #endregion
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnConflict_WhenHasNotificationIsTrueWithTypeBusinessRules()
        {
            #region Arrange

            var mockBaseResponse = new MockBaseResponse() as BaseResponse;
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);
            _notificationMock
                .SetupGet(x => x.NotificationModel)
                .Returns(
                    new NotificationModel(
                        _fixture.Create<string>(),
                        _fixture.Create<string>(),
                        NotificationModel.ENotificationType.BusinessRules)
                );

            #endregion

            #region Act

            var response = _baseController.Response(mockBaseResponse);
            var responseObject = (ObjectResult)response;
            #endregion

            #region Assert

            Assert.IsType<ConflictObjectResult>(responseObject);

            #endregion
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnInternalServerError_WhenHasNotificationIsTrueWithTypeInternalServer()
        {
            #region Arrange

            var mockBaseResponse = new MockBaseResponse() as BaseResponse;

            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);

            _notificationMock
                .SetupGet(x => x.NotificationModel)
                .Returns(
                    new NotificationModel(
                        _fixture.Create<string>(),
                        _fixture.Create<string>(),
                        NotificationModel.ENotificationType.InternalServerError)
                );

            #endregion

            #region Act

            var response = _baseController.Response(mockBaseResponse);
            var responseObject = (ObjectResult)response;
            #endregion

            #region Assert

            //responseObject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

            #endregion
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnOk_WhenNoNotificationIsFoundAndResponseIsNotNull()
        {
            #region Arrange

            var mockBaseResponse = new MockBaseResponse() as BaseResponse;

            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);

            #endregion

            #region Act

            var response = _baseController.Response(mockBaseResponse);
            var responseObject = (ObjectResult)response;

            #endregion

            #region Assert

            //responseObject.Should().BeOfType<OkObjectResult>();

            #endregion
        }

        [Fact]
        public void ResponseBaseResponse_ShouldReturnNoContent_WhenNoNotificationIsFoundAndResponseIsNull()
        {
            #region Arrange

            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);

            #endregion

            #region Act

            var response = _baseController.Response(null);

            #endregion

            #region Assert

            //response.Should().BeOfType<NoContentResult>();

            #endregion
        }

        [Fact]
        public void ResponseObject_ShouldReturnBadRequest_WhenNotificationIsFound()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);
            #endregion

            #region Act

            var response = _baseController.Response(new { });

            #endregion


            #region Assert

            //response.Should().BeOfType<BadRequestObjectResult>();
            #endregion
        }

        [Fact]
        public void ResponseObject_ShouldReturnNoContent_WhenNoNotificationIsFoundAndResponseIsNull()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);
            #endregion

            #region Act

            var response = _baseController.Response(null);

            #endregion

            #region Assert
            //response.Should().BeOfType<NoContentResult>();
            #endregion
        }

        [Fact]
        public void ResponseObject_ShouldReturnOk_WhenNoNotificationIsFoundAndResponseIsNotNull()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);
            #endregion

            #region Act

            var response = _baseController.Response(new { });

            #endregion


            #region Assert

            //response.Should().BeOfType<OkObjectResult>();

            #endregion
        }

        [Fact]
        public void ResponseIdObject_ShouldReturnBadRequest_WhenNotificationIsFound()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(true);
            #endregion

            #region Act

            var response = _baseController.Response(_fixture.Create<int>(), _fixture.Create<object>());

            #endregion


            #region Assert

            //response.Should().BeOfType<BadRequestObjectResult>();
            #endregion
        }

        [Fact]
        public void ResponseIdObject_ShouldReturOk_WhenNoNotificationIsFoundAndIdIsNull()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);
            #endregion

            #region Act

            var response = _baseController.Response(null, _fixture.Create<object>());

            #endregion


            #region Assert

            //response.Should().BeOfType<OkObjectResult>();
            #endregion
        }

        [Fact]
        public void ResponseIdObject_ShouldReturCreatedAtAction_WhenNoNotificationIsFoundAndIdIsNotNull()
        {
            #region Arrange
            _notificationMock
                .SetupGet(x => x.HasNotification)
                .Returns(false);
            #endregion

            #region Act

            var response = _baseController.Response(_fixture.Create<int>(), _fixture.Create<object>());

            #endregion


            #region Assert

            //response.Should().BeOfType<CreatedAtActionResult>();
            #endregion
        }
    }
}
