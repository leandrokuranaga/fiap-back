using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Application.Validators.UsersValidators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UsersAggregate;

namespace Fiap.Application.User.Services
{
    public class UsersService(INotification notification, IUserRepository userRepository) : BaseService(notification), IUsersService
    {
        public Task<UserResponse> Create(CreateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            try
            {
                Validate(request, new CreateUserRequestValidator());

                var exists = await userRepository.ExistAsync(u => u.Email == request.Email);
                if (exists)
                {
                    _notification.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules);
                    return new UserResponse();
                }

                var user = (UserDomain)request;

                await userRepository.InsertOrUpdateAsync(user);

                response = (UserResponse)user;

                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Create User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        public Task<UserResponse> Update(int id, UpdateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            try
            {
                Validate(request, new UpdateUserRequestValidator());

                var user = await userRepository.GetByIdAsync(id, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Update User", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                UpdateUserProperties(user, request);

                await userRepository.UpdateAsync(user);

                return (UserResponse)user;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Update User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        private void UpdateUserProperties(UserDomain user, UpdateUserRequest request)
        {
            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.TypeUser = request.Type ?? user.TypeUser;
            user.Active = request.Active ?? user.Active;

            if (!string.IsNullOrEmpty(request.Password))
                user.Password = PasswordHasher.HashPassword(request.Password);
        }

        public Task<BaseResponse<object>> Delete(int id) => ExecuteAsync(async () =>
        {
            var user = await userRepository.GetByIdAsync(id, noTracking: false);

            if (user == null)
            {
                _notification.AddNotification("Delete User", "User Not found", NotificationModel.ENotificationType.NotFound);
                return BaseResponse<object>.Fail(_notification.NotificationModel);
            }

            await userRepository.DeleteAsync(user);
            return BaseResponse<object>.Ok(null);
        });

        public Task<UserResponse> Get(int userId) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            try
            {
                var user = await userRepository.GetByIdAsync(userId, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Get User", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                response = (UserResponse)user;

                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Get User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        public Task<List<UserResponse>> GetAll() => ExecuteAsync(async () =>
        {
            var response = new List<UserResponse>();

            try
            {
                var users = await userRepository.GetAllAsync();

                var allUsers = users
                    .Select(user => (UserResponse)user)
                    .ToList();

                return allUsers;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Get Users", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

    }
}