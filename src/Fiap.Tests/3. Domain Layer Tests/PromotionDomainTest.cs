using Fiap.Domain.PromotionAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class PromotionDomainTest
    {
        [Fact]
        public void PromotionDomainSuccess()
        {
            #region Arrange
            var mockPromotionDomain = new PromotionDomain()
            {
                Discount = 10.0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
            };
            #endregion

            #region Act
            var mockPromotionDomainAct = new PromotionDomain(
                mockPromotionDomain.Discount,
                mockPromotionDomain.StartDate,
                mockPromotionDomain.EndDate
            );
            #endregion

            #region Assert
            Assert.Equal(mockPromotionDomain.Discount, mockPromotionDomainAct.Discount);
            Assert.Equal(mockPromotionDomain.StartDate, mockPromotionDomainAct.StartDate);
            Assert.Equal(mockPromotionDomain.EndDate, mockPromotionDomainAct.EndDate);
            #endregion
        }
    }
}
