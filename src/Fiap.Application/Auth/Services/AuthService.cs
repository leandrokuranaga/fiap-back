using Fiap.Application.Auth.Models;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using System.Security.Claims;
using System.Text;

namespace Fiap.Application.Auth.Services
{
    public class AuthService(IUserService userRepository, INotification notification) : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.ge(request.Username);

            if (user == null)
            {
                notification.Add("Usuário ou senha inválidos.");
                return null;
            }

            var hashedPassword = HashPassword(request.Password, user.Salt);

            if (hashedPassword != user.PasswordHash)
            {
                notification.Add("Usuário ou senha inválidos.");
                return null;
            }

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        private static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            var bytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes("seu-secret-key-super-seguro"); // Ajustar depois para appsettings

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                // Exemplo: se seu user tiver um campo Role
                new Claim(ClaimTypes.Role, user.Role ?? "User")
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
