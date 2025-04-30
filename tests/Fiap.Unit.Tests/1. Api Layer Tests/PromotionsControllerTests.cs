using Fiap.Api.Controllers;
using Fiap.Application.Common;
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
            var mockCreateRequest = new CreatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                GameId = [1, 2, 3]
            };

            var mockCreateResponse = new PromotionResponse
            {
                PromotionId = 1,
                Discount = mockCreateRequest.Discount,
                StartDate = DateTime.UtcNow,
                EndDate = mockCreateRequest.ExpirationDate
            };
            #endregion

            #region Act
            _promotionsServiceMock
                .Setup(x => x.CreateAsync(mockCreateRequest))
                .ReturnsAsync(mockCreateResponse);

            var result = await _controller.CreateAsync(mockCreateRequest);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<PromotionResponse>>(okResult.Value);

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

            var mockUpdateRequest = new UpdatePromotionRequest
            {
                Discount = 20,
                ExpirationDate = DateTime.UtcNow.AddDays(60),
                GameId = [1, 2, 3]
            };
            var mockUpdateResponse = new PromotionResponse
            {
                PromotionId = 1,
                Discount = mockUpdateRequest.Discount.Value,
                StartDate = DateTime.UtcNow,
                EndDate = mockUpdateRequest.ExpirationDate.Value
            };
            #endregion

            #region Act
            _promotionsServiceMock
                .Setup(x => x.UpdateAsync(promotionId, mockUpdateRequest))
                .ReturnsAsync(mockUpdateResponse);

            var result = await _controller.UpdateAsync(promotionId, mockUpdateRequest);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<BaseResponse<PromotionResponse>>(okResult.Value);
            #endregion

            #region Assert
            Assert.True(response.Success);
            #endregion
        }
        #endregion
    }
}
