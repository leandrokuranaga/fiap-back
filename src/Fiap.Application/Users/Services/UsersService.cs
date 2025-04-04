using Fiap.Application.Common;
using Fiap.Application.Contact.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UsersAggregate;
using Microsoft.Extensions.Caching.Memory;

namespace Fiap.Application.User.Services
{
    public class UsersService(INotification notification, IUserRepository userRepository) : BaseService(notification), IUsersService
    {

        Task<CreateUserResponse> IUsersService.Create(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        Task<UpdateUserResponse> IUsersService.Update(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
