using Fiap.Domain.SeedWork.Enums;
using Fiap.Domain.UserAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class UserDomainTest
    {
        [Fact]
        public void UserDomainSuccess()
        {
            #region Arrange
            var mockUserDomain = new UserDomain()
            {
                Name = "Test User",
                Email = "le.silva@hotmail.com",
                Password = "password",
                TypeUser = TypeUser.Admin,
                Active = true,
            };

            #endregion

            #region Act
            var mockUserDomainAct = new UserDomain(
                mockUserDomain.Name,
                mockUserDomain.Email,
                mockUserDomain.Password,
                mockUserDomain.TypeUser,
                mockUserDomain.Active
            );

            #endregion

            #region Assert
            Assert.Equal(mockUserDomain.Name, mockUserDomainAct.Name);
            Assert.Equal(mockUserDomain.Email, mockUserDomainAct.Email);
            Assert.Equal(mockUserDomain.Password, mockUserDomainAct.Password);
            Assert.Equal(mockUserDomain.TypeUser, mockUserDomainAct.TypeUser);
            Assert.Equal(mockUserDomain.Active, mockUserDomainAct.Active);
            #endregion

        }
    }
}
