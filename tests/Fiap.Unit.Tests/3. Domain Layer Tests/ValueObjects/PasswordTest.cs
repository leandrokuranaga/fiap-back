using Fiap.Domain.UserAggregate.ValueObjects;
using Xunit;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class PasswordTest
    {
        [Fact]
        public void Constructor_GeneratesHashAndSalt_ForPlainPassword()
        {
            // Arrange
            var plainTextPassword = "mySecurePassword";

            // Act
            var passwordVO = new Password(plainTextPassword);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(passwordVO.Hash));
            Assert.False(string.IsNullOrWhiteSpace(passwordVO.PasswordSalt));
        }

        [Fact]
        public void Challenge_ReturnsTrue_WhenPasswordIsCorrect()
        {
            // Arrange
            var plainTextPassword = "123456";
            var passwordVO = new Password(plainTextPassword);

            // Act
            var result = passwordVO.Challenge(plainTextPassword, passwordVO.PasswordSalt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Challenge_ReturnsFalse_WhenPasswordIsIncorrect()
        {
            // Arrange
            var correctPassword = "correct123";
            var wrongPassword = "wrong456";
            var passwordVO = new Password(correctPassword);

            // Act
            var result = passwordVO.Challenge(wrongPassword, passwordVO.PasswordSalt);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HashConsistency_WithSameInput_ProducesDifferentHashesDueToRandomSalt()
        {
            // Arrange
            var passwordText = "repeated";

            // Act
            var p1 = new Password(passwordText);
            var p2 = new Password(passwordText);

            // Assert
            Assert.NotEqual(p1.Hash, p2.Hash);
        }

        [Fact]
        public void Constructor_FromHashAndSalt_CreatesDeterministicPassword()
        {
            // Arrange
            var original = new Password("abc@123");

            // Act
            var clone = new Password(original.Hash, original.PasswordSalt);

            // Assert
            Assert.Equal(original.Hash, clone.Hash);
            Assert.Equal(original.PasswordSalt, clone.PasswordSalt);
        }

        [Fact]
        public void GetAtomicValues_UsedForValueEquality()
        {
            // Arrange
            var p1 = new Password("same");
            var p2 = new Password(p1.Hash, p1.PasswordSalt);

            // Act & Assert
            Assert.Equal(p1.Hash, p2.Hash);
            Assert.Equal(p1.PasswordSalt, p2.PasswordSalt);
        }
    }
}
