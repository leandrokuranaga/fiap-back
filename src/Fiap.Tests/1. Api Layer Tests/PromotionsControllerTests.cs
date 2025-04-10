using Fiap.Api.Controllers;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;
using Fiap.Application.Promotions.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Tests._1._Api_Layer_Tests
{
    public class PromotionsControllerTests
    {
        readonly Mock<IPromotionsService> _promotionsServiceMock;
        readonly Mock<INotification> _mockNotification;
        readonly PromotionsController _controller;

        public PromotionsControllerTests()
        {
            _promotionsServiceMock = new Mock<IPromotionsService>();
            _mockNotification = new Mock<INotification>();
            _controller = new PromotionsController(_promotionsServiceMock.Object, _mockNotification.Object);
        }

        #region CreatePromotion

        [Fact]
        public async Task CreatePromotion_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            var _mockCreateRequest = new CreatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                GameId = new List<int?> { 1, 2, 3 }
            };

            var _mockCreateResponse = new PromotionResponse
            {
                PromotionId = 1,
                Discount = _mockCreateRequest.Discount,
                StartDate = DateTime.UtcNow,
                EndDate = _mockCreateRequest.ExpirationDate
            };
            #endregion

            #region Act
            _promotionsServiceMock
                .Setup(x => x.CreateAsync(_mockCreateRequest))
                .ReturnsAsync(_mockCreateResponse);

            var result = await _controller.Create(_mockCreateRequest);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as PromotionResponse;
            #endregion

            #region Assert
            Assert.True(response.Success);
            #endregion
        }
        #endregion

        #region UpdatePromotion

        [Fact]
        public async Task UpdatePromotion_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            int promotionId = 1;

            var _mockUpdateRequest = new UpdatePromotionRequest
            {
                Discount = 20,
                ExpirationDate = DateTime.UtcNow.AddDays(60),
                GameId = new List<int?> { 1, 2, 3 }
            };
            var _mockUpdateResponse = new PromotionResponse
            {
                PromotionId = 1,
                Discount = _mockUpdateRequest.Discount.Value,
                StartDate = DateTime.UtcNow,
                EndDate = _mockUpdateRequest.ExpirationDate.Value
            };
            #endregion

            #region Act
            _promotionsServiceMock
                .Setup(x => x.UpdateAsync(promotionId, _mockUpdateRequest))
                .ReturnsAsync(_mockUpdateResponse);

            var result = await _controller.Update(promotionId, _mockUpdateRequest);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as PromotionResponse;
            #endregion

            #region Assert
            Assert.True(response.Success);
            #endregion
        }
        #endregion
    }
}
