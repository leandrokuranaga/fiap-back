using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;

namespace Fiap.Application.Games.Services
{
    public interface IGamesService
    {
        Task<GameResponse> CreateAsync(CreateGameRequest request);
        Task<IEnumerable<GameResponse>> GetAllAsync();
        Task<GameResponse> GetAsync(int id);
    }
}
