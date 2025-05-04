using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;
using Fiap.Domain.UserAggregate.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class UserDomainTest
    {
        [Fact]
        public void UserDomain_Creation_Success()
        {
            #region Arrange
            var name = "Test User";
            var email = "testuser@gmail.com";
            var password = "password@123";
            var typeUser = TypeUser.Admin;
            var active = true;
            #endregion

            #region Act
            var user = new User(name, email, password, typeUser, active);
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email.Trim().ToLowerInvariant(), user.Email.Address);
            Assert.True(user.Password.Challenge(password, user.Password.PasswordSalt));
            Assert.Equal(typeUser, user.TypeUser);
            Assert.True(user.Active);
            Assert.NotNull(user.LibraryGames);
            #endregion
        }

        [Fact]
        public void CreateByAdmin_CreatesUserCorrectly()
        {
            #region Arrange
            var name = "Admin User";
            var email = "admin@gmail.com";
            var password = "admin@1password";
            var typeUser = TypeUser.Admin;
            var active = true;
            #endregion

            #region Act
            var user = User.CreateByAdmin(name, email, password, typeUser, active);
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email.Trim().ToLowerInvariant(), user.Email.Address);
            Assert.True(user.Password.Challenge(password, user.Password.PasswordSalt));
            Assert.Equal(typeUser, user.TypeUser);
            Assert.True(user.Active);
            Assert.NotNull(user.LibraryGames);
            #endregion
        }

        [Fact]
        public void CreateByPublic_CreatesUserCorrectly()
        {
            #region Arrange
            var name = "Public User";
            var email = "publicuser@gmail.com";
            var password = "public@1password";
            #endregion

            #region Act
            var user = User.CreateByPublic(name, email, password);
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email.Trim().ToLowerInvariant(), user.Email.Address);
            Assert.True(user.Password.Challenge(password, user.Password.PasswordSalt));
            Assert.Equal(TypeUser.User, user.TypeUser);
            Assert.True(user.Active);
            Assert.NotNull(user.LibraryGames);
            #endregion
        }

        [Fact]
        public void UserDomain_UpdatesPropertiesCorrectly()
        {
            #region Arrange
            var user = new User("Old Name", "oldemail@gmail.com", "old@123password", TypeUser.User, true);
            var newName = "New Name";
            var newEmail = "newemail@gmail.com";
            var newPassword = "new@123password";
            var newTypeUser = TypeUser.Admin;
            var newActive = false;
            #endregion

            #region Act
            user.Name = newName;
            user.Email = new Email(newEmail);
            user.Password = new Password(newPassword);
            user.TypeUser = newTypeUser;
            user.Active = newActive;
            #endregion

            #region Assert
            Assert.Equal(newName, user.Name);
            Assert.Equal(newEmail, user.Email.Address);
            Assert.True(user.Password.Challenge(newPassword, user.Password.PasswordSalt));
            Assert.Equal(newTypeUser, user.TypeUser);
            Assert.False(user.Active);
            Assert.NotNull(user.LibraryGames);
            #endregion
        }
    }
}
