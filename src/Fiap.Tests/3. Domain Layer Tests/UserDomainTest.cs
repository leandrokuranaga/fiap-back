using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.SeedWork.Enums;
using Fiap.Domain.UserAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class UserDomainTest
    {
        [Fact]
        public void UserDomain_DefaultConstructor_CreatesInstanceSuccessfully()
        {
            #region Act
            var user = new UserDomain();
            #endregion

            #region Assert
            Assert.NotNull(user);
            Assert.Null(user.Name);
            Assert.Null(user.Email);
            Assert.Null(user.Password);
            Assert.Equal(default(TypeUser), user.TypeUser); 
            Assert.False(user.Active);
            Assert.Null(user.Library);
            #endregion
        }
        [Fact]
        public void UserDomain_Creation_Success()
        {
            #region Arrange
            var name = "Test User";
            var email = "testuser@gmail.com";
            var password = "password123";
            var typeUser = TypeUser.Admin;
            var active = true;
            var library = new LibraryDomain(1);
            #endregion

            #region Act
            var user = new UserDomain(name, email, password, typeUser, active)
            {
                Library = library
            };
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(typeUser, user.TypeUser);
            Assert.True(user.Active);
            Assert.Equal(library, user.Library);
            #endregion
        }

        [Fact]
        public void UserDomain_Library_IsNull_ByDefault()
        {
            #region Arrange
            var user = new UserDomain("Test User", "testuser@gmail.com", "password123", TypeUser.User, true);
            #endregion

            #region Act & Assert
            Assert.Null(user.Library);
            #endregion
        }

        [Fact]
        public void CreateByAdmin_CreatesUserCorrectly()
        {
            #region Arrange
            var name = "Admin User";
            var email = "admin@gmail.com";
            var password = "adminpassword";
            var typeUser = TypeUser.Admin;
            var active = true;
            var library = new LibraryDomain(1);
            #endregion

            #region Act
            var user = UserDomain.CreateByAdmin(name, email, password, typeUser, active);
            user.Library = library; 
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(typeUser, user.TypeUser);
            Assert.True(user.Active);
            Assert.Equal(library, user.Library);
            #endregion
        }

        [Fact]
        public void CreateByPublic_CreatesUserCorrectly()
        {
            #region Arrange
            var name = "Public User";
            var email = "publicuser@gmail.com";
            var password = "publicpassword";
            #endregion

            #region Act
            var user = UserDomain.CreateByPublic(name, email, password);
            #endregion

            #region Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(TypeUser.User, user.TypeUser);
            Assert.True(user.Active);
            Assert.Null(user.Library);
            #endregion
        }

        [Fact]
        public void UserDomain_UpdatesPropertiesCorrectly()
        {
            #region Arrange
            var user = new UserDomain("Old Name", "oldemail@gmail.com", "oldpassword", TypeUser.User, true);
            var newName = "New Name";
            var newEmail = "newemail@gmail.com";
            var newPassword = "newpassword";
            var newTypeUser = TypeUser.Admin;
            var newActive = false;
            #endregion

            #region Act
            user.Name = newName;
            user.Email = newEmail;
            user.Password = newPassword;
            user.TypeUser = newTypeUser;
            user.Active = newActive;
            #endregion

            #region Assert
            Assert.Equal(newName, user.Name);
            Assert.Equal(newEmail, user.Email);
            Assert.Equal(newPassword, user.Password);
            Assert.Equal(newTypeUser, user.TypeUser);
            Assert.False(user.Active);
            #endregion
        }
    }
}
