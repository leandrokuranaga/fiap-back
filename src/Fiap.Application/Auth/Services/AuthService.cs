using Fiap.Application.Auth.Models.Request;
using Fiap.Application.Auth.Models.Response;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Application.Auth.Services
{
    public class AuthService(
        IUserRepository userRepository,
        INotification notification,
        IConfiguration configuration
        ) : BaseService(notification), IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request) => await ExecuteAsync(async () =>
    {
        var login = new LoginResponse();


        var user = await userRepository.GetOneNoTracking(a => a.Email == request.Username);

        if (user is null)
        {
            notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
            return login;
        }

        if (!user.Active)
        {
            notification.AddNotification("Login Failed", "Your account is disabled. Please contact support.", NotificationModel.ENotificationType.Unauthorized);
            return login;
        }

        bool validPassword = user.Password.Challenge(request.Password, user.Password.PasswordSalt);

        if (!validPassword)
        {
            notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
            return login;
        }

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(2)
        };
    });


        private string GenerateJwtToken(Fiap.Domain.UserAggregate.User user)
        {

            var secretKey = configuration["JwtSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Email),
                new(ClaimTypes.Role, user.TypeUser.ToString())
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
