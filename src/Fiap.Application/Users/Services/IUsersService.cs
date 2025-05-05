using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;

namespace Fiap.Application.Users.Services
{
    public interface IUsersService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> GetAsync(int userId);
        Task<UserResponse> CreateAsync(CreateUserRequest request);
        Task<UserResponse> CreateAdminAsync(CreateUserAdminRequest request);
        Task<BaseResponse<object>> UpdateAsync(int id, UpdateUserRequest request);
        Task<BaseResponse<object>> DeleteAsync(int id);     
    }
}
