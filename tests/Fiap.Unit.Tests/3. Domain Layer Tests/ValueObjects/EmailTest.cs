using Fiap.Domain.SeedWork.Exceptions;
using Fiap.Domain.UserAggregate.ValueObjects;

namespace Fiap.Unit.Tests._3._Domain_Layer_Tests.ValueObjects
{
    public class EmailTest
    {
        [Fact]
        public void Constructor_ShouldCreateEmail_WhenValidAddress()
        {
            // Arrange
            var validEmail = "test.user@domain.com";

            // Act
            var email = new Email(validEmail);

            // Assert
            Assert.Equal(validEmail.ToLowerInvariant(), email.Address);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailIsEmpty()
        {
            // Arrange
            var emptyEmail = "";

            // Act & Assert
            Assert.Throws<BusinessRulesException>(() => new Email(emptyEmail));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailIsNull()
        {
            // Arrange
            string? nullEmail = null;

            // Act & Assert
            Assert.Throws<BusinessRulesException>(() => new Email(nullEmail!));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailExceedsMaxLength()
        {
            // Arrange
            var longEmail = new string('a', 101) + "@domain.com";

            // Act & Assert
            Assert.Throws<BusinessRulesException>(() => new Email(longEmail));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailHasInvalidFormat()
        {
            // Arrange
            var invalidEmail = "invalid-email";

            // Act & Assert
            Assert.Throws<BusinessRulesException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void Constructor_ShouldTrimSpaces_WhenEmailHasLeadingOrTrailingSpaces()
        {
            // Arrange
            var emailWithSpaces = "   user@domain.com   ";

            // Act
            var email = new Email(emailWithSpaces);

            // Assert
            Assert.Equal("user@domain.com", email.Address);
        }

        [Fact]
        public void Constructor_ShouldNormalizeToLowerCase()
        {
            // Arrange
            var mixedCaseEmail = "Test.User@Domain.COM";

            // Act
            var email = new Email(mixedCaseEmail);

            // Assert
            Assert.Equal("test.user@domain.com", email.Address);
        }

        //[Fact]
        //public void GetAtomicValues_ShouldReturnAddress()
        //{
        //    // Arrange
        //    var email = new Email("user@domain.com");

        //    // Act
        //    var atomicValues = email.GetAtomicValues().ToList();

        //    // Assert
        //    Assert.Single(atomicValues);
        //    Assert.Contains("user@domain.com", atomicValues);
        //}
    }
}