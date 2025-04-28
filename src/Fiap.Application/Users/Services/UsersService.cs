using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Application.Validators.UsersValidators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.ValueObjects;

namespace Fiap.Application.User.Services
{
    public class UsersService(INotification notification, IUserRepository userRepository) : BaseService(notification), IUsersService
    {
        public Task<List<UserResponse>> GetAllAsync() => ExecuteAsync(async () =>
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

        public Task<UserResponse> GetAsync(int userId) => ExecuteAsync(async () =>
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

        public Task<UserResponse> CreateAsync(CreateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            try
            {
                Validate(request, new CreateUserRequestValidator());

                request.Email = request.Email.Trim().ToLowerInvariant();

                var exists = await userRepository.ExistAsync(u => u.Email == request.Email.ToLower());
                if (exists)
                {
                    _notification.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules);
                    return new UserResponse();
                }

                var user = (Domain.UserAggregate.User)request;

                await userRepository.InsertOrUpdateAsync(user);
                await userRepository.SaveChangesAsync();

                response = (UserResponse)user;

                return response;
            }
            catch (Exception ex)
            {
                await userRepository.RollbackAsync();

                if (!_notification.HasNotification)
                    _notification.AddNotification("Create User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        

        public Task<UserResponse> CreateAdminAsync(CreateUserAdminRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            try
            {
                Validate(request, new CreateUserAdminRequestValidator());

                request.Email = request.Email.Trim().ToLowerInvariant();

                await userRepository.BeginTransactionAsync();

                var exists = await userRepository.ExistAsync(u => u.Email == request.Email.ToLower());
                if (exists)
                {
                    _notification.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules);
                    return new UserResponse();
                }

                var user = (Domain.UserAggregate.User)request;               

                await userRepository.InsertOrUpdateAsync(user);
                await userRepository.SaveChangesAsync();

                await userRepository.CommitAsync();

                response = (UserResponse)user;

                return response;
            }
            catch (Exception ex)
            {
                await userRepository.RollbackAsync();

                if (!_notification.HasNotification)
                    _notification.AddNotification("Create User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        public Task<UserResponse> UpdateAsync(int id, UpdateUserRequest request) => ExecuteAsync(async () =>
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
                await userRepository.SaveChangesAsync();

                return (UserResponse)user;
            }
            catch (Exception ex)
            {
                await userRepository.RollbackAsync();

                _notification.AddNotification("Update User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });

        private void UpdateUserProperties(Domain.UserAggregate.User user, UpdateUserRequest request)
        {
            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.TypeUser = request.Type.HasValue ? request.Type.Value : user.TypeUser;
            user.Active = request.Active ?? user.Active;

            if (!string.IsNullOrEmpty(request.Password))
                user.Password = new Password(request.Password);
        }

        public Task<BaseResponse<object>> DeleteAsync(int id) => ExecuteAsync(async () =>
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Delete User", "User Not found", NotificationModel.ENotificationType.NotFound);
                    return BaseResponse<object>.Fail(_notification.NotificationModel);
                }

                await userRepository.DeleteAsync(user);
                await userRepository.SaveChangesAsync();

                return BaseResponse<object>.Ok(null);
            }
            catch(Exception ex)
            {
                await userRepository.RollbackAsync();
                _notification.AddNotification("Delete User", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return BaseResponse<object>.Fail(_notification.NotificationModel);
            }
        });

        
    }
}