using Fiap.Application.Common;
using Fiap.Application.Library.Models.Response;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.SeedWork;

namespace Fiap.Application.Library.Services
{
    public class LibraryService(INotification notification, ILibraryRepository libraryRepository) : BaseService(notification), ILibraryService
    {
        public Task<List<LibraryResponse>> GetAllByUserLoggedAsync(int userId) => ExecuteAsync(async () =>
        {
            List<LibraryResponse> response = new List<LibraryResponse>();

            try
            {
                if (userId.Equals(0))
                {
                    _notification.AddNotification("Get Library by User logged", "User not found", NotificationModel.ENotificationType.NotFound);
                    return response;
                }

                var allLibrary = await libraryRepository.GetAllAsync();
                response = allLibrary
                                .Where(l => l.UserId.Equals(userId))
                                .Select(l => (LibraryResponse)l)
                                .ToList();

                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Get Library by User logged", ex.Message, NotificationModel.ENotificationType.InternalServerError);
                return response;
            }
        });
    }
}
