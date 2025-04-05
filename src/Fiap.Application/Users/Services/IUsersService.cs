using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;

namespace Fiap.Application.Users.Services
{
    public interface IUsersService
    {
        Task<CreateUserResponse> Create(CreateUserRequest request);
        Task<UpdateUserResponse> Update(UpdateUserRequest request);
    }
}
