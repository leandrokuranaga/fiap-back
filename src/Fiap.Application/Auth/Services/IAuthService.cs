using Fiap.Application.Auth.Models;

namespace Fiap.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
