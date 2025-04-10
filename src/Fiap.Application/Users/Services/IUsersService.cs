using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;

namespace Fiap.Application.Users.Services
{
    public interface IUsersService
    {
        Task<UserResponse> Create(CreateUserRequest request);
        Task<UserResponse> Update(int id, UpdateUserRequest request);
        Task<BaseResponse<object>> Delete(int id);
        Task<UserResponse> Get(int userId);
        Task<List<UserResponse>> GetAll();

    }
}
