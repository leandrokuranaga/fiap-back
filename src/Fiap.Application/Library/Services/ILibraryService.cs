using Fiap.Application.Library.Models.Response;

namespace Fiap.Application.Library.Services
{
    public interface ILibraryService
    {
        Task<List<LibraryResponse>> GetAllByUserLoggedAsync(int userId);
    }
}
