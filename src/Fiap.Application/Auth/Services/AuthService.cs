using Fiap.Application.Auth.Models;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate; // CERTA importação do User do domínio
using Fiap.Application.Users.Services;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens; // IMPORTANTE: adiciona
using System.IdentityModel.Tokens.Jwt; // IMPORTANTE: adiciona

namespace Fiap.Application.Auth.Services
{
    public class AuthService(IUserRepository userRepository, INotification notification) : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.GetOneNoTracking(a => a.Email == request.Username);

            if (user == null)
            {
                notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
                return null;
            }

            // Agora usa o ValueObject Password
            bool validPassword = user.Password.Challenge(request.Password, user.Password.PasswordSalt);

            if (!validPassword)
            {
                notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
                return null;
            }

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        private static string GenerateJwtToken(Fiap.Domain.UserAggregate.User user)
        {
            var key = Encoding.ASCII.GetBytes("seu-secret-key-super-seguro"); // Trocar depois pra appsettings

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.TypeUser.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
