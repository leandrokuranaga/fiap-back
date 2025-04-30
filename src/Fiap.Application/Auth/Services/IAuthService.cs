using Fiap.Application.Auth.Models.Request;
using Fiap.Application.Auth.Models.Response;

namespace Fiap.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
