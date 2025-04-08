using Abp;
using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Application.Validators.UsersValidators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.SeedWork.Enums;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UsersAggregate;

namespace Fiap.Application.User.Services
{
    public class UsersService(INotification notification, IUserRepository userRepository) : BaseService(notification), IUsersService
    {
        public Task<CreateUserResponse> Create(CreateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new CreateUserResponse();

            try
            {
                Validate(request, new CreateUserRequestValidator());

                var user = new UserDomain(
                    request.Name,
                    request.Email,
                    request.Password,
                    TypeUser.User,
                    true
                );

                await userRepository.InsertOrUpdateAsync(user);

                response.UserId = user.Id;
                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Create User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        public Task<UpdateUserResponse> Update(UpdateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new UpdateUserResponse();

            try
            {
                Validate(request, new UpdateUserRequestValidator());

                var user = await userRepository.GetByIdAsync(request.Id, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Update User", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                if (!string.IsNullOrEmpty(request.Name))
                    user.Name = request.Name;

                if (!string.IsNullOrEmpty(request.Email))
                    user.Email = request.Email;

                if (!string.IsNullOrEmpty(request.Password))
                    user.Password = request.Password;

                if (request.Type.HasValue)
                    user.TypeUser = request.Type.Value;

                if (request.Active.HasValue)
                    user.Active = request.Active.Value;

                await userRepository.UpdateAsync(user);

                response.UserId = user.Id;
                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Update User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });


        public Task<DeleteUserResponse> Delete(DeleteUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new DeleteUserResponse();

            try
            {
                var user = await userRepository.GetByIdAsync(request.UserId, noTracking:false);

                if(user == null)
                {
                    _notification.AddNotification("Delete User", "User Not found", NotificationModel.ENotificationType.InternalServerError);
                    return response;
                }

                await userRepository.DeleteAsync(user);

                response.UserId = user.Id;
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {

                _notification.AddNotification("Delete User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                response.Success = false;
                return response;
            }

        });

        public Task<GetUserResponse> Get(int userId) => ExecuteAsync(async () =>
        {
            var response = new GetUserResponse();

            try
            {
                var user = await userRepository.GetByIdAsync(userId, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Get User", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                response.UserId = user.Id;
                response.Name = user.Name;
                response.Email = user.Email;
                response.Type = user.TypeUser;
                response.Active = user.Active;

                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Get User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        public Task<List<GetUserResponse>> GetAll() => ExecuteAsync(async () =>
        {
            var response = new List<GetUserResponse>();

            try
            {
                var users = await userRepository.GetAllAsync();

                var allUsers = users
                    .Where(u => u.TypeUser == TypeUser.Admin)
                    .Select(user => new GetUserResponse
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Type = user.TypeUser,
                        Active = user.Active
                    })
                    .ToList();

                return allUsers;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Get Admin Users", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });


    }
}