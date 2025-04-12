using Fiap.Application.Common;
using Fiap.Application.Games.Models.Requests;
using Fiap.Application.Games.Models.Responses;
using System.Threading.Tasks;

public interface IGamesService
{
    Task<BaseResponse<GameResponse>> CreateAsync(CreateGameRequest request);

    Task<IEnumerable<GameResponse>> GetAllAsync();
}
