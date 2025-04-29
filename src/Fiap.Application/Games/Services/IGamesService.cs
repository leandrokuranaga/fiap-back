using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using System.Threading.Tasks;

namespace Fiap.Application.Games.Services
{
    public interface IGamesService
    {
        Task<GameResponse> CreateAsync(CreateGameRequest request);
        Task<IEnumerable<GameResponse>> GetAllAsync();
    }
}
