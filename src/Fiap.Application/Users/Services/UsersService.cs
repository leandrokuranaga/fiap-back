using Fiap.Application.Common;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Application.Validators.UsersValidators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.SeedWork.Exceptions;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.ValueObjects;

namespace Fiap.Application.User.Services
{
    public class UsersService(INotification notification, IUserRepository userRepository, IUnitOfWork unitOfWork) : BaseService(notification), IUsersService
    {
        public Task<List<UserResponse>> GetAllAsync() => ExecuteAsync(async () =>
        {
            var response = new List<UserResponse>();

            var users = await userRepository.GetAllAsync();

            var allUsers = users
                .Select(user => (UserResponse)user)
                .ToList();

            return allUsers;
        });

        public Task<UserResponse> GetAsync(int userId) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

                var user = await userRepository.GetByIdAsync(userId, noTracking: false);

                if (user == null)
                {
                    _notification.AddNotification("Get User", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                response = (UserResponse)user;

                return response;
        });

        public Task<UserResponse> CreateAsync(CreateUserRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            Validate(request, new CreateUserRequestValidator());

            var email = request.Email.Trim().ToLowerInvariant();

            var exists = await userRepository.ExistAsync(u => u.Email.Address == request.Email.ToLower());
            if (exists)
            {
                _notification.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules);
                return response;
            }

            var user = (Domain.UserAggregate.User)request;

            await unitOfWork.BeginTransactionAsync();

            await userRepository.InsertOrUpdateAsync(user);
            await userRepository.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response = (UserResponse)user;

            return response;            
        });

        public Task<UserResponse> CreateAdminAsync(CreateUserAdminRequest request) => ExecuteAsync(async () =>
        {
            var response = new UserResponse();

            Validate(request, new CreateUserAdminRequestValidator());

            var email = request.Email.Trim().ToLowerInvariant();

            var exists = await userRepository.ExistAsync(u => u.Email.Address.ToLower() == email);
            if (exists)
            {
                _notification.AddNotification("Create User", "Email already registered", NotificationModel.ENotificationType.BusinessRules);
                return response;
            }

            var user = (Domain.UserAggregate.User)request;
            await unitOfWork.BeginTransactionAsync();

            await userRepository.InsertOrUpdateAsync(user);
            await userRepository.SaveChangesAsync();

            await unitOfWork.CommitAsync();

            response = (UserResponse)user;

            return response;

        });

        public Task<BaseResponse<object>> UpdateAsync(int id, UpdateUserRequest request) => ExecuteAsync<BaseResponse<object>>(async () =>
        {
            var response = new UserResponse();

            Validate(request, new UpdateUserRequestValidator());

            var user = await userRepository.GetByIdAsync(id, noTracking: false);

            if (user == null)
            {
                _notification.AddNotification("Update User", "User not found", NotificationModel.ENotificationType.NotFound);
                return BaseResponse<object>.Fail(_notification.NotificationModel);
            }

            UpdateUserProperties(user, request);

            await unitOfWork.BeginTransactionAsync();
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            return BaseResponse<object>.Ok(null);
        });

        private void UpdateUserProperties(Domain.UserAggregate.User user, UpdateUserRequest request)
        {
            user.Name = request.Name ?? user.Name;
            user.Email = new Email(request.Email ?? user.Email.Address);
            user.TypeUser = request.Type.HasValue ? request.Type.Value : user.TypeUser;
            user.Active = request.Active ?? user.Active;

            if (!string.IsNullOrEmpty(request.Password))
                user.Password = new Password(request.Password);
        }

        public Task<BaseResponse<object>> DeleteAsync(int id) => ExecuteAsync(async () =>
        {
            var user = await userRepository.GetByIdAsync(id, noTracking: false);

            if (user == null)
            {
                _notification.AddNotification("Delete User", "User Not found", NotificationModel.ENotificationType.NotFound);
                return BaseResponse<object>.Fail(_notification.NotificationModel);
            }

            await unitOfWork.BeginTransactionAsync();
            await userRepository.DeleteAsync(user);
            await userRepository.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            return BaseResponse<object>.Ok(null);
        });

        public Task<List<UserLibraryGameResponse>> GetGamesByUserAsync(int id) => ExecuteAsync(async () =>
        {
            var response = new GameResponse();

            var user = await userRepository.GetByIdGameUserAsync(id, noTracking:true);

            if (user is null)
            {
                _notification.AddNotification("Get User", "User not found", NotificationModel.ENotificationType.NotFound);
                return [];
            }

            return user.LibraryGames.Select(lg => (UserLibraryGameResponse)lg).ToList();
        });
    }
}