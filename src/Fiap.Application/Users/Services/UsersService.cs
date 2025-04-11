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

                var user = CreateUserDomain(request);

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

        private UserDomain CreateUserDomain(CreateUserRequest request)
        {
            var hashedPassword = PasswordHasher.HashPassword(request.Password);

            return new UserDomain(
                request.Name,
                request.Email,
                hashedPassword,
                TypeUser.User,
                request.Active
            );
        }

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

                UpdateUserProperties(user, request);

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
                var user = await userRepository.GetByIdAsync(request.UserId, noTracking: false);

                if (user == null)
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

                return MapToGetUserResponse(user);
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

                 response = users
                     .Select(MapToGetUserResponse)
                     .ToList();

                 return response;
             }
             catch (Exception ex)
             {
                 _notification.AddNotification("Get All Users", ex.Message, NotificationModel.ENotificationType.InternalServerError);
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

        private GetUserResponse MapToGetUserResponse(UserDomain user)
        {
            return new GetUserResponse
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Type = user.TypeUser,
                Active = user.Active
            };
        }
    }
}