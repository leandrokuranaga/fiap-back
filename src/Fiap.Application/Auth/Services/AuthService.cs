using Fiap.Application.Auth.Models;
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
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotification _notification;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            INotification notification,
            IConfiguration configuration
        ) : base(notification)
        {
            _userRepository = userRepository;
            _notification = notification;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetOneNoTracking(a => a.Email == request.Username);

            if (user == null)
            {
                _notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
                return null;
            }

            if (!user.Active)
            {
                _notification.AddNotification("Login Failed", "Your account is disabled. Please contact support.", NotificationModel.ENotificationType.Unauthorized);
                return null;
            }
            
            bool validPassword = user.Password.Challenge(request.Password, user.Password.PasswordSalt);

            if (!validPassword)
            {
                _notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
                return null;
            }

            var token = GenerateJwtToken(user); 

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2)
            };
        }

    
        private string GenerateJwtToken(Fiap.Domain.UserAggregate.User user)
        {
            
            var secretKey = _configuration["JwtSettings:SecretKey"];
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
