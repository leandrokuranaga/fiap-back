using Fiap.Infra.CrossCutting.Http.BrasilAPI.Models.Response;

namespace Fiap.Infra.CrossCutting.Http.ViaCEP.Services
{
    public interface IBrasilAPIService
    {
        Task<ApiResponse> GetState(int ddd);
    }
}
